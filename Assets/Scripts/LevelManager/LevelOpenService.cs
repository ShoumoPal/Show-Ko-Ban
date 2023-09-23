using UnityEngine;
using UnityEngine.UI;

/* Level opening service present on each button */

[RequireComponent (typeof(Button))]
public class LevelOpenService : MonoBehaviour
{
    private Button button;
    [SerializeField] private string levelName;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenLevel);
    }

    private void OnEnable()
    {
        LevelStatus status = LevelManagerService.Instance.GetLevelStatus(levelName);

        if(status == LevelStatus.Locked)
        {
            button.interactable = false;
        }
        if(status == LevelStatus.Unlocked)
        {
            button.interactable = true;
        }
    }

    private void OpenLevel()
    {
        AudioService.Instance.PlayFX(SoundType.Button_Click);
        LevelStatus status = LevelManagerService.Instance.GetLevelStatus(levelName);
        switch (status)
        {
            case LevelStatus.Locked:
                Debug.Log("Locked");
                break;
            case LevelStatus.Unlocked:
                StartCoroutine(LevelManagerService.Instance.LoadScene(levelName));
                break;
            case LevelStatus.Completed:
                StartCoroutine(LevelManagerService.Instance.LoadScene(levelName));
                break;
        }
    }
}
