using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
