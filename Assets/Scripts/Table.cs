using UnityEngine;


[CreateAssetMenu(fileName ="New Table", menuName ="Table")]
public class Table: ScriptableObject
{
    public Customer customer;
    public bool isOccupied;
    public int tableNo;
}
