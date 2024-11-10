using UnityEngine;


[CreateAssetMenu(menuName = "GreenWitch/TrashDefinition", fileName = "TrashDefinition_", order = 0)]
public class FoodDefinition : ScriptableObject
{
    public FoodGroup group;
    public Sprite sprite;


    public enum FoodGroup
    {
        Grain,
        Fruit,
        Vegetable,
        Protein
    }
}