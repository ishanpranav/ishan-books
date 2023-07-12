using Liber.Readers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Liber;

internal sealed class Settings
{
    private readonly SortedSet<RecentItem> _recentItems;
    private readonly Dictionary<string, Reader> _readers;

    public Settings()
    {
        Groups = new Dictionary<string, IList<CustomMenuItem>>();
        Shortcuts = new List<CustomMenuItem>();
        _recentItems = new SortedSet<RecentItem>();
        _readers = new Dictionary<string, Reader>(StringComparer.OrdinalIgnoreCase);
    }

    [JsonConstructor]
    public Settings(
            IDictionary<string, IList<CustomMenuItem>> groups,
            IList<CustomMenuItem> shortcuts,
            IEnumerable<RecentItem> recentItems,
            IReadOnlyDictionary<string, Reader> readers)
    {
        Groups = groups;
        Shortcuts = shortcuts;
        _recentItems = new SortedSet<RecentItem>(recentItems);
        _readers = new Dictionary<string, Reader>(readers, StringComparer.OrdinalIgnoreCase);
    }

    public IDictionary<string, IList<CustomMenuItem>> Groups { get; }
    public IList<CustomMenuItem> Shortcuts { get; }
    public CultureInfo? Culture { get; set; }
    public int MaxRecentItems { get; set; }

    [JsonIgnore]
    public string? MostRecentPath
    {
        get
        {
            return _recentItems.Min?.Path;
        }
    }

    public IEnumerable<RecentItem> RecentItems
    {
        get
        {
            return _recentItems;
        }
    }

    public IReadOnlyDictionary<string, Reader> Readers
    {
        get
        {
            return _readers;
        }
    }

    public void ClearRecentItems()
    {
        _recentItems.Clear();
    }

    public void AddRecentItem(string path)
    {
        RecentItem value = new RecentItem(path)
        {
            Opened = DateTime.Now
        };

        _ = _recentItems.Remove(value);
        _ = _recentItems.Add(value);

        while (_recentItems.Count >= MaxRecentItems)
        {
            _ = _recentItems.Remove(_recentItems.Max!);
        }
    }
}
