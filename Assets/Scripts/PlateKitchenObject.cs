using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSo;
    }

    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSO;

    private List<KitchenObjectSO> _kitchenObjectsSO;

    private void Awake()
    {
        this._kitchenObjectsSO = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        bool isAdded = this._kitchenObjectsSO.Contains(kitchenObjectSo);
        bool isValid = this._validKitchenObjectSO.Contains(kitchenObjectSo);

        if (!isAdded && isValid)
        {
            this._kitchenObjectsSO.Add(kitchenObjectSo);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs()
            {
                kitchenObjectSo = kitchenObjectSo
            });
            return true;
        }

        return false;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return this._kitchenObjectsSO;
    }
}