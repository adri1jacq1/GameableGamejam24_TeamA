using Children;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CycleManager : MonoBehaviour
{
    public TextMeshProUGUI timerUIText;
    public int timerMaxSeconds = 150;
    public Transform childrenParent;
    public int numberOfChildrenInCycle1 = 3;
    public Transform trashesParent;
    public UnityEvent OnGameOver, OnGameWin;

    private bool isRunning = false;
    private float startTime;
    private int numberOfActiveChildren;

    void Start()
    {
        numberOfActiveChildren = numberOfChildrenInCycle1;
    }

    public void StartCycle()
    {
        startTime = Time.time;
        isRunning = true;

        // Enable children for the new cycle
        for (int i = 0; i < childrenParent.childCount; i++)
        {
            var child = childrenParent.GetChild(i).gameObject;

            child.GetComponent<Child>().StopDisableCoroutine();
            child.SetActive(i < numberOfActiveChildren);
        }

        // Reset trashes for the new cycle
        foreach (TrashOpening trash in trashesParent.GetComponentsInChildren<TrashOpening>())
        {
            trash.Reset();
        }
    }

    public void StopCycle()
    {
        isRunning = false;
        numberOfActiveChildren++;
    }

    void Update()
    {
        if (isRunning)
        {
            RefreshTimer();
            if (IsGameWin())
            {
                StopCycle();
                OnGameWin?.Invoke();
            }
        }
    }

    void RefreshTimer()
    {
        float timeSinceStart = Time.time - startTime;
        float remainingTime = timerMaxSeconds - timeSinceStart;
        string minutes, seconds;
        if (remainingTime > 0)
        {
            minutes = ((int)remainingTime / 60).ToString();
            seconds = (remainingTime % 60).ToString("f0");
        }
        else
        {
            minutes = "0";
            seconds = "00";
            StopCycle();
            OnGameOver?.Invoke();
        }

        timerUIText.text = minutes + ":" + seconds;
        timerUIText.color = minutes == "0" ? Color.red : Color.white;
    }

    bool IsGameWin()
    {
        foreach (Child child in childrenParent.GetComponentsInChildren<Child>())
        {
            if (!child.IsFed())
                return false;
        }

        return true;
    }
}