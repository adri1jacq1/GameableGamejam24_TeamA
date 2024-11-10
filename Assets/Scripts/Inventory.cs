using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action OnInventoryChanged;

    public Dictionary<Trash.FoodGroup, int> items = new();


    public void AddItem(Trash.FoodGroup foodGroup)
    {
        var val = items.GetValueOrDefault(foodGroup, 0);

        items[foodGroup] = val + 1;
        
        OnInventoryChanged?.Invoke();
    }
}