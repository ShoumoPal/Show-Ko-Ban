using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* Volume controller script for controlling volume in the lobby */

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private RectTransform _imageRT;
    [SerializeField] private CanvasGroup _imageCG;

    private bool isImageShown;

    private void Awake()
    {
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _volumeButton.onClick.AddListener(ShowVolumeSlider);
        _imageCG.gameObject.SetActive(false);
        isImageShown = false;
    }

    private void ChangeVolume(float value)
    {
        _masterMixer.SetFloat("Volume", Mathf.Log10(value/100) * 20);
    }

    private void ShowVolumeSlider()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        if (isImageShown)
        {
            ImageFadeOut();
        }
        else
        {
            ImageFadeIn();
        }
    }

    private void ImageFadeIn()
    {
        _imageCG.gameObject.SetActive(true);
        _imageCG.alpha = 0f;
        _imageRT.transform.localPosition = new Vector3(82f, 0f, 0f);
        _imageRT.DOAnchorPos(new Vector2(82f, -60f), 0.5f, false).SetEase(Ease.InOutSine);
        _imageCG.DOFade(1f, 0.5f);
        isImageShown = true;
    }

    private void ImageFadeOut()
    {
        _imageCG.alpha = 1f;
        _imageRT.transform.localPosition = new Vector3(82f, -60f, 0f);
        _imageRT.DOAnchorPos(new Vector2(82f, 0f), 0.5f, false).SetEase(Ease.InOutSine);
        _imageCG.DOFade(0f, 0.5f);
        _imageCG.gameObject.SetActive(true);
        isImageShown = false;
    }
}
