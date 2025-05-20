using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements/Achievement")]
public class AchievementSO : ScriptableObject
{
    public string achievementID;
    public string title;
    public string description;
    public string objectiveID;
    public bool IsUnlocked;
    
    private void OnEnable()
    {
        IsUnlocked = false; 
    }
}