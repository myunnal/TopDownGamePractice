using UnityEngine;

public class AchievementMediator : MonoBehaviour
{
    public static AchievementMediator Instance { get; private set; }

    [SerializeField] private AchievementUI achievementUI;
    [SerializeField] private AudioSource achievementSound;

    void Awake() => Instance = this;

    public void OnObjectiveCompleted(string objectiveID)
    {
        AchievementSO achievement = AchievementManager.Instance.GetAchievementByObjective(objectiveID);
        
        if (achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            achievementUI.ShowAchievementBanner(achievement);
            achievementSound.Play();
            achievementUI.UpdateAchievementMenu();
        }
    }
}