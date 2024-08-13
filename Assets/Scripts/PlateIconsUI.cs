using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake()
    {
        this._iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        this._plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        this.UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == this._iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSo in this._plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(this._iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform
                .GetComponent<PlateIconsSingleUI>()
                .SetKitchenObjectSO(kitchenObjectSo);
        }
    }
}