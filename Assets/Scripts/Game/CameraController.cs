using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = Player.position;
    }

    private void OnEnable()
    {
        GameManager.PauseGameHandler += (bool isPaused) => enabled = !isPaused;
    }
    private void OnDisable()
    {
        GameManager.PauseGameHandler -= (bool isPaused) => enabled = !isPaused;

    }
    
}
