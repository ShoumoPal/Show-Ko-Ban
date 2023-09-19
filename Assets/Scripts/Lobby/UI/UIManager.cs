using UnityEngine;
using DG.Tweening;

public class UIManager : GenericLazySingleton<UIManager>
{
    [SerializeField] private float _fadeTime;
    [SerializeField] private CanvasGroup _levelSelectionCG;
    [SerializeField] private RectTransform _LevelSelectionRT;

    public void PanelFadeIn()
    {
        _levelSelectionCG.alpha = 0f;
        _LevelSelectionRT.transform.localPosition = new Vector3(0f, -1000f, 0f);
        _LevelSelectionRT.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.Linear);
        _levelSelectionCG.DOFade(1f, _fadeTime);
    }

    public void PanelFadeOut()
    {
        _levelSelectionCG.alpha = 1f;
        _LevelSelectionRT.transform.localPosition = new Vector3(0f, 0f, 0f);
        _LevelSelectionRT.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.InOutQuint);
        _levelSelectionCG.DOFade(1f, _fadeTime);
    }
}
