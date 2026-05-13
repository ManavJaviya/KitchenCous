using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKichenObjectParent
{
   public static event EventHandler OnObjectPalceHere;
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

      if (kitchenObject != null)
      {
         OnObjectPalceHere?.Invoke(this, EventArgs.Empty);
      }
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

   /// <summary>
   /// Removes any kitchen object sitting on this counter and resets counter-specific state for a full game restart.
   /// </summary>
   public virtual void ClearForRestart()
   {
      if (HasKitchenObject())
      {
         GetKitchenObject().DestroySelf();
      }
   }
}
