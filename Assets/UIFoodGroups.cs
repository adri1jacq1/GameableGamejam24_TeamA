using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFoodGroups : MonoBehaviour
{
    public List<TextMeshProUGUI> foodGroupLabels = new();

    public List<int> foodGroupsCount = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var group in Enum.GetNames(typeof(Trash.FoodGroup)))
        {
            foodGroupsCount.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(Trash.FoodGroup foodGroupIndex)
    {
        foodGroupsCount[(int)foodGroupIndex] ++;
        foodGroupLabels[(int) foodGroupIndex].SetText(foodGroupsCount[(int)foodGroupIndex].ToString());
    }

    public void GoBack()
    {
        Debug.LogError("WE WANT TO LEAVE BUT IT'S NOT IMPLEMENTED YET, WOMP WOMP");
    }
}
