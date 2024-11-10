using UnityEngine;

public class TrashOpening : MonoBehaviour
{
    public GameObject trashMiniGame;

    private bool isWitchNear = false;
    private Transform witch;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            isWitchNear = true;
            witch = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            isWitchNear = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isWitchNear)
        {
            Instantiate(trashMiniGame, Camera.main.transform);
            witch.GetComponent<WitchMovement>().enabled = false;
        }
    }
}
