using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;

    [SerializeField] float Speed = 10;
    private float _speed 
    { 
        get
        {
            if (isCrouching)
            {
                return Speed * 0.4f;
            }
            else
            {
                return Speed;
            }
        } 
    }
    public bool isCrouching = false;

    private Rigidbody rb;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(input.moveVector.x,0,input.moveVector.y).normalized * _speed;
        if (input.moveVector != Vector2.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rb.velocity, transform.up), 0.2f);

        }
    }
}
