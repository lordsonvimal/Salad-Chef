using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="New Customer", menuName ="Customer")]
public class Customer:ScriptableObject
{
    public float originalTime;
    public float waitTime;
    public List<Ingredient> ingredients = new List<Ingredient>();
    public List<GameObject> ingredientSpriteObject = new List<GameObject>();
    public int score;
}
