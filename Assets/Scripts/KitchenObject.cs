using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSo()
    {
        return this._kitchenObjectSo;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent clearCounter)
    {
        if (this._kitchenObjectParent != null)
        {
            this._kitchenObjectParent.ClearKitchenObject();
        }

        this._kitchenObjectParent = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent already has a KitchenObject");
            return;
        }

        clearCounter.SetKitchenObject(this);

        this.transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        this.transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this._kitchenObjectParent;
    }
}