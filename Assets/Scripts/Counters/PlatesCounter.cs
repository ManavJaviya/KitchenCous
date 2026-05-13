using System;
using JetBrains.Annotations;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnplateSpwaned;
    public event EventHandler OnplateRemoved;
    /// <summary>Fired when the stacked plate count is cleared on game restart (visuals should reset).
    /// </summary>
    public event EventHandler OnPlatesStackCleared;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spwanPlateTimer;
    private float spwanPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    public override void ClearForRestart()
    {
        base.ClearForRestart();
        platesSpawnAmount = 0;
        spwanPlateTimer = 0f;
        OnPlatesStackCleared?.Invoke(this, EventArgs.Empty);
    }

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
            if(platesSpawnAmount <= 0)
            {
                return;
            }

            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            bool plateHeld = player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out _);
            if (plateHeld)
            {
                platesSpawnAmount--;
                OnplateRemoved?.Invoke(this , EventArgs.Empty);
            }
        }
    }
}
