using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter _clearCounter;
    [SerializeField] private GameObject _visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += this.PlayerInstanceOnOnSelectedCounterChanged;
    }

    private void PlayerInstanceOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == this._clearCounter)
        {
            this.Show();
        }
        else
        {
            this.Hide();
        }
    }

    private void Show()
    {
        this._visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        this._visualGameObject.SetActive(false);
    }
}