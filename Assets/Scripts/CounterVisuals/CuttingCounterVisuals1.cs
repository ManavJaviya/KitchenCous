using UnityEngine;

public class CuttingCounterVisuals : MonoBehaviour
{
    private Animator animator;
    [SerializeField] CuttinmgCounter cuttinmgCounter;
    const string CUT = "Cut";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        cuttinmgCounter.OnCut += CuttinmgCounter_onCut;
    }
    private void CuttinmgCounter_onCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
