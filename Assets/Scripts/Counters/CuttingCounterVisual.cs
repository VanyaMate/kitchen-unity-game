using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter _cuttingCounter;
    private Animator _animator;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        this._cuttingCounter.OnCut += this.CuttingCounterOnOnPlayerGrabbedObject;
    }

    private void CuttingCounterOnOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        this._animator.SetTrigger(CUT);
    }
}