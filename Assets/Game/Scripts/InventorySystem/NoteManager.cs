using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public List<Note> Notes = new List<Note>();

    public static NoteManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddNote(Note note)
    {
        Notes.Add(note);
        foreach (var item in note.ItemsWithNote)
        {
            if (item != null)
            {
                InventoryManager.instance.AddItem(item);
            }
        }
    }
}
