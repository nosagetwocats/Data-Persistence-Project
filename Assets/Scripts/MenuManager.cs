using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public int HighestScore;
    public string HighestScorePlayerName;

    public InputField PlayerNameInput;
    public string PlayerName;

    public Text BestScorePlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int HighestScore;
        public string HighestScorePlayerName;
    }

    public void SavePlayerAndScore()
    {
        SaveData data = new SaveData();
        data.HighestScore = HighestScore;
        data.HighestScorePlayerName = HighestScorePlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HighestScore = data.HighestScore;
            HighestScorePlayerName = data.HighestScorePlayerName;
            BestScorePlayerName.text = "Best Score : " + HighestScorePlayerName + " : " + HighestScore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerAndScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        PlayerName = PlayerNameInput.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        SavePlayerAndScore();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
        Application.Quit(); // original code to quit Unity
        #endif
    }
}
