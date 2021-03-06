using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localMove : MonoBehaviour
{
    public Vector3 direction;
    public float delta;

    Vector3 start;

    const float tay = Mathf.PI * 2;
    float movmentRange;
    float rawSin;


    bool up = true;

    // Use this for initialization
    void Start()
    {
        start = transform.position;
    }
	
    // Update is called once per frame
    void Update()
    {
        
        MovingPanels();
    }

    void MathConditionMovingRange()
    {
        // numbers cicles in time
//        if (period <= Mathf.Epsilon) // not to divide by 0           
//        return;
        float cicle = Time.time / 5;

        rawSin = Mathf.Sin(tay * cicle); //  movefrom-1 to +1
        movmentRange = rawSin / 2f + 0.5f;
    }

    void MovingPanels()
    {
        MathConditionMovingRange();
        if (rawSin >= 0 && up)
            transform.Translate(direction);
        else
        {
            up = !up;
            transform.Translate(-direction);
        }
       

       
    }
}
