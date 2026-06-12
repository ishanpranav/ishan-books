// RecentPathManager.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Windows.Shell;
using Liber.Forms.Properties;

namespace Liber.Forms.Components;

internal sealed class RecentPathManager : Component
{
    private readonly Dictionary<string, DateTime> _dates = new Dictionary<string, DateTime>();

    private SortedSet<RecentPath>? _paths;

    [Browsable(false)]
    public bool Empty
    {
        get
        {
            if (_paths == null)
            {
                Load();
            }

            return _paths == null || _paths.Count == 0;
        }
    }

    [Browsable(false)]
    public IEnumerable<string> Paths
    {
        get
        {
            if (_paths == null)
            {
                Load();

                if (_paths == null)
                {
                    return Enumerable.Empty<string>();
                }
            }

            return _paths.Select(x => x.Path);
        }
    }

    [DefaultValue(10)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int MaxPaths { get; set; } = 1;

    public event EventHandler? Invalidated;

    public RecentPathManager() { }

    public RecentPathManager(IContainer container)
    {
        container.Add(this);
    }

    public void Add(string path)
    {
        try
        {
            JumpList.AddToRecentCategory(path);
        }
        catch { }

        DateTime modified = DateTime.Now;

        if (_paths == null)
        {
            Load();

            if (_paths == null)
            {
                _paths = new SortedSet<RecentPath>();
            }
        }

        if (_dates.TryGetValue(path, out DateTime lastModified))
        {
            _paths.Remove(new RecentPath(path, lastModified));
        }

        _paths.Add(new RecentPath(path, modified));

        _dates[path] = modified;

        Save();
        Invalidated?.Invoke(sender: this, EventArgs.Empty);
    }

    private void Save()
    {
        if (_paths != null)
        {
            while (_paths.Count > MaxPaths && _paths.Max != null)
            {
                _paths.Remove(_paths.Max);
            }
        }

        Settings.Default.RecentPaths = JsonSerializer.Serialize(_paths, FormattedStrings.JsonOptions);

        Settings.Default.Save();
    }

    private void Load()
    {
        RecentPath[]? paths;

        try
        {
            paths = JsonSerializer.Deserialize<RecentPath[]>(Settings.Default.RecentPaths, FormattedStrings.JsonOptions);

            if (paths == null)
            {
                return;
            }
        }
        catch (JsonException)
        {
            return;
        }

        _paths = new SortedSet<RecentPath>(paths);

        _dates.Clear();

        foreach (RecentPath path in paths)
        {
            _dates[path.Path] = path.LastModified;
        }
    }
}
