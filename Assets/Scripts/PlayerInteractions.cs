using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInteractions : MonoBehaviour, IInteractable
{
    [SerializeField] private Player player;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private List<Ingredient> ingredientsPicked = new List<Ingredient>();
    [SerializeField] private List<Ingredient> ingredientsChopped = new List<Ingredient>();
    [SerializeField] private List<Ingredient> deliveryPicked = new List<Ingredient>();
    [SerializeField] IInteractable interactable;
    [SerializeField] PlayerController playerController;
    [SerializeField] bool canPick;
    [SerializeField] bool canChop;
    [SerializeField] bool canDeliver;
    public object interactObject { get; set; }
    private void Start()
    {
        interactObject = this;
        canPick = true;
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(inputHandler.interact))
        {
            if(playerController.isEnabled)
            {
                if (interactable != null)
                    Interact();
                else
                    Debug.LogError("No interactions Found");
            }
        }
    }
    public void Interact()
    {
        if (typeof(Ingredient) == interactable.interactObject.GetType())
        {
            OnIngredientInteracted((Ingredient)interactable.interactObject);
        }
        else if (typeof(ChopBoard) == interactable.interactObject.GetType())
        {
            OnChopBoardInteracted((ChopBoard)interactable.interactObject);
        }
        if (typeof(Plate) == interactable.interactObject.GetType())
        {
            OnPlateInteracted((Plate)interactable.interactObject);
        }
        interactable.Interact();
    }

    void OnIngredientInteracted(Ingredient ingredient)
    {
        if (ingredientsPicked.Contains(ingredient))
        {
            ingredientsPicked.Remove(ingredient);
        }
        else if (ingredientsPicked.Count > 2)
        {
            Debug.LogError("Cannot Collect More than 2 Ingredients");
            return;
        }
        else
        {
            ingredientsPicked.Add(ingredient);
        }
    }

    void OnChopBoardInteracted(ChopBoard board)
    {
        if(ingredientsPicked.Count == 0)
        {
            Debug.LogError("No Ingredient is Picked");
            return;
        }
        else if(ingredientsPicked.Count > 0)
        {

        }
    }

    void OnPlateInteracted(Plate plate)
    {
        
    }

    void OnCustomerInteracted(Customer cust)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.GetComponent<IInteractable>() != null)
        {
            interactable = collision.GetComponent<IInteractable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = null;
    }
}
