using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Children
{
    public class Child : MonoBehaviour
    {
        [SerializeField] private UIChild _uiChild;
        [SerializeField] private int _allowMissingFoodGroups;
        [SerializeField] private Animator animator;

        private Transform witch;

        private bool canWitchFeed = false;

        private List<FoodDefinition> _missingFoodGroups = new();

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
                        _uiChild.gameObject.SetActive(false);
                        canWitchFeed = true;
                    }
                    else
                    {
                        other.transform.GetChild(0).gameObject.SetActive(false);
                        canWitchFeed = true;
                        _uiChild.gameObject.SetActive(true);
                        _uiChild.DisplayMissingFood(_missingFoodGroups);
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
                        
                        animator.SetTrigger("Destroy");

                        Destroy(gameObject, 0.3f);

                        DisableAllUI(witch);
                        enabled = false;
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
            _uiChild.gameObject.SetActive(false);
            canWitchFeed = false;
        }
    }
}