using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem 
{
   bool isEquipped { get; set; }
   Type type { get; }
   int maxItemInInvetorySlot { get; }
   int amount { get; set; }

   IInventoryItem clone();
}
