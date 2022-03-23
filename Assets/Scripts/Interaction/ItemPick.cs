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
        UiManager.instance.DisplayNotificationText(item.Name);

    }

    public void PickItem()
    {
        if (!InventoryManager.instance.AddItem(item))
        {
            Debug.Log("Envanter Dolu");
        }
        Destroy(gameObject);

    }
}
