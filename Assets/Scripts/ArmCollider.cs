using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmCollider : MonoBehaviour
{
    public CapsuleArm arm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.LogError(col.name);
        if (col.tag == "Wall")
        {
            arm.HitAWall();
        }
    }    
}
