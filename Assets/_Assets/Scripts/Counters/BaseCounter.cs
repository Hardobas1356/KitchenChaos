using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;

    public static void ResetStaticData()
    {
        OnAnyObjectPlaced = null;
    }

    [SerializeField] private Transform topPoint;

    private KitchenObjects kitchenObject;

    public virtual void Interact(Player player)
    {
    }

    public virtual void InteractAlternate(Player player)
    {
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return topPoint;
    }

    public void SetKitchenObject(KitchenObjects kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlaced?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObjects GetKitchenObject()
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
