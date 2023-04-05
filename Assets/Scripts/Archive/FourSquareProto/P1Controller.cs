using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : Competitor
{
    [SerializeField] private BeatManager beatManager;
    
    public override void HitBallTo(Action<BallController.BallLocation> callback)
    {
        if (beatManager.OnBeat())
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
}
