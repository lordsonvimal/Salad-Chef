using UnityEngine;

public class IngredientController : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredient;

    [SerializeField] Player interacting;
    [SerializeField] GameEvent ingredientInteractEvent;

    public object interactObject { get; set; }

    private void Start()
    {
        interactObject = ingredient;
    }
    public void Interact()
    {
        ingredientInteractEvent.Raise();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogError(other.transform.name);
        interacting = other.GetComponent<PlayerController>().player;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interacting = null;
    }
}
