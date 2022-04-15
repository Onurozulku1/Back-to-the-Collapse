using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement pm;
    private CharacterController cc;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        cc = GetComponent<CharacterController>();

    }

    private void Update()
    {
        animator.SetFloat("Speed", cc.velocity.magnitude);
        animator.SetBool("Crouching", pm.isCrouching);
    }


}
