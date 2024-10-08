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

    public void DestroySelf()
    {
        this._kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent parent)
    {
        KitchenObject kitchenObject = Instantiate(kitchenObjectSo.prefab)
            .GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(parent);

        return kitchenObject;
    }
}