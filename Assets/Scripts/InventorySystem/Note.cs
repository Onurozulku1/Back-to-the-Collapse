using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Note")]
public class Note : ScriptableObject
{
    public string Title;
    [TextArea]
    public string Context;
    public string Author;
    public Item[] ItemsWithNote;
    
}
