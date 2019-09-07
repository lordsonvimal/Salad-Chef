using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]private InputHandler inputHandler;
    [SerializeField]private Player player;

    private new Rigidbody2D rigidbody2D;
    Vector2 pos;

    private void Awake()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Interact();
    }

    private void Move()
    {
        var speed = player.speed * Time.smoothDeltaTime;
        pos = transform.position;

        if (Input.GetKey(inputHandler.front))
        {
            pos = pos + (Vector2.up * speed);
            AddForce(pos);
        }
        if (Input.GetKey(inputHandler.back))
        {
            pos = pos + (Vector2.down * speed);
            AddForce(pos);
        }
        if (Input.GetKey(inputHandler.left))
        {
            pos = pos + (Vector2.left * speed);
            AddForce(pos);
        }
        if (Input.GetKey(inputHandler.right))
        {
            pos = pos + (Vector2.right * speed);
            AddForce(pos);
        }
    }

    void AddForce(Vector2 position)
    {
        rigidbody2D.MovePosition(position);
    }

    private void Interact()
    {
        if (Input.GetKey(inputHandler.interact))
        {

        }
    }
}
