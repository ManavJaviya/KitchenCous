using System;
using JetBrains.Annotations;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnplateSpwaned;
    public event EventHandler OnplateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spwanPlateTimer;
    private float spwanPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update()
    {
        spwanPlateTimer += Time.deltaTime;
        if(spwanPlateTimer > spwanPlateTimerMax)
        {
            spwanPlateTimer = 0f;
            if(platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;
                OnplateSpwaned?.Invoke(this , EventArgs.Empty);
            }
        }  
    }
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            //player dosen't have any kitchenobject
            if(platesSpawnAmount > 0)
            {
                platesSpawnAmount--;
                
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO , player);
                OnplateRemoved?.Invoke(this , EventArgs.Empty);
            }
        }
    }
}
