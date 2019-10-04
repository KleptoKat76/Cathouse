using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFxns : MonoBehaviour
{
    public static float squareNumber (float number)
    {
        return number * number;
    }

    public static float sqRoot (float number)
    {
        return Mathf.Pow(number, .5f);
    }

    public static int returnNegativeOrPosOne(float number)
    {
        if (number < 0)
        {
            return -1;
        }
        else if (number > 0)
        {
            return 1;
        }
        else 
        {
            throw new System.ArgumentException("Cannot calculate values of zero");
        }
    }
}
