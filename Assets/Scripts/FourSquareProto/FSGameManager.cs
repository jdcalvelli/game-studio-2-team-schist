using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Drawing.Color;

public class FSGameManager : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private P1Controller p1Controller;
    [SerializeField] private EnemyController enemyController;
    [SerializeField] private BeatManager beatManager;
    // this is a great example of terrible practice that will change later
    [SerializeField] private BeatPingView beatPingView;
    
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
                ballController.MoveBallTo(BallController.BallLocation.Enemy1);
                gs = GameStates.GameInProgress;
                beatManager.Initialize();
                break;
            case GameStates.GameInProgress:
                switch (ballController.bl)
                {
                    case BallController.BallLocation.Enemy1:
                        enemyController.HitBallTo(ballController.MoveBallTo);
                        break;
                    case BallController.BallLocation.Enemy2:
                        enemyController.HitBallTo(ballController.MoveBallTo);
                        break;
                    case BallController.BallLocation.Enemy3:
                        enemyController.HitBallTo(ballController.MoveBallTo);
                        break;
                    case BallController.BallLocation.Player:
                        p1Controller.HitBallTo(ballController.MoveBallTo);
                        break;
                }

                if (beatManager.OnBeat())
                {
                    beatPingView.ChangeColor(UnityEngine.Color.red);
                }
                else
                {
                    beatPingView.ChangeColor(UnityEngine.Color.white);
                }

                break;
        }
    }
}
