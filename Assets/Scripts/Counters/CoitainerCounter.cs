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
      else
      {
         if (player.HasKitchenObject())
         {
            //player already have kitchen object can't grab more than one   
            if (player.HasKitchenObject())
            {
               //player already have kitchen object can't grab more than one
               if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
               { //player has plate
                  if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                  {
                     player.GetKitchenObject().DestroySelf();
                  }
               }
            }
         }
         else
         {
            GetKitchenObject().SetKitchenObjectParent(player);
         }
      }
   }

}
