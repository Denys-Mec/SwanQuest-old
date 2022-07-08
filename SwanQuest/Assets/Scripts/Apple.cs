using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : IInventoryItem
{
    public IInventoryItemInfo info { get; }
    public IInventoryItemState state { get; }

    public Type type => GetType();


    public Apple(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IInventoryItem clone()
    {
        var clonedApple = new Apple(info);
        clonedApple.state.amount = state.amount;
        return clonedApple;
        
    }
}
