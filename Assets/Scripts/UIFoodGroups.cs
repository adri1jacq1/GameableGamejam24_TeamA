using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFoodGroups : MonoBehaviour
{
    public Inventory inventory;

    public List<TextMeshProUGUI> foodGroupLabels = new();
    
    void Start()
    {
        inventory.OnInventoryChanged += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        foreach (var inventoryItem in inventory.foodCounts)
        {
            foodGroupLabels[(int)inventoryItem.Key.group].SetText(inventoryItem.Value.ToString());
        }
    }
}