using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKichenObjectParent
{
    public Transform GetKitchenFollowTransform();

    public void SetKitchenObjact(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}
