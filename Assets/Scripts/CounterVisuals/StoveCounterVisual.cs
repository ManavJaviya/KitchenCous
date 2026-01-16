using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particalsGameObject;

    private void Start() {
        stoveCounter.onStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.onStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Fryinng || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particalsGameObject.SetActive(showVisual);
    }
}
