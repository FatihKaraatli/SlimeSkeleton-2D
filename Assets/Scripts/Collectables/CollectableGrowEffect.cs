using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class CollectableGrowEffect : MonoBehaviour
{
    public Timer startGrowTimer;
    public Vector3 firstScale;

    public void Start()
    {
        this.gameObject.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        startGrowTimer.UpdateTimer();
        if (!startGrowTimer.Done())
        {
            this.gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, firstScale, startGrowTimer.NormalizedTime);
        }
    }
}
