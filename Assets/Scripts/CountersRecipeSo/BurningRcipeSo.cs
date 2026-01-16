using UnityEngine;

[CreateAssetMenu()]
public class BurningRcipeSo : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float maxBurnTimer;
}
