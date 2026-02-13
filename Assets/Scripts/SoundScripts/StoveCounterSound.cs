using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.onStateChange += StoveCounter_onStateChange;
    }

    private void StoveCounter_onStateChange(object sender, StoveCounter.onStateChangeEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Fryinng || e.state == StoveCounter.State.Fried;

        if(playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

}
