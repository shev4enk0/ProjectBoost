using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    
    const float tay = Mathf.PI * 2;

    [SerializeField]Vector3 direction;
    [SerializeField]float period = 5f;
    [SerializeField]float delayTime;


    Vector3 startPos;
    float rawSin;
    [Range(0f, 1f)]float movmentRange;
    bool up = true;
    Vector3 offset;
    bool limUp;
    bool limDown;


    // Use this for initialization
    void Start()
    {
        
        StartPosFixing();
         
    }

    void StartPosFixing()
    {
        startPos = transform.position;
    }
   
    // Update is called once per frame
    void Update()
    {
        MovingPanels();
    }

    void MathConditionMovingRange()
    {
        // numbers cicles in time
        if (period <= Mathf.Epsilon) // not to divide by 0
                return;
        float cicle = (Time.time) / period;
        rawSin = Mathf.Sin(tay * cicle);
        //  movefrom-1 to +1
        movmentRange = rawSin / 2f + 0.5f;
    }

    void MovingPanels()
    {
        MathConditionMovingRange();
//        if (Time.timeSinceLevelLoad > delayTime)
        transform.Translate(direction * rawSin * Time.deltaTime);
       
       
    }
}
