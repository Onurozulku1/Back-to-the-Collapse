using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public Mission[] Missions;
    public List<Mission> ActiveMissions;

    public List<Objective> ActiveObjectives;

    public static ObjectiveManager instance;
    private void Awake()
    {
        instance = this;

        SetObjectives();
        SetParentMissions();

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

        if (IsMissionCompleted(objective))
        {
            objective.ParentMission.IsActive = false;
        }
    }

    private bool IsMissionCompleted(Objective objective)
    {
        foreach (Objective item in objective.ParentMission.objectives)
        {
            if (item.IsActive)
                return false;
        }
        return true;
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

    public void SetObjectives()
    {
        if (Missions != null)
        {
            ActiveMissions = new List<Mission>();
            foreach (Mission mission in Missions)
            {
                if (mission.IsActive)
                {
                    ActiveMissions.Add(mission);

                    if (mission.objectives == null)
                        return;

                    foreach (Objective objective in mission.objectives)
                    {
                        if (objective.IsActive)
                        {
                            ActiveObjectives.Add(objective);
                        }
                    }
                }
            }
        }

    }

    public void SetParentMissions()
    {
        if (Missions != null)
        {
            foreach (Mission mission in Missions)
            {
                foreach (Objective objective in mission.objectives)
                {
                    objective.ParentMission = mission;
                }
            }
        }
        
    }

    private void Update()
    {
        CheckObjective();
    }


}

[System.Serializable]
public struct Mission
{
    public string Title;
    public Objective[] objectives;
    public bool IsActive;
}
