using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredNumber;
    private GameManager subscribedGameManager;

    private void Start()
    {
        subscribedGameManager = GameManager.Instance;
        if (subscribedGameManager != null)
        {
            subscribedGameManager.OnStateChanged += GameManager_OnStatechanged;
        }
        Hide();
    }

    private void OnDestroy()
    {
        if (subscribedGameManager != null)
        {
            subscribedGameManager.OnStateChanged -= GameManager_OnStatechanged;
        }
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
