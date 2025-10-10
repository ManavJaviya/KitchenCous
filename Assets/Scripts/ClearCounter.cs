using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKichenObjectParent
{
   [SerializeField] private KitchenObjectSO kitchenObjectSO;
   [SerializeField] private Transform counterTopPoint;
   [SerializeField] private ClearCounter secoentClearCounter;
   [SerializeField] private bool testing;

   private KitchenObject kitchenObject;


   private void Update()
   {
      if (testing && Input.GetKeyDown(KeyCode.T))
      {
         if (kitchenObject != null)
         {
            kitchenObject.SetKitchenObjectParent(secoentClearCounter);
         }
      }
   }

   // public void Interect(Player player)
   // {
   //    if (kitchenObject == null)
   //    {
   //       Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
   //       kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
   //    }
   //    else
   //    {
   //       Debug.Log(kitchenObject.GetKitchenObjectParent());
   //       //kitchenObject.SetKitchenObjectParent(player);
   //    }
   // }
   
   public void Interact(Player player)
{
    if (kitchenObject == null)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        KitchenObject newKitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        if (newKitchenObject != null)
        {
            newKitchenObject.SetKitchenObjectParent(this);
        }
        else
        {
            Debug.LogError("Prefab does not contain a KitchenObject component! Make sure the prefab has it.");
        }
    }
    else
    {
        Debug.Log(kitchenObject.GetKitchenObjectParent());
        // kitchenObject.SetKitchenObjectParent(player);
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
