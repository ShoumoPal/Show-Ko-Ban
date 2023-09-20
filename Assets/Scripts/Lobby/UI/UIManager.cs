using UnityEngine;
using DG.Tweening;
using System.Collections;

public class UIManager : GenericLazySingleton<UIManager>
{
    [SerializeField] private float _fadeTime;
    [SerializeField] private CanvasGroup _panelCG;
    [SerializeField] private RectTransform _panelRT;

    public void PanelFadeIn()
    {
        Debug.Log("Fade in called");
        _panelCG.gameObject.SetActive(true);
        _panelCG.alpha = 0f;
        _panelRT.transform.localPosition = new Vector3(0f, -1000f, 0f);
        _panelRT.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.Linear);
        _panelCG.DOFade(1f, _fadeTime);
    }

    public float GetFadeTime()
    {
        return _fadeTime;
    }

    public IEnumerator PanelFadeOut()
    {
        _panelCG.alpha = 1f;
        _panelRT.transform.localPosition = new Vector3(0f, 0f, 0f);
        _panelRT.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.Linear);
        _panelCG.DOFade(0f, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);

        _panelCG.gameObject.SetActive(false);
    }
}
