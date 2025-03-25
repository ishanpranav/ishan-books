// RecentPathManager.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Windows.Shell;
using Liber.Forms.Properties;

namespace Liber.Forms.Components;

internal sealed class RecentPathManager : Component
{
    private readonly Dictionary<string, DateTime> _dates = new Dictionary<string, DateTime>();

    private SortedDictionary<DateTime, string>? _paths;

    public RecentPathManager() { }

    public RecentPathManager(IContainer container)
    {
        container.Add(this);
    }

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

            return _paths.Values;
        }
    }

    public event EventHandler? Updated;

    public void Add(string path)
    {
        JumpList.AddToRecentCategory(path);

        DateTime modified = DateTime.Now;

        if (_paths == null)
        {
            Load();

            if (_paths == null)
            {
                _paths = new SortedDictionary<DateTime, string>(ReverseDateTimeComparer.Default);
            }
        }

        if (_dates.TryGetValue(path, out DateTime lastModified))
        {
            _paths.Remove(lastModified);
        }

        _paths.Add(modified, path);
        _dates[path] = modified;

        Save();
        Updated?.Invoke(sender: this, EventArgs.Empty);
    }

    private void Save()
    {
        Settings.Default.RecentPaths = JsonSerializer.Serialize(_paths, FormattedStrings.JsonOptions);

        Settings.Default.Save();
    }

    private void Load()
    {
        Dictionary<DateTime, string>? paths = JsonSerializer.Deserialize<Dictionary<DateTime, string>>(Settings.Default.RecentPaths, FormattedStrings.JsonOptions);

        if (paths == null)
        {
            return;
        }

        _paths = new SortedDictionary<DateTime, string>(paths, ReverseDateTimeComparer.Default);

        _dates.Clear();

        foreach (KeyValuePair<DateTime, string> path in paths)
        {
            _dates.Add(path.Value, path.Key);
        }
    }
}
