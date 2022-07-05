using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public int capacity { get; set;}
    public bool isFull => _slots.All( slot => slot.isFull);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);
        for (int i = 1; i<capacity; i++)
            _slots.Add(new InventorySlot());
    }

    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find( slot => slot.itemType == itemType).type;
    }

    public IInventoryItem[] GetAllItems()
    {
        List<IInventoryItem> allItems = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if(!slot.isEmpty)
                allItems.Add(slot.item);
        }
    }
    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfItem = _slots.FindAll(slot => !slot.isEmpty && slot.type == itemType);

    }

    public IInventoryItem[] GetEquippedItems();
    public int GetItemAmount(Type itemType);

    public bool TryToAdd(object sender, IInventoryItem item);
    public void delete(object sender, Type itemType, int amount = 1);
    public bool HasItem(Type type, out IInventoryItem item);    
}
