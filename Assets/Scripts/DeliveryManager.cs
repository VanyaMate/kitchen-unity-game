using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeListSo;

    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer = 0f;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        this._waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        this._spawnRecipeTimer -= Time.deltaTime;
        if (this._spawnRecipeTimer <= 0f)
        {
            this._spawnRecipeTimer = this._spawnRecipeTimerMax;

            if (this._waitingRecipeMax > this._waitingRecipeSOList.Count)
            {
                int randomIndex = Random.Range(0, this._recipeListSo.recipeSOList.Count);
                RecipeSO recipe = this._recipeListSo.recipeSOList[randomIndex];

                Debug.Log($"New delivery: {recipe.recipeName}");
                this._waitingRecipeSOList.Add(recipe);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < this._waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = this._waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingrediendFound = false;
                    foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (kitchenObjectSo == recipeKitchenObjectSO)
                        {
                            ingrediendFound = true;
                            break;
                        }
                    }

                    if (!ingrediendFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    Debug.Log("Correct delivery");
                    this._waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return this._waitingRecipeSOList;
    }
}