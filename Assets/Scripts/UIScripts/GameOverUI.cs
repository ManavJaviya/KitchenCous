using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredNumber;
   private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStatechanged;
        Hide();
    }

    private void GameManager_OnStatechanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredNumber.text = DeliveryManager.Instance.GetSuccessfulRecipeCount().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
