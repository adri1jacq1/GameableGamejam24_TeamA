using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{

    public bool collectable;

    public Animator animator;

    public FoodDefinition foodGroup;

    [SerializeField] private SpriteRenderer _renderer;

    public FoodDefinition Collect()
    {
        if (collectable)
        {
            Destroy(this.gameObject, 0.3f);

            animator.SetTrigger("Destroy");
        }

        return foodGroup;
    }

    public void ApplyDefinition(FoodDefinition definition)
    {
        _renderer.sprite = definition.sprite;
        _renderer.color = Color.white;
        _renderer.transform.localScale = Vector3.one;
        
        foodGroup = definition;
        collectable = true;
    }
}
