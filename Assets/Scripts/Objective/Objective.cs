using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective : ScriptableObject
{

    [TextArea] public string Description;
    public bool IsActive;
    public Objective BridgeObjective;

    public Mission ParentMission;



    public virtual bool IsAchived()
    {
        return false;
    }

    public virtual void OnComplete()
    {
        ObjectiveManager.instance.CompleteObjective(this);
        Debug.Log("Objective Completed");
    }

}
