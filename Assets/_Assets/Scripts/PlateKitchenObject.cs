using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
       public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectsSOArray;
    private List<KitchenObjectSO> kitchenObjectSOs;

    private void Awake()
    {
        kitchenObjectSOs = new List<KitchenObjectSO>();
    }
    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectsSOArray.Contains(kitchenObjectSO))
            return false;
        if (kitchenObjectSOs.Contains(kitchenObjectSO))
            return false;


        kitchenObjectSOs.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO
        });
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOs()
    {
        return kitchenObjectSOs;
    }
}
