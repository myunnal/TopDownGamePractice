using System.IO;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    [Tooltip("StartScene")]
    private string saveLocation;
    private InventoryController inventoryController;
    public string mainMenuSceneName = "StartScene";

    [System.Obsolete]
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindObjectOfType<InventoryController>();
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundry = FindAnyObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    [System.Obsolete]
    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

            FindObjectOfType<CinemachineConfiner2D>().BoundingShape2D = GameObject.Find(saveData.mapBoundry).GetComponent<PolygonCollider2D>();

            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        else
        {
            SaveGame();
        }
    }

    public void ExitToMainMenu()
    {

        SaveGame();
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
