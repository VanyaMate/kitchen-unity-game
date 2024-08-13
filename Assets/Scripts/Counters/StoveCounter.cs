using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangeEventArgs> OnStateChange;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;

    public class OnStateChangeEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] _fryingRecipes;

    private State _state = State.Idle;
    private FryingRecipeSO _currentRecipe = null;
    private float _fryTimer = 0;
    private bool _cooked = false;

    private void Start()
    {
        this._state = State.Idle;
    }

    private void Update()
    {
        switch (this._state)
        {
            case State.Idle:
                break;
            case State.Frying:
                this._fryTimer += Time.deltaTime;

                if (this._fryTimer > this._currentRecipe.fryingTimerMax)
                {
                    this.GetKitchenObject().DestroySelf();
                    this._fryTimer = 0;
                    KitchenObject.SpawnKitchenObject(this._currentRecipe.output, this);
                    this._currentRecipe = this.GetFryingRecipeSOWithInput(this._currentRecipe.output);

                    if (this._currentRecipe != null)
                    {
                        this._cooked = true;
                        this._state = State.Frying;
                    }
                    else
                    {
                        this._state = State.Burned;
                    }

                    this.OnStateChange?.Invoke(this, new OnStateChangeEventArgs()
                    {
                        state = this._state
                    });

                    this.OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                    {
                        progressNormalized = 0
                    });
                }
                else
                {
                    this.OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                    {
                        progressNormalized = Mathf.Min(1, this._fryTimer / this._currentRecipe.fryingTimerMax)
                    });
                }

                break;
            case State.Fried:
                break;
            case State.Burned:
                break;
        }
    }

    public override void Interact(Player player)
    {
        if (!this.HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                this._fryTimer = 0;
                KitchenObject playerKitchenObject = player.GetKitchenObject();
                this._currentRecipe = this.GetFryingRecipeSOWithInput(playerKitchenObject.GetKitchenObjectSo());
                playerKitchenObject.SetKitchenObjectParent(this);

                if (this._currentRecipe != null)
                {
                    this._cooked = false;
                    this._state = State.Frying;
                    this.OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                    {
                        progressNormalized = 0
                    });
                }
                else
                {
                    this._state = State.Idle;
                }

                this.OnStateChange?.Invoke(this, new OnStateChangeEventArgs()
                {
                    state = this._state
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
                this._state = State.Idle;

                this.OnStateChange?.Invoke(this, new OnStateChangeEventArgs()
                {
                    state = this._state
                });

                this.OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs()
                {
                    progressNormalized = 0
                });
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        FryingRecipeSO recipe = this.GetFryingRecipeSOWithInput(input);
        if (recipe != null)
        {
            return recipe.output;
        }

        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO fryingRecipe in this._fryingRecipes)
        {
            if (fryingRecipe.input == input)
            {
                return fryingRecipe;
            }
        }

        return null;
    }
}