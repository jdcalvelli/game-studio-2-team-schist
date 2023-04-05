using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : Competitor
{
    [SerializeField] private BeatManager beatManager;
    
    public override void HitBallTo(Action<BallController.BallLocation> callback)
    {
        // todo - have to change it such that the enemy cant send it to themselves
        // todo - have to make it so that the enemies only make a shot once within an interval
        
        if (beatManager.OnBeat())
        {
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
}
