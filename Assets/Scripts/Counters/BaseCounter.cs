using UnityEngine;

public class BaseCounter : MonoBehaviour, IKichenObjectParent
{
   [SerializeField] private Transform counterTopPoint;
   private KitchenObject kitchenObject;

   public virtual void Interact(Player player)
   {
      Debug.Log("baseClass");
   }
   public virtual void InteractAlternate(Player player)
   {
      Debug.Log("baseClass");
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
