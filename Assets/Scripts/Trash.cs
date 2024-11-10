using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        _renderer.transform.parent.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        Debug.LogError( _renderer.transform.localScale);
        
        foodGroup = definition.group;
        collectable = true;
    }
}
