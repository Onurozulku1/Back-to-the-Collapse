using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowablePick : Interactable
{
    public override void Interact()
    {
        PlayerController.instance.TakeObject(gameObject);
        base.Interact();
    }

    

}
