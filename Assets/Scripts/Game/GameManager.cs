using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static Action<bool> PauseGameHandler;

    //menzilinde oldu�umuz interact objesini anl�k olarak tutuyor ve input gelince tuttu�u objenin interact metodunu �a��r�yor.
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
            PauseGameHandler?.Invoke(false);
            return;

        }

        PauseGame = !PauseGame;
        PauseGameHandler?.Invoke(PauseGame);

        UiManager.instance.pauseMenu.SetActive(PauseGame);
        UiManager.instance.staticTabs.SetActive(!PauseGame);

    }

  
  


}
