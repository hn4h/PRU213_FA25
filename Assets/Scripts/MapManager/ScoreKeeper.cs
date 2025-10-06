using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionCollect = 0;
    
    public void IncrementScore()
    {
        correctAnswers++;
    }
    public void IncrementQuestionCollect()
    {
        questionCollect++;
    }

    public int GetQuestionCollect()
    {
        return questionCollect;
    }

    public int GetScore()
    {
        return correctAnswers;
    }

}
