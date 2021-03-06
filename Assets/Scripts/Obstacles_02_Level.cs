using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles_02_Level : MonoBehaviour
{
    public GameObject leftPanel;
    public GameObject rightPanel;
    public GameObject verticalPanel;

    [Range(0f, 1f)][SerializeField]float movmentRange;


    [SerializeField]Vector3 leftDir;
    [SerializeField]Vector3 rightDir;
    [SerializeField]Vector3 verticalDir;

    [SerializeField]float period = 5f;

    Vector3 startPosLeft;
    Vector3 startPosRight;
    Vector3 startPosVertical;

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
        startPosVertical = verticalPanel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovingPanels(); 
    }

    void MathConditionMovingRange()
    {
        // numbers cicles in time
        if (period <= Mathf.Epsilon)
            // not to divide by 0
            return;
        float cicle = Time.time / period;
        //  movefrom-1 to +1
        float rawSin = Mathf.Sin(tay * cicle);
        movmentRange = rawSin / 2f + 0.5f;
        //to move from 0 to 1
    }

    void MovingPanels()
    {
        MathConditionMovingRange();

        Vector3 deltaL = movmentRange * leftDir;
        Vector3 deltaR = -movmentRange * rightDir;
        Vector3 deltaV = movmentRange * verticalDir;

        leftPanel.transform.position = deltaL + startPosLeft;
        rightPanel.transform.position = deltaR + startPosRight;
        verticalPanel.transform.position = deltaV + startPosVertical;

    }
}
