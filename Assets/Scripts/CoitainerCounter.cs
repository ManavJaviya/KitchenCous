using UnityEngine;

public class CoitainerCounter : MonoBehaviour,IKichenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   [SerializeField] private Transform counterTopPoint;
   private KitchenObject kitchenObject;


   public void Interact(Player player)
   {
      if (kitchenObject == null)
      {
         Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
         kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

         if (kitchenObject != null)
         {
            kitchenObject.SetKitchenObjectParent(this);
         }
         else
         {
            Debug.LogError("Prefab does not contain a KitchenObject component! Make sure the prefab has it.");
         }
      }
      else
      {
         Debug.Log("KitchenObjectParent : "+kitchenObject.GetKitchenObjectParent());
         kitchenObject.SetKitchenObjectParent(player);
      }
   }

   public Transform GetKitchenFollowTransform()
   {
      return counterTopPoint;
   }

   public void SetKitchenObjact(KitchenObject kitchenObject)
   {
      this.kitchenObject = kitchenObject;
   }
   public KitchenObject GetKitchenObject()
   {
      return kitchenObject;
   }
   public void ClearKitchenObject()
   {
      kitchenObject = null;
   }
   public bool HasKitchenObject()
   {
      return kitchenObject != null;
   }
}
