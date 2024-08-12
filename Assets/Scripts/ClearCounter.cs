using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] protected KitchenObjectSO _kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (!this.HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
            }
        }
    }
}