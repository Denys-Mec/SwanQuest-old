using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
   // [SerializeField] private InventoryItemInfo _appleInfo;
    [SerializeField] private InventoryItemInfo _pepperInfo;
    public InventoryWithSlots inventory => tester.inventory;

    private UIInventoryTester tester;

    private void Start()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        tester = new UIInventoryTester(_pepperInfo, uiSlots);
        tester.FillSlots();
	}

	public InventoryWithSlots getInventory()
	{
		return tester.inventory;
	}
}
