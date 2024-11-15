using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrashOpening : MonoBehaviour
{
    public GameObject trashMiniGamePrefab;
    public AudioClip openingSound, throwingSound;

    private static GameObject currentMiniGame = null;
    private static TrashOpening currentInstance = null;
    private static Transform witch;

    private bool isWitchNear = false;

    [SerializeField] private FoodGroupSpawn[] _spawns;

    public Animator animator;
    private bool opening = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        if (opening)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && isWitchNear && currentMiniGame == null)
        {
            StartCoroutine(OpenMiniGame());
        }
    }

    IEnumerator OpenMiniGame()
    {
        opening = true;

        currentInstance = this;

        audioSource.clip = openingSound;
        audioSource.Play();

        animator.SetTrigger("Open");

        yield return new WaitForSeconds(0.5f);

        currentMiniGame = Instantiate(trashMiniGamePrefab);
        foreach (SpriteRenderer sr in currentMiniGame.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder += 20;
        }

        // Disable witch movement
        witch.GetComponent<Rigidbody2D>().isKinematic = true;

        if (currentMiniGame.TryGetComponent<TrashCanList>(out var trashCanList))
        {
            RandomizeFood(trashCanList, _spawns);
        }

        var arm = currentMiniGame.GetComponentInChildren<CapsuleArm>();

        if (arm && witch.TryGetComponent<Inventory>(out var inventory))
        {
            arm.inventory = inventory;
        }

        witch.GetComponent<WitchMovement>().enabled = false;

        opening = false;
    }

    public static void OnQuitGame()
    {
        Destroy(currentMiniGame);
        currentMiniGame = null;

        // Throw trash
        currentInstance.Disable();
        currentInstance = null;

        // Re-enable witch movement
        witch.GetComponent<Rigidbody2D>().isKinematic = false;
        witch.GetComponent<WitchMovement>().enabled = true;
    }

    private void Disable()
    {
        animator.SetTrigger("Break");
        audioSource.clip = currentInstance.throwingSound;
        audioSource.Play();
        currentInstance.audioSource.volume = 0.5f;
        // Disable trash trigger
        foreach (var col in animator.GetComponents<Collider2D>())
        {
            if (col.isTrigger)
            {
                col.enabled = false;
            }
        }
    }

    [ContextMenu(nameof(Reset))]
    public void Reset()
    {
        animator.SetTrigger("Reset");
        foreach (var col in animator.GetComponents<Collider2D>())
        {
            if (col.isTrigger)
            {
                col.enabled = true;
            }
        }
    }

    private void RandomizeFood(TrashCanList trashCanList, FoodGroupSpawn[] spawns)
    {
        var rolledTrashes = new List<FoodDefinition>();

        foreach (var spawn in spawns)
        {
            var quantity = Random.Range(spawn.min, spawn.max + 1);
            for (int i = 0; i < quantity; i++)
            {
                rolledTrashes.Add(spawn.type);
            }
        }

        if (rolledTrashes.Count > trashCanList.trashes.Count)
        {
            rolledTrashes = rolledTrashes.Shuffle().Take(trashCanList.trashes.Count).ToList();
        }

        trashCanList.SpawnTrashes(rolledTrashes);
    }


    [Serializable]
    private struct FoodGroupSpawn
    {
        public FoodDefinition type;
        public int min;
        public int max;
    }
}