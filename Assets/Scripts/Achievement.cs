[System.Serializable]
public class Achievement
{
    public string achievementID;
    public string title;
    public string description;
    public string objectiveID;
    public bool IsUnlocked { get; private set; }

    public void Unlock() => IsUnlocked = true;
}