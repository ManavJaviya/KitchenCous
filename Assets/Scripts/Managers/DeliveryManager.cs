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

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;

    private int successfulRecipeCount;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one DeliveryManager in scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Start()
    {
        if (GameMenu.Instance != null)
        {
            GameMenu.Instance.OnRestart += GameMenu_OnRestart;
        }
    }

    private void OnDestroy()
    {
        if (GameMenu.Instance != null)
        {
            GameMenu.Instance.OnRestart -= GameMenu_OnRestart;
        }
    }

    private void GameMenu_OnRestart(object sender, EventArgs e)
    {
        waitingRecipeSOList.Clear();
        successfulRecipeCount = 0;
        spawnRecipeTimer = spawnRecipeTimerMax;
    }

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.IsGamePlaying())
        {
            return;
        }

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
        IReadOnlyList<KitchenObjectSO> plateContents = plateKitchenObject.GetKitchenObjectSOList();
        for( int i = 0 ; i < waitingRecipeSOList.Count ; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count != plateContents.Count)
            {
                continue;
            }

            bool plateMatchesRecipe = true;
            foreach ( KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
            {
                bool ingredientFound = false;
                foreach(KitchenObjectSO plateKitchenObjectSO in plateContents)
                {
                    if(plateKitchenObjectSO == recipeKitchenObjectSO)
                    {
                        ingredientFound = true;
                        break;
                    }
                }
                if(!ingredientFound)
                {
                    plateMatchesRecipe = false;
                    break;
                }
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
        Debug.Log("Player did not deliver correct order");
        OnRecipeFail?.Invoke(this , EventArgs.Empty);
    }

    public IReadOnlyList<RecipeSO> GetWaitingRecipeSoList()
    {
        return waitingRecipeSOList;
    }
    public int GetSuccessfulRecipeCount()
    {
        return successfulRecipeCount;
    }
}
