using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;

    private bool _isWalking;
    private Vector3 _lastDirection = Vector3.zero;

    private readonly float _playerRadius = .7f;
    private readonly float _playerHeight = 2f;
    private readonly float _interactDistance = 2f;


    private void Update()
    {
        Vector3 inputDirection = gameInput.GetMovementVectorNormalized();

        if (inputDirection != Vector3.zero)
        {
            _lastDirection = inputDirection;
        }

        HandleMovement(inputDirection);
        HandleInteractions(_lastDirection);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void HandleInteractions(Vector3 direction)
    {
        if (Physics.Raycast(
                transform.position,
                direction,
                out RaycastHit raycastHit,
                _interactDistance,
                counterLayerMask
            ))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }

    private void HandleMovement(Vector3 direction)
    {
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * _playerHeight,
            _playerRadius,
            direction,
            moveDistance
        );

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(direction.x, 0, 0);
            canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * _playerHeight,
                _playerRadius,
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
                canMove = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * _playerHeight,
                    _playerRadius,
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
            transform.position += moveDistance * direction;
        }

        _isWalking = direction != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
    }
}