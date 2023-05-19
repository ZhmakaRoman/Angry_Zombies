using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubber : MonoBehaviour
{
    
    public LineRenderer[] _LineRenderers;
    public Transform[] _stripPositions;
    public Transform _defaultPosition;
   


    private bool _isMouseDown;
    private void Start()
    {
        _LineRenderers[0].positionCount = 2;
        _LineRenderers[1].positionCount = 2;
        _LineRenderers[0].SetPosition(0,_stripPositions[0].position);
        _LineRenderers[1].SetPosition(0,_stripPositions[1].position);
        SetStrips(_defaultPosition.position);
    }

  
    private void Update()
    {

        if (_isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;
           mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            SetStrips(mousePosition);
        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        _isMouseDown = true;
    }

    private void OnMouseUp()
    {
        _isMouseDown = false;
    }

    private void ResetStrips()
    {
        SetStrips(_defaultPosition.position);
    }

    private void SetStrips(Vector3 position)
    {
        _LineRenderers[0].SetPosition(1,position);
        _LineRenderers[1].SetPosition(1,position);
        
    }
}
