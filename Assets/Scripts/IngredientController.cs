using UnityEngine;

public class IngredientController : MonoBehaviour
{
    [SerializeField]private Ingredient ingredient;
    

    public Ingredient GetIngredient()
    {
        return ingredient;
    }

}
