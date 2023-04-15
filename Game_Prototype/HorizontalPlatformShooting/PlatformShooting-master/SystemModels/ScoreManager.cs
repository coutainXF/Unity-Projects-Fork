using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] float comboInterval;
    
    static int score;
    int currentScore;

    int combo;
    bool inCombo;//是否正在连击

    public float comboLefttime;

    void Start()
    {
        StartCoroutine(nameof(CheckCombo));
    }

    public void Reset()
    {
        score = 0;
        currentScore = 0;
        combo = 0;
        inCombo = false;
        ScoreDisplay.Instance.UpdateScore(score);
        ComboDisplay.Instance.UpdateCombo(combo);
    }

    public void AddScore(int scorePoint)
    {
        score += scorePoint;
        StartCoroutine(nameof(AddScoreCoroutine));
    }

    IEnumerator AddScoreCoroutine()
    {
        while (currentScore<score)
        {
            currentScore += 1;
            ScoreDisplay.Instance.UpdateScore(currentScore);
            yield return null;
        }
    }

    public void AddCombo(int combo)
    {
        inCombo = true;
        this.combo += combo;
        comboLefttime = comboInterval;
        ComboDisplay.Instance.UpdateCombo(this.combo);
    }

    IEnumerator CheckCombo()
    {
        while (gameObject.activeSelf)
        {
            comboLefttime -= Time.deltaTime;
            if (comboLefttime < 0)
            {
                combo = 0;
                ComboDisplay.Instance.UpdateCombo(this.combo);
            }
            yield return null;
        }
    }
}
