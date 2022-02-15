using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : Interactable
{
    public Item item;
    public override void Interact()
    {
        PickItem();
        base.Interact();
    }

    public void PickItem()
    {
        if (InventoryManager.instance.AddItem(item))
            Destroy(gameObject);
        else
            Debug.Log("envanter dolu");

    }
}
