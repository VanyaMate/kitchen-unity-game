using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ProgressBarUI : MonoBehaviour
{
    private const int MINIMAL_PROGRESS = 0;
    private const int MAXIMAL_PROGRESS = 1;

    [SerializeField] private GameObject _progressItemGameObject;
    [SerializeField] private Image _barImage;

    private IHasProgress _progressItem;

    private void Start()
    {
        this._progressItem = this._progressItemGameObject.GetComponent<IHasProgress>();
        if (this._progressItem == null)
        {
            Debug.LogError("GameObject" + this._progressItemGameObject + "not implement a IHasProgress");
        }

        this._progressItem.OnProgressChange += ProgressItemOnOnProgressChange;
        this._barImage.fillAmount = MINIMAL_PROGRESS;

        this.Hide();
    }

    private void ProgressItemOnOnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        if (e.progressNormalized > MINIMAL_PROGRESS && e.progressNormalized < MAXIMAL_PROGRESS)
        {
            this.Show();
            this._barImage.fillAmount = e.progressNormalized;
        }
        else
        {
            this.Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}