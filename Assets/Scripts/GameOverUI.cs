using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]Player player1;
    [SerializeField]Player player2;
    [SerializeField] TMPro.TextMeshProUGUI winner;

    public void OnGameOver()
    {
        gameObject.SetActive(true);
        if (player1.score == player2.score)
            winner.text = "It is a Draw";
        else if (player1.score > player2.score)
            winner.text = "Winner is : "+player1.name;
        else
            winner.text = "Winner is : " + player2.name;

    }

    // Close Panel
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
