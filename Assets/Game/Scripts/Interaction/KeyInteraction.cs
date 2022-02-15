using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : Interactable
{
    public Item KeyItem;

    public override void Interact()
    {
        base.Interact();

        if (InventoryManager.instance.CheckItem(KeyItem))
        {
            Debug.Log("gerekli item mevcut");
        }
    }
}
