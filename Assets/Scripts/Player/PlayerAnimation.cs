using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private PlayerMovement pm;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);
        animator.SetBool("Crouching", pm.isCrouching);
    }


}
