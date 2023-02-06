using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : Competitor
{
    public override void HitBallTo(Action<BallController.BallLocation> callback)
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Test");
            callback(BallController.BallLocation.Enemy);
        }
    }
}
