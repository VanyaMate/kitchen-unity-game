using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO _plateKitchenObjectSO;

    private float _spawnPlateTimer;
    private float _spawnPlateTimerMax = 4f;
    private int _platesSpawnedAmount;
    private int _platesSpawnedAmountMax = 4;

    private void Update()
    {
        this._spawnPlateTimer += Time.deltaTime;

        if (this._spawnPlateTimer > this._spawnPlateTimerMax)
        {
            this._spawnPlateTimer = 0;
            if (this._platesSpawnedAmount < this._platesSpawnedAmountMax)
            {
                this._platesSpawnedAmount += 1;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (this._platesSpawnedAmount > 0)
            {
                this._platesSpawnedAmount -= 1;
                KitchenObject.SpawnKitchenObject(this._plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}