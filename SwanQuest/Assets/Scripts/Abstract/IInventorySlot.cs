using System;
using System.Collections;
using System.Collections.Generic;


public interface IInventorySlot
{
    bool isFull { get; }
    bool isEmpty { get; }

    IInventoryItem item { get; }
    Type type { get; }
    int amount { get; }
    int capacity { get; }

    void setItem(IInventoryItem item);
    void clear();
}