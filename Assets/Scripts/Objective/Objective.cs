using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective : ScriptableObject
{

    public string Title;
    [TextArea] public string Description;
    public bool IsActive;
    public Objective BridgeObjective;


    public virtual bool IsAchived()
    {
        return false;
    }

    public virtual void OnComplete()
    {
        ObjectiveManager.instance.CompleteObjective(this);
    }

}
