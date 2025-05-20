using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    public List<AchievementSO> achievements;

    void Awake() => Instance = this;
    

    public AchievementSO GetAchievementByObjective(string objectiveID)
    {
        return achievements.Find(a => a.objectiveID == objectiveID);
    }
}