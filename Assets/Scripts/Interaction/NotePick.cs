public class NotePick : Interactable
{
    public Note Note;

    public override void Interact()
    {
        base.Interact();
        PickNote();
        UiManager.instance.ShowNotification("New Note added to your journal");
    }

    public void PickNote()
    {
        NoteManager.instance.AddNote(Note);
        Destroy(gameObject);
        GameManager.PauseGameHandler?.Invoke(true);

    }


}
