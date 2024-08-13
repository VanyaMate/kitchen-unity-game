using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    [SerializeField] private CuttingRecipeSO[] _cuttingRecipes;

    private int _cuttingProgress = 0;

    public override void Interact(Player player)
    {
        if (!this.HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                this._cuttingProgress = 0;
                player.GetKitchenObject().SetKitchenObjectParent(this);

                CuttingRecipeSO recipe = this.GetCuttingRecipeSOWithInput(this.GetKitchenObject().GetKitchenObjectSo());

                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                {
                    progressNormalized = recipe ? (float)this._cuttingProgress / recipe.cuttingProgresMax : 0
                });
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                // maybe swap
            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                {
                    progressNormalized = 0
                });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (this.HasKitchenObject())
        {
            CuttingRecipeSO recipe = this.GetCuttingRecipeSOWithInput(this.GetKitchenObject().GetKitchenObjectSo());

            if (recipe)
            {
                this._cuttingProgress += 1;

                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                {
                    progressNormalized = recipe ? (float)this._cuttingProgress / recipe.cuttingProgresMax : 0
                });

                OnCut?.Invoke(this, EventArgs.Empty);

                if (this._cuttingProgress >= recipe.cuttingProgresMax)
                {
                    KitchenObjectSO output = recipe.output;
                    this.GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(output, this);
                }
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        CuttingRecipeSO recipe = GetCuttingRecipeSOWithInput(input);
        if (recipe != null)
        {
            return recipe.output;
        }

        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipe in this._cuttingRecipes)
        {
            if (cuttingRecipe.input == input)
            {
                return cuttingRecipe;
            }
        }

        return null;
    }
}