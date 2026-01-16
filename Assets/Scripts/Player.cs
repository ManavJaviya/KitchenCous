using System;
using UnityEngine;

public class Player : MonoBehaviour, IKichenObjectParent
{
     public static Player Instance { get; private set; }

     public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
     public class OnSelectedCounterChangedEventArgs : EventArgs
     {
          public BaseCounter selectedCounter;
     }

     [SerializeField] private float speed = 7f;
     [SerializeField] private GameInput gameInput;
     [SerializeField] private LayerMask countersLayerMask;
     [SerializeField] private Transform kitchenObjectHoldPoint;


     private bool isWalking;
     private Vector3 lastInterectDir;
     private BaseCounter selectedCounter;
     private KitchenObject kitchenObject;

     private void Awake()
     {
          if (Instance != null)
          {
               Debug.LogError("there is more than one player");
          }
          Instance = this;
     }

     private void Start()
     {
          gameInput.OnInteractAction += GameInput_OnInteractAction;
          gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
     }

     private void GameInput_OnInteractAction(object sender, System.EventArgs e)
     {
          if (selectedCounter != null)
          {
               selectedCounter.Interact(this);
          }
     }
     private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
     {
          if (selectedCounter != null)
          {
               selectedCounter.InteractAlternate(this);
          }
     }
     private void Update()
     {
          HandelMovement();
          HandleInteractions();
     }

     public bool IsWalking()
     {
          return isWalking;
     }

     private void HandleInteractions()
     {
          Vector2 inputVector = gameInput.GetMovement();
          Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

          if (moveDir != Vector3.zero)
          {
               lastInterectDir = moveDir;
          }

          float interactDistance = 1f; // Increase distance
          Vector3 rayOrigin = transform.position + Vector3.up * 1f;

          Debug.DrawRay(rayOrigin, lastInterectDir * interactDistance, Color.red, 1f); // Draw ray in Scene view

          if (Physics.Raycast(rayOrigin, lastInterectDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
          {
               //Debug.Log("Raycast hit: " + raycastHit.transform.name);

               if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
               {
                    SetSelectedCounter(baseCounter);
               }
               else
               {
                    //Debug.LogError("Hit object is NOT a ClearCounter!");
                    SetSelectedCounter(null);
               }
          }
          else
          {
               //Debug.LogError("Raycast did NOT hit anything! Check object layers and distance.");
               SetSelectedCounter(null);
          }
     }

     private void HandelMovement()
     {
          Vector2 inputVector = gameInput.GetMovement();
          Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

          transform.position += moveDir * Time.deltaTime * speed;  // Update position if there is movement input
          isWalking = moveDir != Vector3.zero;  // Check if the player is moving

          if (isWalking)
          {
               float rotateSpeed = 10f;        // Update rotation only when moving
               transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
          }
     }

     private void SetSelectedCounter(BaseCounter newCounter)
     {
          if (selectedCounter != newCounter)
          {
               selectedCounter = newCounter;
               OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
               {
                    selectedCounter = newCounter
               });
               //Debug.Log("OnSelectedCounterChanged event invoked.");
          }
          else
          {
               //Debug.Log("Selected counter did not change.");
          }
     }
     public Transform GetKitchenFollowTransform()
     {
          return kitchenObjectHoldPoint;
     }

     public void SetKitchenObjact(KitchenObject kitchenObject)
     {
          this.kitchenObject = kitchenObject;
     }
     public KitchenObject GetKitchenObject()
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
