using UnityEngine;

public class TrashOpening : MonoBehaviour
{
    public GameObject trashMiniGamePrefab;

    private static GameObject currentMiniGame = null;
    private static Transform witch;

    private bool isWitchNear = false;

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
        if (Input.GetKeyDown(KeyCode.E) && isWitchNear && currentMiniGame == null)
        {
            currentMiniGame = Instantiate(trashMiniGamePrefab);
            foreach (SpriteRenderer sr in currentMiniGame.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.sortingOrder += 20;
            }

            // Disable with movement
            witch.GetComponent<Rigidbody2D>().isKinematic = true;
            witch.GetComponent<WitchMovement>().enabled = false;
        }
    }

    public static void OnQuitGame()
    {
        Destroy(currentMiniGame);

        // Disable with movement
        witch.GetComponent<Rigidbody2D>().isKinematic = false;
        witch.GetComponent<WitchMovement>().enabled = true;
    }
}
