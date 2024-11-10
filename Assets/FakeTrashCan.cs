using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTrashCan : MonoBehaviour
{

    private static Transform witch;

    private bool isWitchNear = false;

    public Jumpscare jumpscare;

    public Animator animator;

    private AudioSource audioSource;
    public AudioClip openingSound, throwingSound;

    bool played = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (played)
            return;

        if (other.tag == "Player")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
            isWitchNear = true;
            witch = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
            isWitchNear = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isWitchNear && !played)
        {
            StartCoroutine(JumpscareCoroutine());
        }
    }

    IEnumerator JumpscareCoroutine()
    {
        played = true;
        witch.transform.GetChild(0).gameObject.SetActive(false);

        animator.SetTrigger("Open");

        audioSource.clip = openingSound;
        audioSource.Play();

        yield return new WaitForSeconds(0.3f);

        jumpscare.AAAAAAAAH();

        yield return new WaitForSeconds(1f);

        audioSource.clip = throwingSound;
        audioSource.Play();

        animator.SetTrigger("Break");
    }
}
