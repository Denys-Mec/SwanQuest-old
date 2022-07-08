using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public event Action<object, Type, int> OnInventoryItemRemovedEvent;
    public event Action<object> OnInventoryStateChangedEvent;


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
        return _slots.Find( slot => slot.type == itemType).item;
    }

    public IInventoryItem[] GetAllItems()
    {
        List<IInventoryItem> allItems = new List<IInventoryItem>();
        foreach (var slot in _slots)
        {
            if(!slot.isEmpty)
                allItems.Add(slot.item);
        }

        return allItems.ToArray();
    }
    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfItem = _slots.FindAll(slot => !slot.isEmpty && slot.type == itemType);
        foreach(var slot in slotsOfItem)
        {
            allItemsOfType.Add(slot.item);
        }
        return allItemsOfType.ToArray();

    }

    public IInventoryItem[] GetEquippedItems()
    {
        var allEquippedItems = new List<IInventoryItem>();
        var equippedSlots = _slots.FindAll(slot => !slot.isEmpty && slot.item.state.isEquipped);
        foreach(var slot in equippedSlots)
        {
            allEquippedItems.Add(slot.item);
        }
        return allEquippedItems.ToArray();
    }


    public int GetItemAmount(Type itemType)
    {
        var amount = 0;
        var slotsOfItem = _slots.FindAll(slot => !slot.isEmpty && slot.type == itemType);
        foreach(var slot in slotsOfItem)
        {
            amount += slot.amount;
        }
        return amount;
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotWithSameItemButNotEmpty = _slots.Find( slot => !slot.isEmpty &&
                                                        slot.type == item.type &&
                                                        !slot.isFull);
        if(slotWithSameItemButNotEmpty != null)
            return TryToAddToSlot(sender, slotWithSameItemButNotEmpty, item);
        
        var emptySlot = _slots.Find(slot => slot.isEmpty);
        if(emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);

        Debug.Log($"Cannot add a item ({item.type}), amount ({item.state.amount}), " + 
                    $"because there is no place for that.");

        return false;
    }

    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.state.amount <= item.info.maxItemInInvetorySlot;
        var amountToAdd = fits 
            ? item.state.amount
            : item.info.maxItemInInvetorySlot - slot.amount;
        var amountLeft = item.state.amount - amountToAdd;
        var clonedItem = item.clone();
        clonedItem.state.amount = amountToAdd;

        if(slot.isEmpty)
            slot.setItem(clonedItem);
        else
            slot.item.state.amount += amountToAdd;

        Debug.Log($"Item added to inventory. Item: {item.type}, amount: {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
        OnInventoryStateChangedEvent?.Invoke(sender);

        if(amountLeft <= 0)
            return true;
        
        item.state.amount = amountLeft;
        return TryToAdd(sender, item);
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if(fromSlot.isEmpty)
            return;

        if(toSlot.isFull)
            return;
        
        if(!toSlot.isEmpty && toSlot.type != fromSlot.type)
            return;

        var slotCapacity = fromSlot.capacity;
        var fits = fromSlot.amount + toSlot.amount <= slotCapacity;
        var amountToAdd = fits ? fromSlot.amount : slotCapacity - toSlot.amount;
        var amountLeft = fromSlot.amount - amountToAdd;

        if(toSlot.isEmpty)
        {
            toSlot.setItem(fromSlot.item);
            fromSlot.clear();
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        toSlot.item.state.amount += amountToAdd;
        if(fits)
            fromSlot.clear();
        else
            fromSlot.item.state.amount = amountLeft;
        OnInventoryStateChangedEvent?.Invoke(sender);


    }

    public void delete(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);
        if(slotsWithItem.Length == 0)
            return;

        var amountToRemove = amount;
        var count = slotsWithItem.Length;

        for(int i = count - 1; i>=0; i--)
        {
            var slot = slotsWithItem[i];
            if(slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if(slot.amount <= 0)
                    slot.clear();

                Debug.Log($"Item removed from inventory. Item: {itemType}, amount: {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);
                break;
            }


            var amountRemoved = slot.amount;

            amountToRemove -= slot.amount;
            slot.clear(); 
            Debug.Log($"Item removed from inventory. Item: {itemType}, amount: {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }
    }

    public bool HasItem(Type type, out IInventoryItem item)
    {
        item = GetItem(type);
        return item != null;
    } 

    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty &&
                                slot.type == itemType).ToArray();
    }

    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }
}
