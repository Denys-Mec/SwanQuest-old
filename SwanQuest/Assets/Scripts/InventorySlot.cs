using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IInventorySlot
{

    public bool isFull => !isEmpty && amount == capacity;
    public bool isEmpty => item == null;

    public IInventoryItem item { get; private set; }
    public Type type => item.type;
    public int amount => isEmpty ? 0 : item.state.amount;
    public int capacity { get; private set; }

    public void setItem(IInventoryItem item)
    {
        if(!isEmpty)
            return;
        
        this.item = item;
        this.capacity = item.info.maxItemInInvetorySlot;
    }
    public void clear()
    {
        if(isEmpty)
            return;
        
        item.state.amount = 0;
        item = null;
    }

}
