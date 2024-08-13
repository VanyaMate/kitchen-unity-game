using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter _containerCounter;
    private Animator _animator;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        this._containerCounter.OnPlayerGrabbedObject += ContainerCounterOnOnPlayerGrabbedObject;
    }

    private void ContainerCounterOnOnPlayerGrabbedObject(object sender, EventArgs e)
    {
        this._animator.SetTrigger(OPEN_CLOSE);
    }
}