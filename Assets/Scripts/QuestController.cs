using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; private set; }
    public List<QuestProgress> activatedQuests = new();
    private QuestUI questUI;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        questUI = FindObjectOfType<QuestUI>();
    }

    public void AcceptQuest(Quest quest)
    {
        if (IsQuestActive(quest.questID)) return;

        activatedQuests.Add(new QuestProgress(quest));
        questUI.UpdateQuestUI();
    }

    public bool IsQuestActive(string questID) => activatedQuests.Exists(q => q.QuestID == questID);
}
