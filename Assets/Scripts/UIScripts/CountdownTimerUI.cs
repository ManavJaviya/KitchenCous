using System;
using TMPro;
using UnityEngine;

public class CountdownTimerUI : MonoBehaviour
{
    public static CountdownTimerUI Instance{get; private set;}
    [SerializeField] private TextMeshProUGUI countdownTimer;

    private void Awake()
    {
        Instance = this;   
    }
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        GameMenu.Instance.OnRestart += GameMenu_OnRestart;
        Hide();
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
