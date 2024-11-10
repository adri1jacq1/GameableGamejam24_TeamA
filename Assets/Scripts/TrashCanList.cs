using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class TrashCanList : MonoBehaviour
{
    public List<Trash> trashes;

    public void SpawnTrashes(List<FoodDefinition> trashDefinitions)
    {
        var randomizedTrash = trashes.Shuffle().ToList();

        for (var index = 0; index < trashDefinitions.Count; index++)
        {
            randomizedTrash[index].ApplyDefinition(trashDefinitions[index]);

            randomizedTrash[index].gameObject.SetActive(false);

            var trash = Instantiate(trashDefinitions[index].prefab, randomizedTrash[index].transform.position, quaternion.identity);
            trash.GetComponent<Trash>().foodGroup = randomizedTrash[index].foodGroup;
        }
       
    }
}