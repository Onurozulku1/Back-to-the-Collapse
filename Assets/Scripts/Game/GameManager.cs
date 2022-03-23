using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static Action<bool> PauseGameHandler;

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
