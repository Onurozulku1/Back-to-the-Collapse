using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float InteractRadius = 2;
    protected Transform Player;

    private GameManager gameManager;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameManager.instance;

    }

    public virtual void Interact()
    {
        
    }

    private void Update()
    {
        StoreInteractable();
    }

    private void StoreInteractable()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
            return;
        }
       

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
        if (gameManager!=null)
            gameManager.InteractedObject = null;

    }

}
