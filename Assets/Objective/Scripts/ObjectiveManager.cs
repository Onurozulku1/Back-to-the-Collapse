using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public Objective[] Objectives;
    private List<Objective> ActiveObjectives;

    public static ObjectiveManager instance;
    private void Awake()
    {
        instance = this;

        if (Objectives == null)
            return;

        ActiveObjectives = new List<Objective>();
        foreach (Objective obj in Objectives)
        {
            if (obj.IsActive)
                ActiveObjectives.Add(obj);
        }
    }

    public void CompleteObjective(Objective objective)
    {
        ActiveObjectives.Remove(objective);
        objective.IsActive = false;

        if (objective.BridgeObjective != null)
        {
            objective.BridgeObjective.IsActive = true;
            ActiveObjectives.Add(objective.BridgeObjective);
        }
    }

    private void Update()
    {
        if (ActiveObjectives == null)
            return;

        foreach (Objective item in ActiveObjectives)
        {
            if (item.IsAchived())
            {
                item.OnComplete();
            }
        }
    }

}
