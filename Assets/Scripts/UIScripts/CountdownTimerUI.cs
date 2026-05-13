using System;
using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    public static CountdownTimerUI Instance{get; private set;}
    [SerializeField] private TextMeshProUGUI countdownTimer;

    private GameManager subscribedGameManager;
    private GameMenu subscribedGameMenu;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one CountdownTimerUI in scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;   
    }
    private void Start()
    {
        subscribedGameManager = GameManager.Instance;
        if (subscribedGameManager != null)
        {
            subscribedGameManager.OnStateChanged += GameManager_OnStateChanged;
        }
        subscribedGameMenu = GameMenu.Instance;
        if (subscribedGameMenu != null)
        {
            subscribedGameMenu.OnRestart += GameMenu_OnRestart;
        }
        Hide();
    }

    private void OnDestroy()
    {
        if (subscribedGameManager != null)
        {
            subscribedGameManager.OnStateChanged -= GameManager_OnStateChanged;
        }
        if (subscribedGameMenu != null)
        {
            subscribedGameMenu.OnRestart -= GameMenu_OnRestart;
        }
    }

    private void GameMenu_OnRestart(object sender, EventArgs e)
    {
        Show();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if(GameManager.Instance.IsContdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Update()
    {
        countdownTimer.text = Mathf.Ceil(GameManager.Instance.GetCountdoenTimer()).ToString();
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
