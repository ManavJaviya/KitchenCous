using System;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.onProgressChangeEventArgs> onProgressChange;
    public event EventHandler<onStateChangeEventArgs> onStateChange;
    public class onStateChangeEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Ideal,
        Fryinng,
        Fried,
        Burned,
    }


    [SerializeField] private StoveRecipeSO[] stoveRecipeSOArray;
    [SerializeField] private BurningRcipeSo[] burnRecipeSOArray;
    private State state;
    private float fryingTimer;
    private float burningTimer;
    private StoveRecipeSO stoveRecipeSO;
    private BurningRcipeSo burningRcipeSo;

    private void Start()
    {
        state = State.Ideal;
    }

    public override void ClearForRestart()
    {
        base.ClearForRestart();
        state = State.Ideal;
        fryingTimer = 0f;
        burningTimer = 0f;
        stoveRecipeSO = null;
        burningRcipeSo = null;

        onStateChange?.Invoke(this, new onStateChangeEventArgs { state = state });
        onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
        {
            progressNormalized = 0f
        });
    }

    void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Ideal:
                    break;
                case State.Fryinng:
                    fryingTimer += Time.deltaTime;

                    onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = fryingTimer / stoveRecipeSO.maxFryingTimer
                    });

                    if (fryingTimer > stoveRecipeSO.maxFryingTimer)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(stoveRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRcipeSo = GetBurnRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        onStateChange?.Invoke(this, new onStateChangeEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    if (burningRcipeSo == null) return;

                    burningTimer += Time.deltaTime;

                    onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = burningTimer / burningRcipeSo.maxBurnTimer
                    });

                    if (burningTimer > burningRcipeSo.maxBurnTimer)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRcipeSo.output, this);
                        state = State.Burned;

                        onStateChange?.Invoke(this, new onStateChangeEventArgs
                        {
                            state = state
                        });

                        onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    stoveRecipeSO = GetStoveRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Fryinng;
                    fryingTimer = 0;

                    onStateChange?.Invoke(this, new onStateChangeEventArgs
                    {
                        state = state
                    });
                    onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
                    {
                        progressNormalized = fryingTimer / stoveRecipeSO.maxFryingTimer
                    });
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //player already have kitchen object can't grab more than one
                if (player.HasKitchenObject())
                {
                    //player already have kitchen object can't grab more than one
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    { //player has plate
                        if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                        {
                            GetKitchenObject().DestroySelf();
                            state = State.Ideal;

                onStateChange?.Invoke(this, new onStateChangeEventArgs
                {
                    state = state
                });

                onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
                {
                    progressNormalized = 0f
                });
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Ideal;

                onStateChange?.Invoke(this, new onStateChangeEventArgs
                {
                    state = state
                });

                onProgressChange?.Invoke(this, new IHasProgress.onProgressChangeEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        StoveRecipeSO stoveRecipeSO = GetStoveRecipeSOWithInput(inputKitchenObjectSO);
        return stoveRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        StoveRecipeSO stoveRecipeSO = GetStoveRecipeSOWithInput(inputKitchenObjectSO);
        if (stoveRecipeSO != null)
        {
            return stoveRecipeSO.output;
        }
        else
        {
            return null;
        }
    }
    private StoveRecipeSO GetStoveRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (StoveRecipeSO stoveRecipeSO in stoveRecipeSOArray)
        {
            if (stoveRecipeSO.input == inputKitchenObjectSO)
            {
                return stoveRecipeSO;
            }
        }
        return null;
    }
    private BurningRcipeSo GetBurnRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRcipeSo burningRcipeSo in burnRecipeSOArray)
        {
            if (burningRcipeSo.input == inputKitchenObjectSO)
            {
                return burningRcipeSo;
            }
        }
        return null;
    }
}
