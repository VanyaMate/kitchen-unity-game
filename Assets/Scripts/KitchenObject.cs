using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;

    private ClearCounter _clearCounter;

    public KitchenObjectSO GetKitchenObjectSo()
    {
        return this._kitchenObjectSo;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        this._clearCounter = clearCounter;
    }

    public ClearCounter GetClearCounter()
    {
        return this._clearCounter;
    }
}