using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BoosterType
{
    None=0,
    Speed=1,
    Time=2,
    Score=3
}

public class BoosterCreator : MonoBehaviour
{
    [SerializeField]Clamp clamp;
    [SerializeField]List<Booster> boosters = new List<Booster>();
    [SerializeField] List<BoosterController> boosterControllers = new List<BoosterController>();
    BoosterType GetRandomBooster()
    {
        int rand = Random.Range(0, 4);

        switch(rand)
        {
            case 0:
                return BoosterType.None;
            case 1:
                return BoosterType.Speed;
            case 2:
                return BoosterType.Time;
            case 3:
                return BoosterType.Score;
            default:
                return BoosterType.None;
        }

    }

    int GetIndexOfBooster(BoosterType type)
    {
        switch(type)
        {
            case BoosterType.Speed:
                return 0;
            case BoosterType.Time:
                return 1;
            case BoosterType.Score:
                return 2;
            default:
                return 0;
        }
    }

    public void CreateBooster(Player player)
    {
        BoosterType type = GetRandomBooster();
        if(type != BoosterType.None)
        {
            GameObject go = new GameObject("Booster");
            float randX = Random.Range(clamp.min.x, clamp.max.x);
            float randY = Random.Range(clamp.min.y, clamp.max.y);
            go.transform.position = new Vector3(randX, randY, 0);
            SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = boosters[GetIndexOfBooster(type)].sprite;
            renderer.sortingOrder = 7;
            BoxCollider2D collider = go.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            BoosterController controller = go.AddComponent<BoosterController>();
            //go.layer = layer;
            controller.interacting = player;
            controller.boosterType = type;
            boosterControllers.Add(controller);
            boosterControllers = boosterControllers.Where(item => item != null).ToList();
        }
    }

    public void Reset()
    {
        foreach(BoosterController controller in boosterControllers)
        if(controller != null)
        {
            controller.Destroy();
        }
        boosterControllers.Clear();
    }
}
