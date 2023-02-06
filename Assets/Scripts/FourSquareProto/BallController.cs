using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    
    public enum BallLocation
    {
        Player,
        Enemy,
    }

    public BallLocation bl;

    public void MoveBallTo(BallLocation ballLocation)
    {
        Vector3 position = gameObject.transform.position;
        Debug.Log(position);
        
        if (ballLocation == BallLocation.Player)
        {
            bl = BallLocation.Player;
            position.y = -2f;
            gameObject.transform.position = position;
        }
        
        if (ballLocation == BallLocation.Enemy)
        {
            bl = BallLocation.Enemy;
            position.y = 2f;
            gameObject.transform.position = position;
        }
    }
}
