using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [Header("Banner")]
    public GameObject achievementBanner;
    public TMP_Text bannerTitle;
    public TMP_Text bannerDescription;
    public float bannerDuration = 3f;

    [Header("Menu")]
    public Transform achievementMenuContent;
    public GameObject achievementEntryPrefab;

    public void ShowAchievementBanner(AchievementSO achievement)
    {
        bannerTitle.text = achievement.title;
        bannerDescription.text = achievement.description;
        achievementBanner.SetActive(true);
        Invoke("HideBanner", bannerDuration);
    }

    void HideBanner() => achievementBanner.SetActive(false);

    public void UpdateAchievementMenu()
    {
        // Clear old entries
        foreach (Transform child in achievementMenuContent)
            Destroy(child.gameObject);

        // Create new entries
        foreach (AchievementSO achievement in AchievementManager.Instance.achievements)
        {
            GameObject entry = Instantiate(achievementEntryPrefab, achievementMenuContent);
            entry.GetComponentInChildren<TMP_Text>().text = 
                $"{achievement.title}: {(achievement.IsUnlocked ? "UNLOCKED" : "LOCKED")}";
        }
    }
}