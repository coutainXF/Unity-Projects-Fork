using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : Singleton<ScoreDisplay>
{
    Text scoreText;
    protected override void Awake()
    {
        base.Awake();
        scoreText = GetComponent<Text>();
    }
    void Start()
    {
        ScoreManager.Instance.Reset();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = "Score"+score.ToString();
    }
}