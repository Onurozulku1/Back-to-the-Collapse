using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyEvents : MonoBehaviour
{
    public static EnemyEvents instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}

