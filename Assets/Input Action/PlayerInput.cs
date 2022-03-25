using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 moveVector;

    void OnMovement(InputValue movementInput)
    {
        moveVector = movementInput.Get<Vector2>();
    }

    void OnInteract(InputValue interactInput)
    {
        if (GameManager.instance.InteractedObject != null)
            GameManager.instance.InteractedObject.Interact();

    }

    void OnCrouch(InputValue crouchInput)
    {
        GetComponent<PlayerMovement>().Crouch();

    }

    void OnToggleInventory(InputValue inventoryInput)
    {
        UiManager.instance.inventoryPanel.SetActive(!UiManager.instance.inventoryPanel.activeSelf);
    }

    //UI Input

    void OnBack(InputValue backInput)
    {
        GameManager.instance.BackAndPauseControl();
    }

    void OnNextDialogue(InputValue nextInput)
    {
        if (TutorialManager.instance.tutorialPanel.activeSelf)
        {
            TutorialManager.instance.NextTutorial();
        }
        else
        {
            DialogueManager.instance.DisplayNextSentence();

        }
    }


    public Transform CameraParent;
    void OnRotateCamera(InputValue mouseDeltaInput)
    {
        float rotateValue = mouseDeltaInput.Get<Vector2>().x;
        CameraParent.Rotate(Vector3.up, rotateValue * Time.deltaTime * 10);
        
    }
}
