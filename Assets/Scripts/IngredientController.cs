using UnityEngine;

public class IngredientController : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredient;

    [SerializeField] Player interacting;

    public object interactObject { get; set; }

    private void Start()
    {
        interactObject = ingredient;
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
