using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public InputHandler inputHandler;
    public Player player;
    public GameEvent timerEvent;
    private new Rigidbody2D rigidbody2D;
    public bool isEnabled;
    public bool isTimeOut;
    Vector2 pos;
    float speed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Restart();
    }

    private void Update()
    {
        if (isEnabled)
        {
            Move();
        }
    }

    public void Restart()
    {
        player.time = 100;
        player.score = 0;
        isEnabled = true;
        isTimeOut = false;
        StartCoroutine(StartTimer());
        timerEvent.Raise();
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            player.time -= 1;
            timerEvent.Raise();
            if (player.time <= 0)
            {
                isEnabled = false;
                isTimeOut = true;
                break;
            }
        }
    }

    private void Move()
    {
        rigidbody2D.angularVelocity = 0;
        rigidbody2D.velocity = Vector2.zero;
        speed = player.speed * Time.smoothDeltaTime;
        pos = transform.position;

        if (Input.GetKey(inputHandler.front))
        {
            pos = pos + (Vector2.up * speed);
            MovePosition(pos);
        }
        if (Input.GetKey(inputHandler.back))
        {
            pos = pos + (Vector2.down * speed);
            MovePosition(pos);
        }
        if (Input.GetKey(inputHandler.left))
        {
            pos = pos + (Vector2.left * speed);
            MovePosition(pos);
        }
        if (Input.GetKey(inputHandler.right))
        {
            pos = pos + (Vector2.right * speed);
            MovePosition(pos);
        }
    }

    private void MovePosition(Vector2 position)
    {
        rigidbody2D.MovePosition(position);
    }
}
