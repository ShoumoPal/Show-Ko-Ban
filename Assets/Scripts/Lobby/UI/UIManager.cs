using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

/* Enum for Panel type */

public enum PanelType
{
    PAUSE,
    LEVEL_COMPLETE,
    LEVEL_SELECTION
}

/* Public class for the UIPanel information */

[Serializable]
public class UIPanel
{
    public CanvasGroup CanvasGroup;
    public RectTransform RectTransform;
    public PanelType PanelType;
}

/* UI Manager lazy singleton which manages the panel animations */

public class UIManager : GenericLazySingleton<UIManager>
{
    [SerializeField] private UIPanel[] _panels;
    [SerializeField] private float _fadeTime;

    public void PanelFadeIn(PanelType panelType)
    {
        UIPanel panel = Array.Find(_panels, i => i.PanelType == panelType);
        panel.CanvasGroup.gameObject.SetActive(true);
        panel.CanvasGroup.alpha = 0f;
        panel.RectTransform.transform.localPosition = new Vector3(0f, -1000f, 0f);
        panel.RectTransform.DOAnchorPos(new Vector2(0f, 0f), _fadeTime, false).SetEase(Ease.Linear);
        panel.CanvasGroup.DOFade(1f, _fadeTime);
    }

    public float GetFadeTime()
    {
        return _fadeTime;
    }

    public IEnumerator PanelFadeOut(PanelType panelType)
    {
        UIPanel panel = Array.Find(_panels, i => i.PanelType == panelType);
        panel.CanvasGroup.alpha = 1f;
        panel.RectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        panel.RectTransform.DOAnchorPos(new Vector2(0f, -1000f), _fadeTime, false).SetEase(Ease.Linear);
        panel.CanvasGroup.DOFade(0f, _fadeTime);
        yield return new WaitForSeconds(_fadeTime);

        panel.CanvasGroup.gameObject.SetActive(false);
    }
}
