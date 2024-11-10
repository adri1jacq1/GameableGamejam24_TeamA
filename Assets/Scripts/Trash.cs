using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        _renderer.transform.parent.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        Debug.LogError( _renderer.transform.localScale);
        
        foodGroup = definition;
        collectable = true;
    }
}
