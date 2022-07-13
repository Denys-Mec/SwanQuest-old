using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UIInventoryTester : MonoBehaviour
{
 //   private IInventoryItemInfo _appleInfo;
    private IInventoryItemInfo _pepperInfo;
    private UIInventorySlot[] _uiSlots;

    public InventoryWithSlots inventory { get; }

    public UIInventoryTester(IInventoryItemInfo pepperInfo, 
                            UIInventorySlot[] slots)
    {
       // _appleInfo = appleInfo;
        _pepperInfo = pepperInfo;
        _uiSlots = slots;

        inventory = new InventoryWithSlots(8);
        inventory.OnInventoryStateChangedEvent += OnInventoryStateChanged;
    }

    public void FillSlots()
    {
        var allSlots = inventory.GetAllSlots();
        var avaibleSlots = new List<IInventorySlot>(allSlots);

        var filledSlots = 0;
        for(int i=0; i<filledSlots; i++)
        {
            var filledSlot = AddRandomApplesIntoRandomSlot(avaibleSlots);
            avaibleSlots.Remove(filledSlot);

            filledSlot = AddRandomPeppersIntoRandomSlot(avaibleSlots);
            avaibleSlots.Remove(filledSlot);
        }
        SetupInventoryUI(inventory);
    }

    private IInventorySlot AddRandomApplesIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(1, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
   //     var apple = new Apple(_appleInfo);
     //   apple.state.amount = rCount;
       // inventory.TryToAddToSlot(this, rSlot, apple);
        return rSlot;
    }

    private IInventorySlot AddRandomPeppersIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(1, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 4);
        var pepper = new Pepper(_pepperInfo);
        pepper.state.amount = rCount;
        inventory.TryToAddToSlot(this, rSlot, pepper);
        return rSlot;
    }

    private void SetupInventoryUI(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;

        for(int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];

            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }

    }

    private void OnInventoryStateChanged(object sender)
    {
        foreach(var uiSlot in _uiSlots)
        {
            uiSlot.Refresh();
        }
    }

}
