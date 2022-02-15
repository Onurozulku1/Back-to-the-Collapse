using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float InteractRadius = 2;
    private Transform Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }

    private void Update()
    {
        if (Vector3.Distance(Player.position, transform.position) < InteractRadius)
        {
            if (GameManager.instance.InteractedObject == null)
            {
                GameManager.instance.InteractedObject = this;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, .3f);
        Gizmos.DrawSphere(transform.position, InteractRadius);
    }


   
}
