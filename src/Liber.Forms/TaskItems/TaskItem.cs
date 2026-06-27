// TaskItem.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.TaskItems;

internal abstract class TaskItem
{ 
    public abstract int Priority { get; }
    public abstract string Description { get; }

    public abstract void Begin();

    public override string ToString()
    {
        return Description;
    }
}
