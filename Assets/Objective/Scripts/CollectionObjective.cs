using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location Objective", menuName = "New Objective / Collection Based")]
public class CollectionObjective : Objective
{
    public Item[] RequiredItems;
    public override bool IsAchived()
    {

        return false;
    }

    public override void OnComplete()
    {
        base.OnComplete();

    }
}
