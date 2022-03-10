using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : Interactable
{
    public Item KeyItem;

    public override void Interact()
    {
        base.Interact();

        
    }

    protected bool HasKey()
    {
        return InventoryManager.instance.RemoveItem(KeyItem);
    }
}
