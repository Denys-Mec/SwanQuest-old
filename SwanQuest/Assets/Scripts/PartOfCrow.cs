using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfCrow : IInventoryItem
{
    public IInventoryItemInfo info { get; }
    public IInventoryItemState state { get; }

    public Type type => GetType();


    public PartOfCrow(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IInventoryItem clone()
    {
        var clonedPart = new PartOfCrow(info);
        clonedPart.state.amount = state.amount;
        return clonedPart;
        
    }
}
