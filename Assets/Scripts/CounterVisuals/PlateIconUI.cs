using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }
    void Start()
    {
        plateKitchenObject.OnIngridentAdded += PlateKitchenObject_OnIngridentAdded;
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
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateSingleIconUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
