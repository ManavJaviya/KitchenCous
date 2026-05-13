using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSO;
   private IKichenObjectParent KitchenObjectParent;


   public KitchenObjectSO GetKitchenObjectSO()
   {
      return kitchenObjectSO;
   }

   public void SetKitchenObjectParent(IKichenObjectParent kitchenObjectPatent)
   {
      if (this.KitchenObjectParent != null)
      {
         // Clear the object from the OLD parent
         this.KitchenObjectParent.ClearKitchenObject();
      }

      if (kitchenObjectPatent.HasKitchenObject())
      {
         KitchenObject existingObject = kitchenObjectPatent.GetKitchenObject();
         // Picking up a plate while already holding an ingredient: move ingredient onto the plate instead of orphaning the held object.
         if (this is PlateKitchenObject plateKitchenObject
             && !existingObject.TryGetPlate(out _)
             && plateKitchenObject.TryAddIngredient(existingObject.GetKitchenObjectSO()))
         {
            existingObject.DestroySelf();
            this.KitchenObjectParent = kitchenObjectPatent;
            kitchenObjectPatent.SetKitchenObjact(this);
            transform.parent = kitchenObjectPatent.GetKitchenFollowTransform();
            transform.localPosition = Vector3.zero;
            return;
         }

         Debug.LogWarning("IKichenObjectParent already has a KitchenObject; could not merge. Destroying incoming object: " + gameObject.name);
         Destroy(gameObject);
         return;
      }

      this.KitchenObjectParent = kitchenObjectPatent;
      kitchenObjectPatent.SetKitchenObjact(this);

      transform.parent = kitchenObjectPatent.GetKitchenFollowTransform();
      transform.localPosition = Vector3.zero;
   }

   public IKichenObjectParent GetKitchenObjectParent()
   {
      return KitchenObjectParent;
   }
   public void DestroySelf()
   {
      KitchenObjectParent.ClearKitchenObject();
      Destroy(gameObject);
   }
   public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
   {
      if (this is PlateKitchenObject)
      {
         plateKitchenObject = this as PlateKitchenObject;
         return true;
      }
      else
      {
         plateKitchenObject = null;
         return false;
      }
   }
   public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKichenObjectParent kichenObjectParent)
   {
      Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
      KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
      kitchenObject.SetKitchenObjectParent(kichenObjectParent);
      return kitchenObject;
   }
}
