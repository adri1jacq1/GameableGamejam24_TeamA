using UnityEngine;

public class GameStart : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
