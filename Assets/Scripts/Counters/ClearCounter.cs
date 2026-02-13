using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

   public override void Interact(Player player)
   {
      if (!HasKitchenObject())
      {
         if (player.HasKitchenObject())
         {
            player.GetKitchenObject().SetKitchenObjectParent(this);
         }
         else
         {
            // player don't have anything  
         }
      }
      else
      {
         if (player.HasKitchenObject())
         {
            //player already have kitchen object can't grab more than one
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            { //player has plate
               if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
               {
                  GetKitchenObject().DestroySelf();
               }
            }
                else
                { // player does not have any plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
         }
         else
         {  //player is not carring anything
            GetKitchenObject().SetKitchenObjectParent(player);
         }
      }
   }
}
