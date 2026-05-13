using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroungImg;
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI massageTxt;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFail(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        backgroungImg.color = failedColor;
        iconImg.sprite = failedSprite;
        massageTxt.text = "DELIVERY \n FAILED";
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
    {        
        gameObject.SetActive(true);
        backgroungImg.color = successColor;
        iconImg.sprite = successSprite;
        massageTxt.text = "DELIVERY \n SUCCESS";
    }
}
