using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEnd : MonoBehaviour
{
    public UnityEvent OnTriggerWitchLaugh, OnQuitPanel;

    void Start()
    {
        StartCoroutine(TriggerWitchLaughCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnQuitPanel?.Invoke();
            gameObject.SetActive(false);
        }
    }

    IEnumerator TriggerWitchLaughCoroutine()
    {
        yield return new WaitForSeconds(2f);
        OnTriggerWitchLaugh?.Invoke();
    }
}
