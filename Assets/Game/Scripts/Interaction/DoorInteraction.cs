using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : KeyInteraction
{
    bool isLocked = true;
    bool isOpen = false;
    
    public override void Interact()
    {
        base.Interact();

        UnlockDoor();

        OpenDoor();
    }

    private void OpenDoor()
    {
        if (isLocked)
            return;

        isOpen = !isOpen;
    }

    private void UnlockDoor()
    {
        if (!isLocked)
            return;

        if (HasKey())
        {
            isLocked = false;
            Debug.Log("Kilit açýldý");
        }
    }
}
