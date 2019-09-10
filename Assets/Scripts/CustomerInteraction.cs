using UnityEngine;

public class CustomerInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Table table;
    [SerializeField] Player interacting;
    public object interactObject { get; set; }

    private void Start()
    {
        interactObject = table;
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
