using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective : ScriptableObject
{
    public ObjectiveManager Manager;

    public string Title;
    [TextArea] public string Description;
    public bool IsActive;
    public Objective BridgeObjective;

    private void Awake()
    {
    }

    public virtual bool IsAchived()
    {
        return false;
    }

    public virtual void OnComplete()
    {
        Manager = ObjectiveManager.instance;
        Manager.CompleteObjective(this);
    }

}
