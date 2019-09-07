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


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(SetGameOver());
    }

    IEnumerator SetGameOver()
    {
        while(true)
        {
            if (player1.isTimeOut && player2.isTimeOut)
            {
                gameOverEvent.Raise();
                yield break;
            }
            else if(player1.isTimeOut)
            {
                if (player2.player.score > player1.player.score)
                {
                    gameOverEvent.Raise();
                    yield break;
                }
            }
            else if(player2.isTimeOut)
            {
                if (player1.player.score > player2.player.score)
                {
                    gameOverEvent.Raise();
                    yield break;
                }
            }
        }
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(pause))
        {
            isPaused = !isPaused;
            if (isPaused)
                pauseEvent.Raise();
            else
                resumeEvent.Raise();
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
