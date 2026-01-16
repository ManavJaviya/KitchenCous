using UnityEngine;

public class ContainerCounterVisuals : MonoBehaviour
{
    private Animator animator;
    [SerializeField] CoitainerCounter coitainerCounter;
    const string OPEN_CLOSE = "OpenClose";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        coitainerCounter.onPlayerGrabObject += CoitainerCounter_onPlayerGrabObject;
    }
    private void CoitainerCounter_onPlayerGrabObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
