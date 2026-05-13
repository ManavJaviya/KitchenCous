using System;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
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
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        // Clear existing visuals
        foreach (Transform child in transform)
        {
            if(child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        // Create new visuals based on the ingredients on the plate
        var ingredients = plateKitchenObject.GetKitchenObjectSOList();
        if (ingredients == null)
        {
            return;
        }
        foreach (KitchenObjectSO kitchenObjectSO in ingredients)
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleIconUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
