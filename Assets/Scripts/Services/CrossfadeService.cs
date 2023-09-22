using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class CrossfadeService : GenericMonoSingleton<CrossfadeService>
{
    [SerializeField] private RectTransform _imageRT;
    [SerializeField] private TextMeshProUGUI _levelText;

    public float CrossFadeTime;

    public void CrossFadeIn(string level)
    {
        _levelText.text = level;
        _imageRT.DOAnchorPos(new Vector2(0f, 0f), CrossFadeTime, false).SetEase(Ease.InOutSine);
        _levelText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), CrossFadeTime, false).SetEase(Ease.InOutSine);
    }

    public void CrossFadeOut()
    {
        _imageRT.DOAnchorPos(new Vector2(-1394f, 0f), CrossFadeTime, false).SetEase(Ease.InOutSine);
        _levelText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1394f, 0f), CrossFadeTime, false).SetEase(Ease.InOutSine);
    }
}
