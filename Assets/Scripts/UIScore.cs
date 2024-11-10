using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    public Score score;

    public TextMeshProUGUI scoreLabel = new();

    void Start()
    {
        score.OnScoreChanged += Redraw;
        Redraw();
    }

    private void Redraw()
    {
        scoreLabel.SetText(score.score.ToString());
    }
}