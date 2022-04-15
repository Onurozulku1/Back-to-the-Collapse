using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;    
    }


    public GameObject ThrowableObject;
    public void TakeObject(GameObject trwObject)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), trwObject.GetComponent<Collider>());

        if (ThrowableObject != null)
        {
            ThrowableObject.transform.position = transform.position;
            ThrowableObject.SetActive(true);

        }

        ThrowableObject = trwObject;
        trwObject.GetComponent<ThrowableNotification>().isLaunched = false;
        ThrowableObject.SetActive(false);

    }
    

    private void OnEnable()
    {
        GameManager.PauseGameAction += (bool isPaused) => enabled = !isPaused;
        GameManager.PauseGameAction += (bool isPaused) => GetComponent<PlayerMovement>().enabled = !isPaused;
        GameManager.PauseGameAction += (bool isPaused) => GetComponent<CharacterController>().enabled = !isPaused;
    }
    private void OnDisable()
    {
        GameManager.PauseGameAction -= (bool isPaused) => enabled = !isPaused;
        GameManager.PauseGameAction -= (bool isPaused) => GetComponent<PlayerMovement>().enabled = !isPaused;
        GameManager.PauseGameAction -= (bool isPaused) => GetComponent<CharacterController>().enabled = !isPaused;


    }
}
