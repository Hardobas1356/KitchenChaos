using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static StoveCounter;
using static UnityEngine.CullingGroup;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnTimerMax)
        {
            spawnPlateTimer = 0;

            if (spawnPlateTimer < spawnTimerMax && platesSpawnedAmount < platesSpawnedAmountMax)
            {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && platesSpawnedAmount > 0)
        {
            KitchenObjects.SpawnKitchenObject(plateKitchenObjectSO, this);
            this.GetKitchenObject().SetKitchenObjectParent(player);

            platesSpawnedAmount--;
            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
}
