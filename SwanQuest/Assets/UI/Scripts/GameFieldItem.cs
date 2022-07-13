using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameFieldItem : MonoBehaviour, IPointerDownHandler
{
	[SerializeField] private UIInventory uiinventory;
	[SerializeField] private InventoryItemInfo itemInfo;
	//private IInventory inventory;

	
	public void OnPointerDown (PointerEventData pointerEventData)
		
	{	
		Debug.Log("Add part to inventory");
	

		var inventory = uiinventory.getInventory();
	   	IInventoryItem item;
		if(itemInfo.id == "crow")
			item =  new PartOfCrow(itemInfo);
		else
			item = new Pepper(itemInfo);

        item.state.amount = 1;
		inventory.TryToAdd(this, item);
		
	}
}
