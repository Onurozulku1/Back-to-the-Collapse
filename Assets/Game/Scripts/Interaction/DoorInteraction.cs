using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : KeyInteraction
{
    bool isLocked = true;
    bool isOpen = false;
    
    public override void Interact()
    {
        base.Interact();

        if (InventoryManager.instance.CheckItem(KeyItem) && isLocked)
        {
            isLocked = false;
        }

        if (!isLocked)
            isOpen = !isOpen;

    }
}
