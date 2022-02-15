using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] GameObject slots;

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
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                itemSlots[i].item = _item;
                return true;
            }
            else
            {
                if (i == itemSlots.Length - 1)
                    return false;
            }
        }
        return false;
    }
}
