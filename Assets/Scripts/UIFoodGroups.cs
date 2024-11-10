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
    }

    private void Redraw()
    {
        foreach (var inventoryItem in inventory.items)
        {
            foodGroupLabels[(int)inventoryItem.Key].SetText(inventoryItem.Value.ToString());
        }
    }
}