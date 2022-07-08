using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItemInfo
{
    string id { get; }
    string title { get; }
    string descriprion { get; }
    int maxItemInInvetorySlot { get; }
    Sprite spriteIcon { get; }

} 
