using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    [SerializeField] private Player player;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
