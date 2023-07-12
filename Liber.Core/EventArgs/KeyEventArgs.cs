using System;

namespace Liber;

public class KeyEventArgs : EventArgs
{
    public KeyEventArgs(Guid key)
    {
        Key = key;
    }

    public Guid Key { get; }
}
