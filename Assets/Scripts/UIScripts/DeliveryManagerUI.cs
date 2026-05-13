using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplete;

    private void Awake()
    {
        recipeTemplete.gameObject.SetActive(false);
    }
    private DeliveryManager subscribedDeliveryManager;

    private void Start()
    {
        subscribedDeliveryManager = DeliveryManager.Instance;
        if (subscribedDeliveryManager != null)
        {
            subscribedDeliveryManager.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
            subscribedDeliveryManager.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        }
        UpdateVisual();
    }

    private void OnDestroy()
    {
        if (subscribedDeliveryManager != null)
        {
            subscribedDeliveryManager.OnRecipeSpawn -= DeliveryManager_OnRecipeSpawn;
            subscribedDeliveryManager.OnRecipeCompleted -= DeliveryManager_OnRecipeCompleted;
        }
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawn(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach ( Transform child in container)
        {
            if(child == recipeTemplete) continue;
            Destroy(child.gameObject);
        }

        foreach(RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSoList())
        {
            Transform recipeTramsform = Instantiate(recipeTemplete,container);
            recipeTramsform.gameObject.SetActive(true);
            recipeTramsform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }
}
