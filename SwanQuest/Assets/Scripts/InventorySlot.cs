using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IInventorySlot
{

    public bool isFull => amount == capacity;
    public bool isEmpty => item == null;

    public IInventoryItem item { get; private set; }
    public Type type => item.type;
    public int amount => isEmpty ? 0 : item.amount;
    public int capacity { get; private set; }

    public void setItem(IInventoryItem item)
    {
        if(!isEmpty)
            return;
        
        this.item = item;
        this.capacity = item.maxItemInInvetorySlot;
    }
    public void clear()
    {
        if(isEmpty)
            return;
        
        item.amount = 0;
        item = null;
    }

}
