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
        if (ActiveObjectives.Contains(objective))
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
        CheckObjective();
    }

    public void CheckObjective()
    {
        if (ActiveObjectives == null)
            return;

        for (int i = 0; i < ActiveObjectives.Count; i++)
        {
            if (ActiveObjectives[i].IsAchived())
            {
                ActiveObjectives[i].OnComplete();
            }
        }
        
    }

}
