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

    public void HandleItemCollected(int itemID)
    {
        foreach (var questProgress in activatedQuests)
        {
            foreach (var objective in questProgress.objectives)
            {
                if (objective.type == ObjectiveType.CollectItem &&
                    objective.objectiveID == itemID.ToString() &&
                    !objective.IsCompleted)
                {
                    objective.currentAmount++;
                    if (objective.IsCompleted)
                    {
                        AchievementMediator.Instance.OnObjectiveCompleted(objective.objectiveID);
                    }
                }
            }
        }
        questUI.UpdateQuestUI();
    }

    public bool IsQuestCompleted(string questID)
    {
        foreach (QuestProgress progress in activatedQuests)
        {
            if (progress.QuestID == questID && progress.IsCompleted)
                return true;
        }
        return false;
    }
    
    public void HandleQuestCompletion(string questID)
    {
        foreach (QuestProgress progress in activatedQuests)
        {
            if (progress.QuestID == questID && progress.IsCompleted)
            {
                AchievementMediator.Instance.OnObjectiveCompleted(questID);
            }
        }
    }
}