using UnityEngine;


[CreateAssetMenu(menuName = "GreenWitch/TrashDefinition", fileName = "TrashDefinition_", order = 0)]
public class TrashDefinition : ScriptableObject
{
    public Trash.FoodGroup group;
    public Sprite sprite;
    public Color color;
    public GameObject prefab;
}