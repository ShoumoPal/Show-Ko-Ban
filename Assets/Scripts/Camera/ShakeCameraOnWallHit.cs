using Cinemachine;
using UnityEngine;


/* Responsible for Camera shke using Cinemachine */

public class ShakeCameraOnWallHit : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam;
    [SerializeField] private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    [SerializeField] private float _hitAmplitudeGain;
    [SerializeField] private float _hitFrequencyGain;
    [SerializeField] private float _shakeDuration;

    private bool _isShaking;
    private float _shakeTimeElapsed;

    private void Awake()
    {
        EventService.Instance.OnCameraShake += ShakeCamera;
        _multiChannelPerlin = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _isShaking = false;
        _shakeTimeElapsed = 0.0f;
    }

    private void Update()
    {
        if (_isShaking)
        {
            _shakeTimeElapsed += Time.deltaTime;
            if (_shakeTimeElapsed > _shakeDuration)
                StopShake();
        }    
    }

    private void ShakeCamera()
    {
        _multiChannelPerlin.m_AmplitudeGain = _hitAmplitudeGain;
        _multiChannelPerlin.m_FrequencyGain = _hitFrequencyGain;
        _isShaking = true;
        _shakeTimeElapsed = 0.0f;
    }

    private void StopShake()
    {
        _multiChannelPerlin.m_AmplitudeGain = 0f;
        _multiChannelPerlin.m_FrequencyGain = 0f;
        _isShaking = false;
        _shakeTimeElapsed = 0.0f;
    }
    private void OnDestroy()
    {
        EventService.Instance.OnCameraShake -= ShakeCamera;
    }
}
