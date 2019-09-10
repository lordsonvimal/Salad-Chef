using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField]KeyCode pause;
    bool isPaused;

    [SerializeField]GameEvent pauseEvent;
    [SerializeField]GameEvent resumeEvent;
    [SerializeField]GameEvent retryEvent;
    [SerializeField]GameEvent gameOverEvent;

    [SerializeField] PlayerController player1;
    [SerializeField] PlayerController player2;


    private void Start()
    {
        StartCoroutine(SetGameOver());
    }

    // Set Game Over
    IEnumerator SetGameOver()
    {
        yield return new WaitForSeconds(10);
        while(true)
        {
            if (player1.isTimeOut && player2.isTimeOut)
            {
                gameOverEvent.Raise();
                yield break;
            }
            yield return null;
        }
    }

    public void Retry()
    {
        retryEvent.Raise();
        StartCoroutine(SetGameOver());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
