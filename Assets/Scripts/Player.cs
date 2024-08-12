using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    [SerializeField] private Transform _kitchenObjectHoldPoint;

    private KitchenObject _kitchenObject;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool _isWalking;
    private Vector3 _lastDirection = Vector3.zero;
    private ClearCounter _selectedCounter;

    private readonly float _playerRadius = .7f;
    private readonly float _playerHeight = 2f;
    private readonly float _interactDistance = 2f;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.gameInput.OnInteractAction += this.GameInputOnOnInteractAction;
    }

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if (this._selectedCounter != null)
        {
            this._selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        Vector3 inputDirection = this.gameInput.GetMovementVectorNormalized();

        if (inputDirection != Vector3.zero)
        {
            this._lastDirection = inputDirection;
        }

        this.HandleMovement(inputDirection);
        this.HandleInteractions(this._lastDirection);
    }

    public bool IsWalking()
    {
        return this._isWalking;
    }

    private void HandleInteractions(Vector3 direction)
    {
        if (Physics.Raycast(this.transform.position,
                direction,
                out RaycastHit raycastHit, this._interactDistance, this.counterLayerMask
            ))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (this._selectedCounter != clearCounter)
                {
                    this.SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                this.SetSelectedCounter(null);
            }
        }
        else
        {
            this.SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(ClearCounter counter)
    {
        this._selectedCounter = counter;
        this.OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = counter
        });
    }

    private void HandleMovement(Vector3 direction)
    {
        float moveDistance = this.moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(this.transform.position,
            this.transform.position + Vector3.up * this._playerHeight, this._playerRadius,
            direction,
            moveDistance
        );

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(direction.x, 0, 0);
            canMove = !Physics.CapsuleCast(this.transform.position,
                this.transform.position + Vector3.up * this._playerHeight, this._playerRadius,
                moveDirX,
                moveDistance
            );

            if (canMove)
            {
                direction = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, direction.z);
                canMove = !Physics.CapsuleCast(this.transform.position,
                    this.transform.position + Vector3.up * this._playerHeight, this._playerRadius,
                    moveDirZ,
                    moveDistance
                );

                if (canMove)
                {
                    direction = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            this.transform.position += moveDistance * direction;
        }

        this._isWalking = direction != Vector3.zero;
        this.transform.forward = Vector3.Slerp(this.transform.forward, direction, Time.deltaTime * this.rotateSpeed);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return this._kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this._kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this._kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this._kitchenObject != null;
    }
}