using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TrashOpening : MonoBehaviour
{
    public GameObject trashMiniGamePrefab;

    private static GameObject currentMiniGame = null;
    private static Transform witch;

    private bool isWitchNear = false;

    [SerializeField] private FoodGroupSpawn[] _spawns;

    public Animator animator;
    private bool opening = false;

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

        animator.SetTrigger("Open");

        currentMiniGame = Instantiate(trashMiniGamePrefab);
        foreach (SpriteRenderer sr in currentMiniGame.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.sortingOrder += 20;
        }

        // Disable with movement
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
    }

    public static void OnQuitGame()
    {
        Destroy(currentMiniGame);

        // Disable with movement
        witch.GetComponent<Rigidbody2D>().isKinematic = false;
        witch.GetComponent<WitchMovement>().enabled = true;
    }

    private void RandomizeFood(TrashCanList trashCanList, FoodGroupSpawn[] spawns)
    {
        var rolledTrashes = new List<TrashDefinition>();

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

        var randomizedTrash = trashCanList.trashes.Shuffle().ToList();

        for (var index = 0; index < rolledTrashes.Count; index++)
        {
            randomizedTrash[index].ApplyDefinition(rolledTrashes[index]);
        }
    }


    [Serializable]
    private struct FoodGroupSpawn
    {
        public TrashDefinition type;
        public int min;
        public int max;
    }
}