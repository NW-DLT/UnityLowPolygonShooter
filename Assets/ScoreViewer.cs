using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;
using System;

public class ScoreViewer : MonoBehaviour
{
    public static ScoreViewer instance;
    [SerializeField] TextMeshProUGUI score;
    public static float scoreValue;
    private void Start()
    {
        score.text = $"Score: {scoreValue}";
    }
    private void Awake()
    {
        instance = this;
    }
    public void AddPoint()
    {
        scoreValue += 1;
        score.text = $"Score: {scoreValue}";

    }
    public void RemovePoint()
    {
        scoreValue -= 1;
        score.text = $"Score: {scoreValue}";
    }
}
