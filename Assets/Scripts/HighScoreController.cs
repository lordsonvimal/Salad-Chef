using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    [SerializeField]HighScoreList highScores;

    [SerializeField]string fileName = "HighScore.json";

    [SerializeField]string filePath = "";

    [SerializeField] List<TMPro.TextMeshProUGUI> highScoresText = new List<TMPro.TextMeshProUGUI>();

    private void Start()
    {
        highScores = new HighScoreList();
        filePath = Path.Combine(Application.dataPath, fileName);
    }

    void SetHighScores()
    {
        string jsonData = File.ReadAllText(filePath);
        highScores = JsonUtility.FromJson<HighScoreList>(jsonData);
    }

    public void CreateDefault()
    {
        highScores = new HighScoreList();
        for (int i = 0; i < 10; i++)
        {
            HighScore score = new HighScore();
            score.name = "NA";
            highScores.highScores.Add(score);
        }
    }

    public void SetHighScores(HighScore score)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
            CreateDefault();
            WriteHighScores();
        }
        SetHighScores();
        foreach (HighScore hs in highScores.highScores)
        {
            if(score.score >= hs.score)
            {
                hs.name = score.name;
                hs.score = score.score;
                break;
            }
        }
        SetHighScoresText();
        WriteHighScores();
    }

    void SetHighScoresText()
    {
        for(int i=0;i<highScores.highScores.Count;i++)
        {
            highScoresText[i].text = highScores.highScores[i].name + ": " + highScores.highScores[i].score.ToString();
        }
    }

    void WriteHighScores()
    {
        var jsonData = JsonUtility.ToJson(highScores);

        File.WriteAllText(filePath, jsonData);
    }
}
