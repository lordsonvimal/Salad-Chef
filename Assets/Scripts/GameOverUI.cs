using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]Player player1;
    [SerializeField]Player player2;
    [SerializeField] HighScoreController highScoreController;
    [SerializeField] TMPro.TextMeshProUGUI winner;

    public void OnGameOver()
    {
        gameObject.SetActive(true);
        HighScore score = new HighScore();
        if (player1.score == player2.score)
            winner.text = "It is a Draw";
        else if (player1.score > player2.score)
        {
            winner.text = "Winner is " + player1.name;
            score.name = player1.name;
            score.score = player1.score;
        }
        else
        {
            winner.text = "Winner is " + player2.name;
            score.name = player1.name;
            score.score = player1.score;
        }
        highScoreController.SetHighScores(score);
    }

    // Close Panel
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
