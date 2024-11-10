using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CapsuleArm : MonoBehaviour
{
    public Transform arm;
    public int movementLimit;
    public float heightLimit;
    public bool leftOrRight;
    public float armSpeed;
    public float goingDownSpeed;
    public float goingUpSpeed;
    public bool goingDownInput;

    public Inventory inventory;
    
    public enum ClawState
    {
        Moving,
        GoingDown,
        GoneDown,
        GoingUp,
        Closing
    }

    public ClawState clawState;

    private float armOrigin;
    private float armTarget;

    public Animator animator;

    public float closingTime;
    public float closingTimer;

    public CircleCollider2D armCollider;
    public Transform trashParent;
    public UIFoodGroups uIFoodGroups;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        clawState = ClawState.Moving;

        armOrigin = arm.position.y;
        armTarget = armOrigin - heightLimit;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TrashOpening.OnQuitGame();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (clawState == ClawState.Moving)
            {
                clawState = ClawState.GoingDown;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (clawState == ClawState.GoingDown || clawState == ClawState.GoneDown)
            {
                Capture();
                if (audioSource != null)
                    audioSource.Play();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (clawState == ClawState.Moving)
        {
            if (arm.position.x > movementLimit || arm.position.x < -movementLimit)
            {
                leftOrRight = !leftOrRight;
            }

            if (leftOrRight)
            {
                arm.position += new Vector3(armSpeed * Time.deltaTime,0,0);
            }
            else
            {
                arm.position += new Vector3(-armSpeed * Time.deltaTime,0,0);
            }
        }

        if (clawState == ClawState.GoingDown)
        {
            if (arm.position.y < armTarget)
            {
                clawState = ClawState.GoneDown;
                arm.position = new Vector3(arm.position.x, armTarget, arm.position.y);
            }

            arm.position += new Vector3(0,- goingDownSpeed * Time.deltaTime, 0);
        }

        if (clawState == ClawState.GoingUp)
        {
            if (arm.position.y > armOrigin)
            {
                clawState = ClawState.Moving;
                arm.position = new Vector3(arm.position.x, armOrigin, arm.position.y);

                OpenClaw();
            }

            arm.position += new Vector3(0, goingUpSpeed * Time.deltaTime, 0);
        }

        if (clawState == ClawState.Closing)
        {
            closingTimer += Time.deltaTime;

            if (closingTimer >= closingTime)
            {
                clawState = ClawState.GoingUp;
            }
        }
    }

    public List<Transform> caughtObjects = new List<Transform>();

    private void Capture()
    {
        clawState = ClawState.Closing;
        closingTimer = 0;
        animator.SetBool("close", true);

        var colliders = new List<Collider2D>();
        Physics2D.OverlapCollider(armCollider, new ContactFilter2D(), colliders);

        caughtObjects.Clear();
        foreach(Collider2D collider in colliders)
        {
            if (collider.tag != "Trash")
            {
                continue;
            }

            var caughtTransform = collider.transform;
            caughtObjects.Add(caughtTransform);

            caughtTransform.parent = armCollider.transform;

            caughtTransform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    private void OpenClaw()
    {
        animator.SetBool("close", false);

        foreach (Transform t in caughtObjects)
        {
            var trash = t.GetComponent<Trash>();

            if (trash != null && trash.collectable)
            {
                var foodGroup = trash.Collect();
                inventory.AddItem(foodGroup);
            }
            else 
            {
                t.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

            t.parent = trashParent;
        }

        caughtObjects.Clear();
    }

    public void HitAWall()
    {
        clawState = ClawState.GoneDown;
    }

    public void HitBadStuff()
    {
        clawState = ClawState.GoingUp;
    }
}
