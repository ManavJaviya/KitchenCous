using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable] public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> KitchenObjectSO_GameObjectList;

    private void Awake()
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSO_GameObject in KitchenObjectSO_GameObjectList)
        {
                kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
        if (plateKitchenObject != null)
        {
            plateKitchenObject.OnIngridentAdded += PlateKitchenObject_OnIngridentAdded;
        }
    }

    private void OnDestroy()
    {
        if (plateKitchenObject != null)
        {
            plateKitchenObject.OnIngridentAdded -= PlateKitchenObject_OnIngridentAdded;
        }
    }

    private void PlateKitchenObject_OnIngridentAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSO_GameObject in KitchenObjectSO_GameObjectList)
        {
            if(kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
