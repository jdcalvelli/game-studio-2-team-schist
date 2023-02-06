using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private P1Controller p1Controller;
    [SerializeField] private EnemyController enemyController;
    
    public enum GameStates
    {
        GameStart,
        GameInProgress,
        GameEnd
    }

    public GameStates gs;
    
    // Start is called before the first frame update
    void Start()
    {
        gs = GameStates.GameStart;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gs)
        {
            case GameStates.GameStart:
                ballController.MoveBallTo(BallController.BallLocation.Enemy);
                gs = GameStates.GameInProgress;
                break;
            case GameStates.GameInProgress:
                if (ballController.bl == BallController.BallLocation.Enemy)
                {
                    enemyController.HitBallTo(ballController.MoveBallTo);
                }
                else if (ballController.bl == BallController.BallLocation.Player)
                {
                    p1Controller.HitBallTo(ballController.MoveBallTo);
                }
                break;
        }
    }
}
