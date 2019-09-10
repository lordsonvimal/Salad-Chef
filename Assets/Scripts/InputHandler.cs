using UnityEngine;

[CreateAssetMenu(fileName ="New Input Handler", menuName ="Input Handler")]
public class InputHandler : ScriptableObject
{
    public KeyCode front;
    public KeyCode back;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;
    public KeyCode chop;
}
