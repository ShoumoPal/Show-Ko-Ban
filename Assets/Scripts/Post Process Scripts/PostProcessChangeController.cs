using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

/* Script responsible for changing PP profile for the bonus feature */

public class PostProcessChangeController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _triesText;
    [SerializeField] private Volume _globalVolume;
    [SerializeField] private VolumeProfile _primaryProfile;
    [SerializeField] private VolumeProfile _secondaryProfile;
    [SerializeField] private float _revealTime;
    [SerializeField] private int _tries;

    private bool isRunning;
    
    private void Start()
    {
        isRunning = false;
        ChangeTriesText(_tries);
    }

    void Update()
    {
        if(!isRunning && Input.GetKeyDown(KeyCode.Space) && _tries != 0)
            StartCoroutine(RevealPositions());
    }

    private IEnumerator RevealPositions()
    {
        isRunning = true;

        AudioService.Instance.PlayFX3(SoundType.Interference);
        ChangeTriesText(--_tries);
        _globalVolume.profile = _secondaryProfile;
        yield return new WaitForSeconds(_revealTime);
        _globalVolume.profile = _primaryProfile;
        isRunning = false;
    }

    private void ChangeTriesText(int tries)
    {
        _triesText.text = "Tries remaining : <color=red>" + tries + "</color>";
    }
}
