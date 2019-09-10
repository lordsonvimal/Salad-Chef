using UnityEngine;

public class ChopBoardController : MonoBehaviour, IInteractable
{
    [SerializeField] ChopBoard chopBoard;
    [SerializeField] Player interacting;
    public object interactObject { get; set; }

    private void Start()
    {
        interactObject = chopBoard;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        interacting = other.GetComponent<PlayerController>().player;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interacting = null;
    }
}
