﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : IInventoryItem
{
    public IInventoryItemInfo info { get; }
    public IInventoryItemState state { get; }

    public Type type => GetType();


    public Pepper(IInventoryItemInfo info)
    {
        this.info = info;
        state = new InventoryItemState();
    }
    public IInventoryItem clone()
    {
        var clonedPepper = new Pepper(info);
        clonedPepper.state.amount = state.amount;
        return clonedPepper;
        
    }
}
