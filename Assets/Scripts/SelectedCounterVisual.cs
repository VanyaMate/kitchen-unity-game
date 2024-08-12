using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += this.PlayerInstanceOnOnSelectedCounterChanged;
    }

    private void PlayerInstanceOnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == this._baseCounter)
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
        foreach (GameObject visualGameObject in this._visualGameObjects)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in this._visualGameObjects)
        {
            visualGameObject.SetActive(false);
        }
    }
}