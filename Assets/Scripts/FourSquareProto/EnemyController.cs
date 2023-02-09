using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : Competitor
{
    public override void HitBallTo(Action<BallController.BallLocation> callback)
    {
        // todo have to 
        
        int shotChooser = Random.Range(0, 4);
        switch (shotChooser)
        {
            case 0:
                callback(BallController.BallLocation.Player);
                break;
            
            case 1:
                callback(BallController.BallLocation.Enemy1);
                break;
            
            case 2:
                callback(BallController.BallLocation.Enemy2);
                break;
            
            case 3:
                callback(BallController.BallLocation.Enemy3);
                break;
        }
    }
}
