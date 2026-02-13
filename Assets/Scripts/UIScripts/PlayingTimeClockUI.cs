using UnityEngine;
using UnityEngine.UI;

public class PlayingTimeClockUI : MonoBehaviour
{
    [SerializeField] private Image clockImage;

    private void Update()
    {
        clockImage.fillAmount = GameManager.Instance.GetPlayingTimerNormalized();
    }
}
