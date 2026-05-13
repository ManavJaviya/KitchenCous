using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<IngredientAddedEventArgs> OnIngridentAdded;
    public class IngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] public List<KitchenObjectSO> validKitchenObjectSOList;
    public List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        if (kitchenObjectSOList == null)
        {
            kitchenObjectSOList = new List<KitchenObjectSO>();
        }
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList == null)
        {
            kitchenObjectSOList = new List<KitchenObjectSO>();
        }
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Not a valid ingredient for plate
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngridentAdded?.Invoke(this, new IngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        if (kitchenObjectSOList == null)
        {
            kitchenObjectSOList = new List<KitchenObjectSO>();
        }
        return kitchenObjectSOList;
    }
}
