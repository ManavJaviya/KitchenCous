using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour
{
    [SerializeField] private Player player;
    private float footStepTimer;
    private float footStepTimerMax = .2f;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0f)
        {
            if (player.IsWalking())
            {
                float volume = 1f;
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
                footStepTimer = footStepTimerMax;
            }
        }
    }
}
