using System.Collections.Generic;

// Create and Update High Score Data
[System.Serializable]
public class HighScore
{
    public string name = "NA";
    public int score;
}

[System.Serializable]
public class HighScoreList
{
    public List<HighScore> highScores = new List<HighScore>();
}

