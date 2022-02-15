using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] GameObject slots;
    List<Item> Items = new List<Item>();

    private void OnValidate()
    {
        itemSlots = slots.GetComponentsInChildren<ItemSlot>();

    }

    public static InventoryManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

    public bool AddItem(Item _item)
    {
        if (Items.Count < 8)
        {
            Items.Add(_item);
            ResetInventory();
            return true;
        }

        return false;
    }

    void ResetInventory()
    {
        foreach (Item item in Items)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null)
                {
                    itemSlot.item = item;
                    break;
                }
            }
        }
    }
    public bool CheckItem(Item _item)
    {
        foreach (Item item in Items)
        {
            if (item == _item)
            {
                return true;
            }
        }
        return false;
    }
}
