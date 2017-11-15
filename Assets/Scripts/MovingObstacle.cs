using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField]float movmentRange;
    [SerializeField]Vector3 direction;
    [SerializeField]float period = 5f;

    Vector3 startPos;
    const float tay = Mathf.PI * 2;

    // Use this for initialization
    void Start()
    {
        StartPosFixing();
    }

    void StartPosFixing()
    {
        startPos = (transform.position);
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
        float cicle = Time.time / period;
       
        float rawSin = Mathf.Sin(tay * cicle); //  movefrom-1 to +1
        movmentRange = rawSin / 2f + 0.5f;
    }

    void MovingPanels()
    {
        MathConditionMovingRange();
//        Vector3 delta = movmentRange * direction;
        Vector3 delta = transform.InverseTransformVector(direction * movmentRange);
        transform.position = delta + (startPos);
        ;

    }
}
