using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player _player;

    private const string IS_WALKING = "IsWalking";
    private Animator _animator;

    private void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        this._animator.SetBool(IS_WALKING, this._player.IsWalking());
    }

    private void Update()
    {
        this._animator.SetBool(IS_WALKING, this._player.IsWalking());
    }
}