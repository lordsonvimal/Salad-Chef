using UnityEngine;

[CreateAssetMenu(fileName ="New Ingredient", menuName ="Ingredient")]
public class Ingredient : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public float chopTime;
    public float pickUpTime;
}
