using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;
    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;

    public void Interact()
    {
        if (this._kitchenObject == null)
        {
            Debug.Log("Interact");
            Transform kitchenObjectTransform = Instantiate(this._kitchenObjectSo.prefab, this._counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;
            this._kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            this._kitchenObject.SetClearCounter(this);
        }
        else
        {
            this._kitchenObject.GetClearCounter();
        }
    }
}