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
            callback(BallController.BallLocation.Enemy1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            callback(BallController.BallLocation.Enemy2);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            callback(BallController.BallLocation.Enemy3);
        }
    }
}
