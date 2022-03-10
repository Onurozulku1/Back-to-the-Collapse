using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Location Objective", menuName = "New Objective / Location Based")]
public class LocationObjective : Objective
{
    public Vector3 TargetLocation;
    public float TargetDistance = 1;


    public override bool IsAchived()
    {
        if (TargetLocation == null)
        {
            Debug.Log("Targetlocation null!");
            return false;
        }

        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
        return Vector3.Distance(player, TargetLocation) < TargetDistance;
    }

    public override void OnComplete()
    {
        base.OnComplete();
        Debug.Log("Target reached");
      
    }

    public void SetLocation()
    {
        if (Selection.transforms[0] == null)
        {
            Debug.Log("Select transform on scene");
            return;
        }

        TargetLocation = Selection.transforms[0].position;
    }


}

[CustomEditor(typeof(LocationObjective))]
public class TestScriptableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (LocationObjective)target;

        if (GUILayout.Button("Set Location", GUILayout.Height(40)))
        {
            script.SetLocation();
        }

    }
}
