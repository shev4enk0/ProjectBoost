using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingObstacl : MonoBehaviour
{
    public GameObject leftPanel;
    public GameObject rightPanel;
    public GameObject secondLevelPanel;

    [Range(0f, 1f)][SerializeField]float movmentRange;

    [SerializeField]Vector3 directionHor;
    [SerializeField]Vector3 directionVer;
    [SerializeField]Quaternion secondLevelDir;
  
    Vector3 startPosLeft;
    Vector3 startPosRight;

    const float tay = Mathf.PI * 2;
    float period = 5f;

    // Use this for initialization
    void Start()
    {
        StartPosFixing();
    }

    void StartPosFixing()
    {
        startPosLeft = leftPanel.transform.position;
        startPosRight = rightPanel.transform.position;
    }
	
    // Update is called once per frame
    void Update()
    {
        MovingPanels(); 
    }

    void MovingPanels()
    {   // numbers cicles in time
        if (period <= Mathf.Epsilon)
            return;
        float cicle = Time.time / period;
        // from-1 to +1
        float rawSin = Mathf.Sin(tay * cicle);

        movmentRange = rawSin / 2f + 0.5f; 

        Vector3 deltaL = -movmentRange * directionHor;
        Vector3 deltaR = movmentRange * directionVer;

        leftPanel.transform.position = deltaL + startPosLeft;
        rightPanel.transform.position = deltaR + startPosRight;
    }
}
