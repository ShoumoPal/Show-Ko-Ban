using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UIManager : GenericLazySingleton<UIManager>
{
    [SerializeField] private float _fadeTime;
    [SerializeField] private CanvasGroup _levelSelectionCG;
    [SerializeField] private RectTransform _LevelSelectionRT;

    public void PanelFadeIn()
    {
        _levelSelectionCG.gameObject.SetActive(true);
        _levelSelectionCG.alpha = 0f;
        _LevelSelectionRT.transform.localPosition = new Vector3(0f, -1000f, 0f);
        _LevelSelectionRT.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.Linear);
        _levelSelectionCG.DOFade(1f, _fadeTime);
    }

    public IEnumerator PanelFadeOut()
    {
        _levelSelectionCG.alpha = 1f;
        _LevelSelectionRT.transform.localPosition = new Vector3(0f, 0f, 0f);
        _LevelSelectionRT.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.Linear);
        _levelSelectionCG.DOFade(0f, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);

        _levelSelectionCG.gameObject.SetActive(false);
    }
}
