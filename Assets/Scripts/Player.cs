using System;
using UnityEngine;

public class Player : MonoBehaviour
{

     public static Player Instance { get; private set; }

     public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
     public class OnSelectedCounterChangedEventArgs : EventArgs
     {
          public ClearCounter selectedCounter;
     }

     [SerializeField] private float speed = 7f;
     [SerializeField] private GameInput gameInput;
     [SerializeField] private LayerMask countersLayerMask;

     private bool isWalking;
     private Vector3 lastInterectDir;
     private ClearCounter selectedCounter;

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
     }

     // private void GameInput_OnInteractAction(object sender, System.EventArgs e)
     // {
     //      if(selectedCounter != null){
     //           selectedCounter.Interect(this);
     //      }
     // }

   private void GameInput_OnInteractAction(object sender, System.EventArgs e)
{
    if (selectedCounter != null)
    {
        Debug.Log("Interacting with: " + selectedCounter.name);
        selectedCounter.Interact(this);
    }
    else
    {
        Debug.LogError("ERROR: selectedCounter is NULL. Raycast might not be detecting objects.");
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

     // public void HandleInteractions()
     // {

     //      Vector2 inputVector = gameInput.GetMovement(); // Get movement input to determine interaction direction
     //      Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

     //      if (moveDir != Vector3.zero)
     //      {
     //           lastInterectDir = moveDir;
     //      }


     //      float interactDistance = 2f;  // Define interaction distance and layer mask

     //      Vector3 rayOrigin = transform.position + Vector3.up * 1f;
     //      if (Physics.Raycast(rayOrigin, lastInterectDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
     //      {
     //           if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
     //           {
     //                SetSelectedCounter(clearCounter); // Always called if a ClearCounter is detected
     //           }
     //           else
     //           {
     //                SetSelectedCounter(null); // Reset if no ClearCounter is found
     //           }
     //      }
     //      else
     //      {
     //           SetSelectedCounter(null); // Reset if nothing is hit
     //      }

     // }

private void HandleInteractions()
{
    Vector2 inputVector = gameInput.GetMovement();
    Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

    if (moveDir != Vector3.zero)
    {
        lastInterectDir = moveDir;
    }

    float interactDistance = 3f; // Increase distance
    Vector3 rayOrigin = transform.position + Vector3.up * 1f;

    Debug.DrawRay(rayOrigin, lastInterectDir * interactDistance, Color.red, 1f); // Draw ray in Scene view

    if (Physics.Raycast(rayOrigin, lastInterectDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
    {
        Debug.Log("Raycast hit: " + raycastHit.transform.name);

        if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        {
            SetSelectedCounter(clearCounter);
        }
        else
        {
            Debug.LogError("Hit object is NOT a ClearCounter!");
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
     // private void SetSelectedCounter(ClearCounter selectedCounter){
     //      this.selectedCounter = selectedCounter; 
     // }
     private void SetSelectedCounter(ClearCounter newCounter)
     {
          if (selectedCounter != newCounter)
          {
               selectedCounter = newCounter;
               //Debug.Log($"Selected Counter Changed to: {newCounter?.name}");
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



}
