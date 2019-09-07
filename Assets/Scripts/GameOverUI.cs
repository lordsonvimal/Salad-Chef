using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void OnGameOver()
    {
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
