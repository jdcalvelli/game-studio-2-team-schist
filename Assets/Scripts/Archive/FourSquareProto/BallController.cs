using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    
    public enum BallLocation
    {
        Player,
        Enemy1,
        Enemy2,
        Enemy3
    }

    public BallLocation bl;

    public void MoveBallTo(BallLocation ballLocation)
    {
        Vector3 position = gameObject.transform.position;
        Debug.Log(position);

        switch (ballLocation)
        {
            case BallLocation.Player:
                bl = BallLocation.Player;
                position.x = -3f;
                position.y = -2.5f;
                break;
            
            case BallLocation.Enemy1:
                bl = BallLocation.Enemy1;
                position.x = -3f;
                position.y = 2.5f;
                break;
            
            case BallLocation.Enemy2:
                bl = BallLocation.Enemy2;
                position.x = 3f;
                position.y = 2.5f;
                break;
            
            case BallLocation.Enemy3:
                bl = BallLocation.Enemy3;
                position.x = 3f;
                position.y = -2.5f;
                break;
        }
        
        gameObject.transform.position = position;
        
    }
}
