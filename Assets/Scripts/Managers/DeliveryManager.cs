using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    public static DeliveryManager Instance { get; private set;}
    [SerializeField] private RecipeListSO recipeListSO ;

    public List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    public int successfulRecipeCount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Start()
    {
        GameMenu.Instance.OnRestart += GameMenu_OnRestart;
    }

    private void GameMenu_OnRestart(object sender, EventArgs e)
    {
        waitingRecipeSOList.Clear();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range( 0 , recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawn?.Invoke(this , EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for( int i = 0 ; i < waitingRecipeSOList.Count ; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            { // has the same number of ingredient
                bool plateMatchesRecipe = true;
                foreach ( KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {   // cycle through all ingredient in recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList() )
                    {   // cycle through all ingredient in plate
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {   //ingredient matches
                            ingredientFound = true;
                            break;
                        }
                        
                    }
                    if(!ingredientFound)
                    {
                        plateMatchesRecipe = false;
                    }
                    if(plateMatchesRecipe)
                    {
                        successfulRecipeCount ++;
                        waitingRecipeSOList.RemoveAt(i);
                        OnRecipeCompleted?.Invoke(this , EventArgs.Empty);
                        OnRecipeSuccess?.Invoke(this , EventArgs.Empty);
                        return;
                    }
                }
                
            }
        }
        Debug.Log("Player did not deliver correct order");
        OnRecipeFail?.Invoke(this , EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSoList()
    {
        return waitingRecipeSOList;
    }
    public int GetSuccessfulRecipeCount()
    {
        return successfulRecipeCount;
    }
}
