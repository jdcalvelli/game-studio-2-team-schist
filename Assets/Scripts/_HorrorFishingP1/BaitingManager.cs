using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitingManager : MonoBehaviour
{

        private enum baitingSubGameStates
    {
        startBaitingGame,
        baiting,
        hookBaited,
        endSubGame,
    }

    private baitingSubGameStates _baitingSubGameState = baitingSubGameStates.startBaitingGame;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    public void BaitingSubGameUpdate()
    {
        switch (_baitingSubGameState)

        {
            case baitingSubGameStates.startBaitingGame:
                //we're ready to begin the process of baiting the hook
                _baitingSubGameState = baitingSubGameStates.baiting;
                break;

            case baitingSubGameStates.baiting:
                //we're actively baiting the hook
                //for now, this is a simple process; press the main input button, and the hook will be baited
                if (inputManager.PrimaryKeyDown())
                {
                    _baitingSubGameState = baitingSubGameStates.hookBaited;
                }

                break;

            case baitingSubGameStates.hookBaited:
                //the hook has been baited, and we're ready to move to fishing

                if (inputManager.PrimaryKeyDown())
                {
                    _baitingSubGameState = baitingSubGameStates.endSubGame;
                }

                break;
            
            case baitingSubGameStates.endSubGame:
                // bubble up a call to game manager to change state
                gameManager.SetGameState(States.GameStates.isFishing);
                break;

        }    
    }


}
