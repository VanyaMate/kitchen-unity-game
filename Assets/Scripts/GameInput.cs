using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputActions _playerInputActions;


    private void Awake()
    {
        this._playerInputActions = new PlayerInputActions();
        this._playerInputActions.Player.Enable();
        this._playerInputActions.Player.Interact.performed += this.InteractOnperformed;
        this._playerInputActions.Player.InteractAlternate.performed += this.InteractAlternateOnperformed;
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        this.OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternateOnperformed(InputAction.CallbackContext obj)
    {
        this.OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector2 inputVector = this._playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return new Vector3(inputVector.x, 0, inputVector.y);
    }
}