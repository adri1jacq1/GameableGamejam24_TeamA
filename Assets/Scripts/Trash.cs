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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FoodGroup Collect()
    {
        if (collectable)
        {
            Destroy(this.gameObject, 0.3f);

            animator.SetTrigger("Destroy");
        }

        return foodGroup;
    }
}
