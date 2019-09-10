using UnityEngine;
using System.Collections;


public class BoosterController : MonoBehaviour, IInteractable
{
    public Player interacting;
    public BoosterType boosterType;
    public object interactObject { get; set; }

    bool isInteracted;

    Coroutine timerCoroutine;

    private void Start()
    {
        interactObject = this;
    }

    private void OnEnable()
    {
        timerCoroutine = StartCoroutine(StartTimer());
    }

    void SetBoosterAction(Player player, BoosterType type)
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        if (type == BoosterType.Score)
        {
            player.score += 150;
            Destroy();
        }
        else if(type == BoosterType.Speed)
        {
            StartCoroutine(AddSpeed());
        }
        else if(type == BoosterType.Time)
        {
            player.time += 20;
            Destroy();
        }
    }

    IEnumerator AddSpeed()
    {
        StopCoroutine(timerCoroutine);
        this.GetComponent<SpriteRenderer>().enabled = false;
        float actualSpeed = interacting.speed;
        interacting.speed += 5;
        yield return new WaitForSeconds(3);
        interacting.speed = actualSpeed;
        Destroy();
        yield break;
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(5);
        Destroy();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (interacting == other.GetComponent<PlayerController>().player)
        {
            Debug.Log("Found");
            SetBoosterAction(interacting, boosterType);
        }
    }

    public void Destroy()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
}
