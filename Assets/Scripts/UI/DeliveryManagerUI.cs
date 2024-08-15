using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _recipeTemplate;

    private void Awake()
    {
        this._recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += this.OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeSpawned += this.OnRecipeSpawned;

        this.UpdateVisual();
    }

    private void OnRecipeSpawned(object sender, EventArgs e)
    {
        this.UpdateVisual();
    }

    private void OnRecipeCompleted(object sender, EventArgs e)
    {
        this.UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in this._container)
        {
            if (child == this._recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSo in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(this._recipeTemplate, this._container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSo);
        }
    }
}