using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private ParticleSystem _particleObject;
    [SerializeField] private ParticleSystem _burnedParticleObject;

    private void Start()
    {
        this._stoveCounter.OnStateChange += StoveCounterOnOnStateChange;
    }

    private void StoveCounterOnOnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        switch (e.state)
        {
            case StoveCounter.State.Idle:
                this._stoveOnGameObject.SetActive(false);
                this._particleObject.Stop();
                this._burnedParticleObject.Stop();
                break;
            case StoveCounter.State.Frying:
            case StoveCounter.State.Fried:
                this._stoveOnGameObject.SetActive(true);
                this._particleObject.Play();
                this._burnedParticleObject.Stop();
                break;
            case StoveCounter.State.Burned:
                this._stoveOnGameObject.SetActive(true);
                this._particleObject.Stop();
                this._burnedParticleObject.Play();
                break;
        }
    }
}