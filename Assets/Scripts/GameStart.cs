using UnityEngine;
using UnityEngine.Events;

public class GameStart : MonoBehaviour
{
    public UnityEvent OnStartGame;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            OnStartGame?.Invoke();
        }
    }
}
