using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Children
{
    public class Child : MonoBehaviour
    {
        [SerializeField] private MissingFoodUI missingFoodUI;
        [SerializeField] private GameObject canFeedUI;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;
        
        [SerializeField] private int _allowMissingFoodGroups;
        [SerializeField] private int _scoreValue;

        private Transform witch;

        private bool canWitchFeed = false;
        public bool isFed = false;

        private List<FoodDefinition> _missingFoodGroups = new();

        void OnEnable()
        {
            isFed = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                witch = other.transform;
                if (other.TryGetComponent<Inventory>(out var inventory))
                {
                    if (CanFeed(inventory, _missingFoodGroups))
                    {
                        other.transform.GetChild(0).gameObject.SetActive(true);
                        missingFoodUI.gameObject.SetActive(false);
                        canFeedUI.SetActive(true);
                        canWitchFeed = true;
                    }
                    else
                    {
                        other.transform.GetChild(0).gameObject.SetActive(false);
                        canWitchFeed = true;
                        missingFoodUI.gameObject.SetActive(true);
                        canFeedUI.SetActive(false);
                        missingFoodUI.DisplayMissingFood(_missingFoodGroups);
                    }
                }
            }
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && canWitchFeed)
            {
                if (witch.TryGetComponent<Inventory>(out var inventory))
                {
                    if (CanFeed(inventory, _missingFoodGroups))
                    {
                        TakeFood(inventory);

                        if (witch.TryGetComponent<Score>(out var score))
                        {
                            score.AddScore(_scoreValue);
                        }
                        
                        audioSource.Play();
                        animator.SetTrigger("Eat");

                        DisableAllUI(witch);

                        gameObject.SetActive(false);
                    }
                    else
                    {
                        canWitchFeed = false;
                    }
                }
            }
        }

        private void TakeFood(Inventory inventory)
        {
            foreach (var foodCount in inventory.foodCounts.ToList())
            {
                if (foodCount.Value > 0)
                {
                    inventory.RemoveFood(foodCount.Key);
                }
            }
            isFed = true;
        }

        private bool CanFeed(Inventory inventory, List<FoodDefinition> missingFoodGroups)
        {
            missingFoodGroups.Clear();

            foreach (var foodCount in inventory.foodCounts)
            {
                if (foodCount.Value < 1)
                {
                    missingFoodGroups.Add(foodCount.Key);
                }
            }

            return missingFoodGroups.Count <= _allowMissingFoodGroups;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                DisableAllUI(other.transform);
            }
        }

        private void DisableAllUI(Transform witchTransform)
        {
            witchTransform.GetChild(0).gameObject.SetActive(false);
            missingFoodUI.gameObject.SetActive(false);
            canFeedUI.SetActive(false);
            canWitchFeed = false;
        }

        public bool IsFed()
        {
            return isFed;
        }
    }
}