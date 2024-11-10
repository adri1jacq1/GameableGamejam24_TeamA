using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    [SerializeField] private StartingValue[] startingValues;

    public event Action OnInventoryChanged;

    public Dictionary<FoodDefinition, int> foodCounts = new();


    private void Start()
    {
        foreach (var definition in startingValues)
        {
            foodCounts[definition.food] = definition.value;
        }

        OnInventoryChanged?.Invoke();
    }

    public void AddItem(FoodDefinition foodGroup)
    {
        var val = foodCounts.GetValueOrDefault(foodGroup, 0);

        foodCounts[foodGroup] = val + 1;

        OnInventoryChanged?.Invoke();
    }

    public void RemoveFood(FoodDefinition foodGroup)
    {
        var val = foodCounts.GetValueOrDefault(foodGroup, 0);

        foodCounts[foodGroup] = Mathf.Max(0, val - 1);

        OnInventoryChanged?.Invoke();
    }

    [Serializable]
    private struct StartingValue
    {
        public FoodDefinition food;
        public int value;
    }
}