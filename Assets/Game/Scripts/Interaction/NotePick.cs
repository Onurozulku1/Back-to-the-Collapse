using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePick : Interactable
{
    public Note Note;

    public override void Interact()
    {
        base.Interact();
        PickNote();

    }

    public void PickNote()
    {
        NoteManager.instance.AddNote(Note);
        Destroy(gameObject);
    }


}
