using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : Interactable
{
    public Item KeyItem;

    public override void Interact()
    {
        base.Interact();
        UiManager.instance.DisplayNotificationText("You used: " + KeyItem.Name);
        
    }

    protected bool HasKey()
    {
        return InventoryManager.instance.RemoveItem(KeyItem);
    }
}
