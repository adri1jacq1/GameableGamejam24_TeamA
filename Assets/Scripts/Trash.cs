using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum FoodGroup
    {
        Grain,
        Fruit,
        Vegetable,
        Protein        
    }

    public bool collectable;

    public Animator animator;

    public FoodGroup foodGroup;

    [SerializeField] private SpriteRenderer _renderer;

    public FoodGroup Collect()
    {
        if (collectable)
        {
            Destroy(this.gameObject, 0.3f);

            animator.SetTrigger("Destroy");
        }

        return foodGroup;
    }

    public void ApplyDefinition(TrashDefinition definition)
    {
        _renderer.sprite = definition.sprite;
        _renderer.color = Color.white;
        _renderer.transform.localScale = Vector3.one;
        
        foodGroup = definition.group;
        collectable = true;
    }
}
