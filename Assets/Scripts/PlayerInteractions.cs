using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private List<Ingredient> ingredientsPicked = new List<Ingredient>();
    [SerializeField] private List<Ingredient> ingredientsInChopBoard = new List<Ingredient>();
    [SerializeField] private List<Ingredient> ingredientsChopped = new List<Ingredient>();
    [SerializeField] private Ingredient ingredientPlacedInPlate;
    [SerializeField] private List<Ingredient> ingredientsDeliveryPicked = new List<Ingredient>();
    [SerializeField] IInteractable interactable;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMPro.TextMeshProUGUI messageBox;
    [SerializeField] TMPro.TextMeshProUGUI hudText;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] TMPro.TextMeshProUGUI chopBoardText;
    [SerializeField] TMPro.TextMeshProUGUI plateText;
    [SerializeField] CustomerController customerController;
    [SerializeField] BoosterCreator boosterCreator;
    [SerializeField] Canvas interactionCanvas;

    // Reset On Retry
    public void Reset()
    {
        ingredientsPicked.Clear();
        ingredientsInChopBoard.Clear();
        ingredientsChopped.Clear();
        ingredientPlacedInPlate = null;
        ingredientsDeliveryPicked.Clear();
        scoreText.text = "0";
        hudText.text = "";
        chopBoardText.text = "";
        plateText.text = "";
        UpdateHUDText();
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        customerController = FindObjectOfType<CustomerController>();
        Reset();
    }

    private void Update()
    {
        if(interactable != null)
        {
            interactionCanvas.gameObject.SetActive(true);
        }
        else
        {
            interactionCanvas.gameObject.SetActive(false);
        }
        if (Input.GetKeyUp(inputHandler.interact))
        {
            if (playerController.isEnabled)
            {
                Interact();
            }
        }
        if (Input.GetKeyUp(inputHandler.chop))
        {
            StartCoroutine(OnChop());
        }
    }

    // Interact with interactable types
    public void Interact()
    {
        if (interactable != null)
        {
            if (typeof(Ingredient) == interactable.interactObject.GetType())
            {
                OnIngredientInteracted((Ingredient)interactable.interactObject);
            }
            else if (typeof(ChopBoard) == interactable.interactObject.GetType())
            {
                OnChopBoardInteracted((ChopBoard)interactable.interactObject);
            }
            else if (typeof(Plate) == interactable.interactObject.GetType())
            {
                OnPlateInteracted((Plate)interactable.interactObject);
            }
            else if (typeof(Table) == interactable.interactObject.GetType())
            {
                OnCustomerInteracted((Table)interactable.interactObject);
            }
            else if (typeof(GarbageBin) == interactable.interactObject.GetType())
            {
                OnGarbageBinInteracted((GarbageBin)interactable.interactObject);
            }
        }
        else
            messageBox.text = "No Interactions Available";
    }

    // Chop in chop Board
    IEnumerator OnChop()
    {
        if (interactable != null)
        {
            if (ingredientsChopped.Count > 0 && ingredientsInChopBoard.Count == 0)
                messageBox.text = "Already Chopped, Pick up the Combination";
            if (ingredientsInChopBoard.Count > 0)
            {
                messageBox.text = "Chopping";
                playerController.isEnabled = false;
                foreach (Ingredient ing in ingredientsInChopBoard)
                {
                    yield return new WaitForSeconds(ing.chopTime);
                    ingredientsChopped.Add(ing);
                }
                ingredientsInChopBoard.Clear();
                messageBox.text = "Chopping Done";
                playerController.isEnabled = true;
                yield break;
            }
            else
                messageBox.text = "Cannot Chop";
        }
        else
            messageBox.text = "No Interactions Available";
    }

    // Interacting with an Ingredient
    void OnIngredientInteracted(Ingredient ingredient)
    {
        if(ingredientsDeliveryPicked.Count > 0)
        {
            return;
        }
        if (ingredientsPicked.Count > 2)
        {
            StartCoroutine(UpdateMessageBox("Cannot Pick More than 2"));
            return;
        }
        if (ingredientsPicked.Contains(ingredient))
        {
            ingredientsPicked.Remove(ingredient);
        }
        else
        {
            if (ingredientsPicked.Count < 2)
            {
                ingredientsPicked.Add(ingredient);
            }
        }
        UpdateHUDText();
    }

    void UpdateHUDText()
    {
        hudText.text = "";
        if(ingredientsDeliveryPicked.Count > 0)
        {
            foreach(Ingredient ing in ingredientsDeliveryPicked)
            {
                hudText.text += ing.name + "\n";
            }
        }
        else if(ingredientsPicked.Count > 0)
        {
            foreach(Ingredient ing in ingredientsPicked)
            {
                hudText.text += ing.name + "\n";
            }
        }
    }

    // Interacting with Chop Board
    void OnChopBoardInteracted(ChopBoard board)
    {
        if (board.player != player)
        {
            StartCoroutine(UpdateMessageBox("Wrong Board Interacted"));
            return;
        }
        
        if (ingredientsDeliveryPicked.Count > 0)
        {
            StartCoroutine(UpdateMessageBox("Deliver and then Interact"));
            return;
        }
        if (ingredientsChopped.Count > 0 && ingredientsPicked.Count == 0)
        {
            if(ingredientsInChopBoard.Count>0)
            {
                StartCoroutine(UpdateMessageBox("Chop and Pick"));
                return;
            }
            ingredientsDeliveryPicked = new List<Ingredient>(ingredientsChopped);
            ingredientsChopped.Clear();
        }
        else if (ingredientsPicked.Count > 0)
        {
            if (ingredientsInChopBoard.Count + ingredientsChopped.Count == 3)
            {
                StartCoroutine(UpdateMessageBox("Cannot Place More than 3"));
                return;
            }
            ingredientsInChopBoard.Add(ingredientsPicked[0]);
            ingredientsPicked.RemoveAt(0);
            StartCoroutine(UpdateMessageBox("Added ingredient to chop Board"));
        }
        else
        {
            StartCoroutine(UpdateMessageBox("No Ingredients Picked"));
            return;
        }
        UpdateChopBoardText();
        UpdateHUDText();
    }

    void UpdateChopBoardText()
    {
        chopBoardText.text = "";
        if (ingredientsChopped.Count > 0)
        {
            foreach (Ingredient ing in ingredientsChopped)
            {
                chopBoardText.text += ing.name+"\n"; 
            }
        }
        if(ingredientsInChopBoard.Count > 0)
        {
            foreach (Ingredient ing in ingredientsInChopBoard)
            {
                chopBoardText.text += ing.name + "\n";
            }
        }
    }

    IEnumerator UpdateMessageBox(string text)
    {
        messageBox.text = text;
        yield return new WaitForSeconds(2);
        messageBox.text = "";
        yield break;
    }

    // Interact with Plate
    void OnPlateInteracted(Plate plate)
    {
        if(plate.player != player)
        {
            StartCoroutine(UpdateMessageBox("Wrong Plate Interacted"));
            return;
        }
        if (ingredientPlacedInPlate && ingredientsPicked.Count == 2)
        {
            StartCoroutine(UpdateMessageBox("Limit Exceeded.Cannot Pick"));
            return;
        }
        if (ingredientsDeliveryPicked.Count > 0)
        {
            StartCoroutine(UpdateMessageBox("Deliver and Pick"));
            return;
        }
        else
        {
            if (ingredientPlacedInPlate)
            {
                ingredientsPicked.Add(ingredientPlacedInPlate);
                ingredientPlacedInPlate = null;
            }
            else if(ingredientsPicked.Count > 0)
            {
                ingredientPlacedInPlate = ingredientsPicked[ingredientsPicked.Count-1];
                ingredientsPicked.RemoveAt(ingredientsPicked.Count-1);
            }
            UpdatePlateText();
            UpdateHUDText();
        }       
    }

    void UpdatePlateText()
    {
        plateText.text = "";
        if (ingredientPlacedInPlate)
            plateText.text = ingredientPlacedInPlate.name;
    }

    // Interact with Garbage Bin
    void OnGarbageBinInteracted(GarbageBin bin)
    {
        if(ingredientsPicked.Count > 0)
        {
            ingredientsPicked.RemoveAt(ingredientsPicked.Count-1);
            player.score -= 5;
        }
        foreach (Ingredient ing in ingredientsDeliveryPicked)
        {
            player.score -= 10;
        }
        ingredientsDeliveryPicked.Clear();
        UpdateScoreText();
        UpdateHUDText();
    }

    // Interact with customer
    void OnCustomerInteracted(Table table)
    {
        if(ingredientsDeliveryPicked.Count > 0)
        {
            if(table.customer.ingredients.Count == ingredientsDeliveryPicked.Count)
            {
                for(int i=0; i< table.customer.ingredients.Count; i++)
                {
                    if(table.customer.ingredients[i] == ingredientsDeliveryPicked[i])
                    {
                        messageBox.text = "Customer Satisfied";
                        player.score += 50;
                        if ((table.customer.waitTime / table.customer.originalTime) * 100 > 70)
                        {
                            // Spawn Booster
                            boosterCreator.CreateBooster(player);
                        }
                        customerController.CustomerServerSuccessfully(table.tableNo);
                    }
                    else
                    {
                        Debug.LogError("Wrong combinations Served, Penalty given");
                        player.score -= 15;
                    }
                }
                ingredientsDeliveryPicked.Clear();
            }
            else
            {
                Debug.LogError("Wrong combinations Served, Penalty given");
                player.score -= 15;
            }
        }
        else
        {
            Debug.LogError("No combinations picked, Penalty given");
            player.score -= 20;
        }
        UpdateScoreText();
        UpdateHUDText();
    }

    void UpdateScoreText()
    {
        scoreText.text = player.score.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
