using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowablePick : Interactable
{

    private void Start()
    {
        Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>());
    }

    public override void Interact()
    {
        PlayerController.instance.TakeObject(gameObject);
        base.Interact();
    }

    

}
