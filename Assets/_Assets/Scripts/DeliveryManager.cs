using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipiesAmount = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        bool correctRecipeDeliverd = false;
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO recipeSO = waitingRecipeSOList[i];
            if (recipeSO.kitchenObjectsSOList.Count != plateKitchenObject.GetKitchenObjectSOs().Count)
                continue;

            if (recipeSO.kitchenObjectsSOList.All(plateKitchenObject.GetKitchenObjectSOs().Contains))
            {
                correctRecipeDeliverd = true;
                waitingRecipeSOList.Remove(recipeSO);
                successfulRecipiesAmount++;

                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                break;
            }
        }

        if (!correctRecipeDeliverd)
        {
            OnRecipeFail?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    { 
        return successfulRecipiesAmount;
    }
}
