using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//when something gets into the altar, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{
    public class PropsAltar1 : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private List<Color> targetColors;
        private List<Color> currentColors;

        private void Awake()
        {
            targetColors = new List<Color>();
            currentColors = new List<Color>();

            foreach (var r in runes)
            {
                // Initialize target and current colors with each rune's original color
                targetColors.Add(r.color);
                currentColors.Add(r.color);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            for (int i = 0; i < targetColors.Count; i++)
            {
                Color targetColor = targetColors[i];
                targetColor.a = 1.0f; // Set alpha to 1
                targetColors[i] = targetColor;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            for (int i = 0; i < targetColors.Count; i++)
            {
                Color targetColor = targetColors[i];
                targetColor.a = 0.0f; // Set alpha to 0
                targetColors[i] = targetColor;
            }
        }

        private void Update()
        {
            for (int i = 0; i < runes.Count; i++)
            {
                // Lerp each rune's current color to its respective target color
                currentColors[i] = Color.Lerp(currentColors[i], targetColors[i], lerpSpeed * Time.deltaTime);
                runes[i].color = currentColors[i];
            }
        }
    }
}