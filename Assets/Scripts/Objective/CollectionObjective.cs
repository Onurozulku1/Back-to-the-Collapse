using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collection Objective", menuName = "New Objective / Collection Based")]
public class CollectionObjective : Objective
{
    public Item[] RequiredItems;
    public override bool IsAchived()
    {
        return CheckReqItems();
    }

    public override void OnComplete()
    {
        base.OnComplete();

    }

    public bool CheckReqItems()
    {
        foreach (Item item in RequiredItems)
        {
            if (!InventoryManager.instance.Items.Contains(item))
            {
                return false;
            }
        }
        return true;
    }


}
