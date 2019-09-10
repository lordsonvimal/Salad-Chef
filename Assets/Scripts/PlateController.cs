using UnityEngine;

public class PlateController : MonoBehaviour, IInteractable
{
    [SerializeField] Plate plate;
    [SerializeField] Player interacting;
    public object interactObject { get; set; }

    private void Start()
    {
        interactObject = plate;
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
