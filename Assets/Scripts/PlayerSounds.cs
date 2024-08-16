using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float _footstepTimer = 0f;
    private float _footstepTimerMax = .1f;

    private void Awake()
    {
        this._player = GetComponent<Player>();
    }

    private void Update()
    {
        this._footstepTimer -= Time.deltaTime;

        if (this._footstepTimer < 0f)
        {
            this._footstepTimer = this._footstepTimerMax;

            if (this._player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepsSound(this._player.transform.position);
            }
        }
    }
}