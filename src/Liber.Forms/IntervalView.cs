using System;
using System.ComponentModel;

namespace Liber.Forms;

public class IntervalView
{
    private DateTime _started = new DateTime(DateTime.Today.Year, 1, 1);
    private DateTime _posted = DateTime.Today;

    [LocalizedCategory(nameof(Started))]
    [LocalizedDescription(nameof(Started))]
    [LocalizedDisplayName(nameof(Started))]
    public DateTime Started
    {
        get
        {
            return _started.Date;
        }
        set
        {
            if (value.Date > _posted)
            {
                _posted = value.Date;
            }

            _started = value.Date;
        }
    }

    [LocalizedCategory(nameof(Posted))]
    [LocalizedDescription(nameof(Posted))]
    [LocalizedDisplayName(nameof(Posted))]
    public DateTime Posted
    {
        get
        {
            return _posted.Date;
        }
        set
        {
            if (value < _started)
            {
                _started = value.Date;
            }

            _posted = value.Date;
        }
    }
}
