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
        GameManager.PauseGameHandler += (bool isPaused) => enabled = !isPaused;
        GameManager.PauseGameHandler += (bool isPaused) => GetComponent<PlayerMovement>().enabled = !isPaused;
        GameManager.PauseGameHandler += (bool isPaused) => GetComponent<Rigidbody>().useGravity = !isPaused;
        GameManager.PauseGameHandler += (bool isPaused) => GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void OnDisable()
    {
        GameManager.PauseGameHandler -= (bool isPaused) => enabled = !isPaused;
        GameManager.PauseGameHandler -= (bool isPaused) => GetComponent<PlayerMovement>().enabled = !isPaused;
        GameManager.PauseGameHandler -= (bool isPaused) => GetComponent<Rigidbody>().useGravity = !isPaused;
        GameManager.PauseGameHandler -= (bool isPaused) => GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
}
