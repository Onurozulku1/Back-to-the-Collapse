using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //menzilinde olduðumuz interact objesini anlýk olarak tutuyor ve input gelince tuttuðu objenin interact metodunu çaðýrýyor.
    public Interactable InteractedObject; 


    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion
}
