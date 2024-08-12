using System;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    [SerializeField] private Image _barImage;

    private void Start()
    {
        this._cuttingCounter.OnProgressChange += CuttingCounterOnOnProgressChange;
        this._barImage.fillAmount = 0;

        this.Hide();
    }

    private void CuttingCounterOnOnProgressChange(object sender, CuttingCounter.OnProgressChangeEventArgs e)
    {
        if (e.progressNormalized != 0 && e.progressNormalized != 1)
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