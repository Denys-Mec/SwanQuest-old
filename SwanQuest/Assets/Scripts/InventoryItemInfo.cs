using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "Gameplay/Items/Create New ItemInfo")]
public class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _descriprion;
    [SerializeField] private int _maxItemInInvetorySlot;
    [SerializeField] private Sprite _spriteIcon;


    public string id => _id;
    public string title => _title;
    public string descriprion => _descriprion;
    public int maxItemInInvetorySlot => _maxItemInInvetorySlot;
    public Sprite spriteIcon => _spriteIcon;

}
