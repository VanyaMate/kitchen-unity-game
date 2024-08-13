using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;


    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter interact");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("BaseCounter interact alternate");
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return this._counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this._kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this._kitchenObject != null;
    }
}