using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static Action<bool> PauseGameAction;

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

    private bool PauseGame = false;
    public void BackAndPauseControl()
    {
        if (UiManager.instance.DisplayingWindows())
        {
            UiManager.instance.CloseWindows();
            PauseGameAction?.Invoke(false);
            return;

        }

        PauseGame = !PauseGame;
        PauseGameAction?.Invoke(PauseGame);

        UiManager.instance.pauseMenu.SetActive(PauseGame);
        UiManager.instance.staticTabs.SetActive(!PauseGame);

        if (PauseGame)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

    }

    private void OnDestroy()
    {
        PauseGameAction = null;
    }

}
