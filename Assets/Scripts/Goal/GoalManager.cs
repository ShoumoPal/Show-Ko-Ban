using UnityEngine;

public class GoalManager : GenericLazySingleton<GoalManager>
{
    public bool isGoalReached;
    [SerializeField] private GameObject[] goals;

    private void OnEnable()
    {
        EventService.Instance.OnGoalReached += CheckGoalReached;
    }
    private void Start()
    {
        //goals = GameObject.FindGameObjectsWithTag("Goal");
        isGoalReached = false;
    }
    public bool CheckGoalReached(int childCount)
    {
        if (childCount + 1 != goals.Length)
            return false;

        for(int i = 0; i < goals.Length; i++)
        {
            if (goals[i].GetComponent<Goal>().GetOccupied() == false)
                return false;
        }
        return true;
    }
    private void OnDisable()
    {
        EventService.Instance.OnGoalReached -= CheckGoalReached;
    }
}
