using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField]
    private  TextMeshProUGUI _scoreLabel;//ссылка на текст
    [SerializeField]
    private int _scorePerSquare ; //количество очков за зомби
    
    private int _currentScore =6; //общее количество очков
    

    
//Вычетаем  oбщее количество очков и записывать их в текст
    public void AddScore()
    {
        _currentScore -= _scorePerSquare;
        _scoreLabel.text = _currentScore.ToString();
    }
    
    public int GetCurrentScore()
    {
        return _currentScore;
    }
    
}

