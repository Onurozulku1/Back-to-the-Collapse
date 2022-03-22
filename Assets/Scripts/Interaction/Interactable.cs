using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float InteractRadius = 2;
    private Transform Player;

    private GameManager gameManager;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnEnable()
    {
        gameManager = GameManager.instance;

    }

    private void Start()
    {
        gameManager = GameManager.instance;

    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }

    private void Update()
    {
        StoreInteractable();
    }

    private void StoreInteractable()
    {
        if (gameManager.InteractedObject == this)
        {
            if (Vector3.Distance(Player.position, transform.position) > InteractRadius)
                gameManager.InteractedObject = null;
        }
        else if (gameManager.InteractedObject == null)
        {
            if (Vector3.Distance(Player.position, transform.position) < InteractRadius)
                gameManager.InteractedObject = this;
        }

            

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, .3f);
        Gizmos.DrawSphere(transform.position, InteractRadius);
    }

    private void OnDisable()
    {
        gameManager.InteractedObject = null;

    }

}
