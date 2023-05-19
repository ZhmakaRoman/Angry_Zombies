using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreResalt : MonoBehaviour
{
  //  [SerializeField] private ScoreController _scoreController;

    [SerializeField] private TextMeshProUGUI _currentScoreLabel;




    private void Update()
    {
        if (Convert.ToInt32(_currentScoreLabel.text) == 0)
        {
           
        }
    }
}
