using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableEffects : MonoBehaviour
{
    public float scaleSpeed;
    public float scaleAmountDivide;
    public float movementSpeedDivide;
    public float scaleIncreaseAmount;

    void Update()
    {
        Vector3 vec = new Vector3(Mathf.Sin(Time.time * scaleSpeed) / scaleAmountDivide, Mathf.Sin(Time.time * scaleSpeed) / scaleAmountDivide, Mathf.Sin(Time.time * scaleSpeed) / scaleAmountDivide);
        float movementSpeed  = vec.y;
        vec.x += scaleIncreaseAmount;
        vec.y += scaleIncreaseAmount;
        vec.x = Mathf.Abs(vec.x);
        vec.y = Mathf.Abs(vec.y);
        vec.z = Mathf.Abs(vec.z);
        transform.localScale = vec;
        transform.localPosition = new Vector3(transform.localPosition.x , transform.localPosition.y+(movementSpeed / movementSpeedDivide) , transform.localPosition.z);
    }
}
