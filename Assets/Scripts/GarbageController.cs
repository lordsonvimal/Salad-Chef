using UnityEngine;

public class GarbageController : MonoBehaviour, IInteractable
{
    [SerializeField] GarbageBin bin;
    [SerializeField] Player interacting;
    public object interactObject { get; set; }

    private void Start()
    {
        interactObject = bin;
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
