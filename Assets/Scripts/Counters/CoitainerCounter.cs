using System;
using UnityEngine;

public class CoitainerCounter : BaseCounter
{
   public EventHandler onPlayerGrabObject;
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

   public override void Interact(Player player)
   {
      if (!player.HasKitchenObject())
      {
         KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

         onPlayerGrabObject?.Invoke(this, EventArgs.Empty);
      }
      else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
      {
         if (plateKitchenObject.TryAddIngredient(kitchenObjectSO))
         {
            onPlayerGrabObject?.Invoke(this, EventArgs.Empty);
         }
      }
   }

}
