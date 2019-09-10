using UnityEngine;

[CreateAssetMenu(fileName ="New Booster", menuName ="Booster")]
public class Booster : ScriptableObject
{
    public new string name;
    public BoosterType type;
    public Sprite sprite;
}
