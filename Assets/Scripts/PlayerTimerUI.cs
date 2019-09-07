using UnityEngine;
using UnityEngine.UI;


public class PlayerTimerUI : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    public void UpdateTime(Player player)
    {
        timerText.text = player.time.ToString() + "s";
    }
}
