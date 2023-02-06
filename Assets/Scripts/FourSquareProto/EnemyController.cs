using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Competitor
{
    public override void HitBallTo(Action<BallController.BallLocation> callback)
    {
        callback(BallController.BallLocation.Player);
    }
}
