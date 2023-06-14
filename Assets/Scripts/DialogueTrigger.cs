using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;

    void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}
