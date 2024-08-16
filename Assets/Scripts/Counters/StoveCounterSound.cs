using System;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    private AudioSource _audioSource;

    private void Awake()
    {
        this._audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        this._stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Burned;
        if (playSound)
        {
            this._audioSource.Play();
        }
        else
        {
            this._audioSource.Pause();
        }
    }
}