using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class EndGameRotateController : MonoBehaviour
{
    public Timer timer;
    public Vector3 lastScale;

    public void Update()
    {
        timer.UpdateTimer();
        if (!timer.Done())
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, -180), Quaternion.Euler(0,0,0), timer.NormalizedTime);
            this.gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, lastScale, timer.NormalizedTime);
        }
    }
}
