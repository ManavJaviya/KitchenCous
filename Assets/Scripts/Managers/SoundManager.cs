using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private DeliveryManager subscribedDeliveryManager;
    private Player subscribedPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one SoundManager in scene.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        subscribedDeliveryManager = DeliveryManager.Instance;
        if (subscribedDeliveryManager != null)
        {
            subscribedDeliveryManager.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
            subscribedDeliveryManager.OnRecipeFail += DeliveryManager_OnRecipeFail;
        }
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        subscribedPlayer = Player.Instance;
        if (subscribedPlayer != null)
        {
            subscribedPlayer.OnPickup += Player_Onpickup;
        }
        BaseCounter.OnObjectPalceHere += BaseCounter_OnObjectPalceHere;
        TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
    }

    private void OnDestroy()
    {
        if (subscribedDeliveryManager != null)
        {
            subscribedDeliveryManager.OnRecipeSuccess -= DeliveryManager_OnRecipeSuccess;
            subscribedDeliveryManager.OnRecipeFail -= DeliveryManager_OnRecipeFail;
        }
        CuttingCounter.OnAnyCut -= CuttingCounter_OnAnyCut;
        if (subscribedPlayer != null)
        {
            subscribedPlayer.OnPickup -= Player_Onpickup;
        }
        BaseCounter.OnObjectPalceHere -= BaseCounter_OnObjectPalceHere;
        TrashCounter.OnObjectTrashed -= TrashCounter_OnObjectTrashed;
    }

    private void TrashCounter_OnObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash , trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectPalceHere(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop , baseCounter.transform.position); 
    }

    private void Player_Onpickup(object sender, EventArgs e)
    {
        Player player = sender as Player;
        if (player == null)
        {
            return;
        }
        PlaySound(audioClipRefsSO.objectPick ,player.transform.position );
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop ,cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail , deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess , deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 transform , float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0 , audioClipArray.Length)] , transform , volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 transform , float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip,transform,volume);
    }
    public void PlayFootStepSound(Vector3 position , float volume)
    {
        PlaySound(audioClipRefsSO.footStep , position , volume);
    }
}
