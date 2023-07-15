using Liber.Forms.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Liber.Forms;

internal sealed class RecentPathManager : Component
{
    private readonly JsonSerializerOptions s_options = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    private readonly Dictionary<string, DateTime> s_dates = new Dictionary<string, DateTime>();

    private SortedDictionary<DateTime, string>? s_paths;

    [Browsable(false)]
    public bool Empty
    {
        get
        {
            if (s_paths == null)
            {
                Load();
            }

            return s_paths == null || s_paths.Count == 0;
        }
    }

    [Browsable(false)]
    public IEnumerable<string> Paths
    {
        get
        {
            if (s_paths == null)
            {
                Load();

                if (s_paths == null)
                {
                    return Enumerable.Empty<string>();
                }
            }

            return s_paths.Values;
        }
    }

    public event EventHandler? Updated;

    public void Add(string path)
    {
        DateTime modified = DateTime.Now;

        if (s_paths == null)
        {
            Load();

            if (s_paths == null)
            {
                s_paths = new SortedDictionary<DateTime, string>(ReverseDateTimeComparer.Default);
            }
        }

        if (s_dates.TryGetValue(path, out DateTime lastModified))
        {
            s_paths.Remove(lastModified);
        }

        s_paths.Add(modified, path);
        s_dates[path] = modified;

        Save();
        Updated?.Invoke(sender: this, EventArgs.Empty);
    }

    private void Save()
    {
        Settings.Default.RecentPaths = JsonSerializer.Serialize(s_paths, s_options);

        Settings.Default.Save();
    }

    private void Load()
    {
        Dictionary<DateTime, string>? paths = JsonSerializer.Deserialize<Dictionary<DateTime, string>>(Settings.Default.RecentPaths, s_options);

        if (paths == null)
        {
            return;
        }

        s_paths = new SortedDictionary<DateTime, string>(paths, ReverseDateTimeComparer.Default);

        s_dates.Clear();

        foreach (KeyValuePair<DateTime, string> path in paths)
        {
            s_dates.Add(path.Value, path.Key);
        }
    }
}
