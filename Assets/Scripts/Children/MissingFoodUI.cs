using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Children
{
    public class MissingFoodUI : MonoBehaviour
    {
        [SerializeField] private Image[] _renderers;

        public void DisplayMissingFood(List<FoodDefinition> missingFood)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                if (i < missingFood.Count)
                {
                    _renderers[i].gameObject.SetActive(true);
                    _renderers[i].sprite = missingFood[i].sprite;
                }
                else
                {
                    _renderers[i].gameObject.SetActive(false);
                }
            }
        }
    }
}