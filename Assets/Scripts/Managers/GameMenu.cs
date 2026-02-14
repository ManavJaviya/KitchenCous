using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu Instance {get; private set;}
    public event EventHandler OnRestart;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainmenuButton;
    [SerializeField] private Button exitButton;
    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;   
    }
    private void Start()
    {
        menuButton.onClick.AddListener(Menu);
        restartButton.onClick.AddListener(RestartButtonPressed);
        exitButton.onClick.AddListener(Exit);
        mainmenuButton.onClick.AddListener(MainMenu);
        Hide();  
    }

    private void Menu()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {    
            Time.timeScale = 0f;
            Debug.Log("Game Pause");
            Show();
        }
        else
        {
            Time.timeScale = 1f;
            Hide();
        }
    }
    private void RestartButtonPressed()
    {
        OnRestart?.Invoke(this, EventArgs.Empty);
        GameManager.Instance.Restart();
        Menu();
        Hide();
    }
    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Exit()
    {
        Application.Quit();
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
