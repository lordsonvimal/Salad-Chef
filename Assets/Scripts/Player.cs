using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : ScriptableObject
{
    public new string name;
    public PlayerState state;
    public int score;
    public int time;
    public float speed;
}
