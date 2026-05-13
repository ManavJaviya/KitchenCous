using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public event EventHandler OnStateChanged;
    public event EventHandler OnPause;
    public event EventHandler OnResume;

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 20f;
    private bool isGamePause = false;

    private State state;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one GameManager in scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Update()
    {
        switch(state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;

                    if(waitingToStartTimer < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChanged?.Invoke(this , EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countdownToStartTimer -= Time.deltaTime;
                
                    if(countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this , EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                
                    if(gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this , EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public bool IsContdownToStartActive()
    {
        return state == State.CountDownToStart;
    }
    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public float GetCountdoenTimer()
    {
        return countdownToStartTimer;
    }
    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    public void Restart()
    {
        ClearKitchenWorldForRestart();

        state = State.WaitingToStart;
        waitingToStartTimer = 1f;
        countdownToStartTimer = 3f;
        gamePlayingTimer = gamePlayingTimerMax;
        isGamePause = false;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private static void ClearKitchenWorldForRestart()
    {
        if (Player.Instance != null && Player.Instance.HasKitchenObject())
        {
            Player.Instance.GetKitchenObject().DestroySelf();
        }

        BaseCounter[] counters = UnityEngine.Object.FindObjectsByType<BaseCounter>(
            FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach (BaseCounter counter in counters)
        {
            counter.ClearForRestart();
        }
    }
}
