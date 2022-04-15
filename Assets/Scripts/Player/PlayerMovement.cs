using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;

    [SerializeField] float Speed = 10;
    [SerializeField] float gravity = -9.81f;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;


    private Vector3 MovementVector;

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

    private CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(GetComponent<Collider>().bounds.min, groundDistance, groundMask);

        if (isGrounded && MovementVector.y < 0)
        {
            MovementVector.y = -2f;
        }

        MovementVector = ((Camera.main.transform.parent.right * input.moveVector.x) + (Camera.main.transform.parent.forward * input.moveVector.y)).normalized * _speed;
        MovementVector.y += gravity * Time.deltaTime * 10;

        cc.Move(MovementVector* Time.deltaTime);
        if (input.moveVector != Vector2.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cc.velocity, transform.up), 0.2f);

        }
    }

    public bool isCrouching;
    public void Crouch()
    {
        isCrouching = !isCrouching;
        if (isCrouching)
        {
            Debug.Log("Crouching");
        }
    }
}
