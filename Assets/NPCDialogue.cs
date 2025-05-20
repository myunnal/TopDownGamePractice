using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    public string npcName;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public bool[] endDialogueLines;

    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;

    public DialogueChoice[] choices;

    public int questInProgressIndex;
    public int questCompletedIndex;
    public Quest quest;
}

[System.Serializable]

public class DialogueChoice
{
    public int dialogueIndex;
    public string[] choices;
    public int[] nextDialogueIndexes;
    public bool[] givesQuest;
}