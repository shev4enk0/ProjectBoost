using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Obstacles_01_Level : MonoBehaviour
{
    public GameObject leftPanel;
    public GameObject rightPanel;
    public GameObject secondLevelPanel;

    [Range(0f, 1f)][SerializeField]float movmentRange;

    [SerializeField]Vector3 leftDir;
    [SerializeField]Vector3 rightDir;
    [SerializeField]Vector3 secondLevelDir;
    [SerializeField]float period = 5f;
  
    Vector3 startPosLeft;
    Vector3 startPosRight;

    const float tay = Mathf.PI * 2;


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
        if (period <= Mathf.Epsilon)// not to divide by 0
            return;
        float cicle = Time.time / period;
        //  movefrom-1 to +1
        float rawSin = Mathf.Sin(tay * cicle);



        movmentRange = rawSin / 2f + 0.5f; //to move from 0 to 1

        Vector3 deltaL = -movmentRange * leftDir;
        Vector3 deltaR = movmentRange * rightDir;

        leftPanel.transform.position = deltaL + startPosLeft;
        rightPanel.transform.position = deltaR + startPosRight;
    }
}
