using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HorizontalCross : MonoBehaviour
{
    public float speed;

    public float switchTime;
    public float switchTimer;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        switchTimer = 0;

        animator.speed = Random.Range(animator.speed - 0.1f, animator.speed + 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        switchTimer += Time.deltaTime * Random.Range(0.9f, 1.1f);

        if (switchTimer >= switchTime)
        {
            switchTimer = 0;
            speed = -speed;
        }
    }

}
