using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // create a var with gameState enum type to track state
    private States.GameStates _gameStates;
    
    // get references to subgame managers
    [SerializeField] private BaitingManagerMVP baitingManager;
    [SerializeField] private FishingManager fishingManager;
    [SerializeField] private CleaningManager cleaningManager;

    // get references to ancilary managers
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CanvasManager canvasManager;

    // booleans for tracking order
    public bool hasBaited = false;
    public bool hasFished = false;
    public bool hasCleaned = false;
    
    
    void Start()
    {
        // on game start set state to be onBoat for now
        _gameStates = States.GameStates.onBoat;
    }

    // for now, switch statement that will check which state we are in and do things based on state
    void Update()
    {
        // debug log current state just for testing purposes
        // Debug.Log(_gameStates);
        
        // starting switch statement that will be checking every frame for current state of program
        // this will probably be refactored into using a lean class based state manager
        switch (_gameStates)
        {
            
            case States.GameStates.gameStart:
                // this state will could be used for intro cutscene, main menu, etc
                break;
            
            case States.GameStates.onBoat:
                // this is a centralized state that will move the states correctly, ideally
                Debug.Log("in onBoat");

                if (!hasBaited && !hasFished && !hasCleaned)
                {
                    // set subgame state here
                    baitingManager.BaitingSubGameState = States.BaitingSubGameStates.startSubGame;
                    _gameStates = States.GameStates.isBaiting;
                    // set has baited to true at end of baiting subgame
                }
                else if (!hasFished && !hasCleaned)
                {
                    // set subgame state here
                    fishingManager.FishingSubGameState = States.FishingSubGameStates.startSubGame;
                    _gameStates = States.GameStates.isFishing;
                    // set has fished to true at end of fishing subgame
                }
                else if (!hasCleaned)
                {
                    // set subgame state here
                    cleaningManager._cleaningSubGameState = States.CleaningSubGameStates.startSubGame;
                    _gameStates = States.GameStates.isCleaning;
                    // set has cleaned to true at end of cleaning subgame
                }
                else {
                    // reset flags
                    hasBaited = false;
                    hasFished = false;
                    hasCleaned = false;
                }
                break;
            
            case States.GameStates.isBaiting:
                // this state will be used for informing the game that we are currently in the baiting "mini game"
                // ie
                // call baiting manager update
                Debug.Log("in isBaiting");
                canvasManager.SetBaitingCleaningUI();
                baitingManager.BaitingSubGameUpdate();
                break;
            
            case States.GameStates.isFishing:
                // this state will be used for informing the game that we are currently in the fishing "mini game"
                // ie
                // call fishing manager update
                Debug.Log("in isFishing");
                canvasManager.SetFishingUI();
                fishingManager.FishingSubGameUpdate();
                break;
            
            case States.GameStates.isCleaning:
                // this state will be used for informing the game that we are currently in the baiting "mini game"
                // ie
                // call baiting manager update
                Debug.Log("in isCleaning");
                canvasManager.SetBaitingCleaningUI();
                cleaningManager.CleaningSubGameUpdate();
                break;
            
            case States.GameStates.gameEnd:
                // this state will be used for telling the game that it's over
                break;
        }
    }

    // public setter for game state
    public void SetGameState(States.GameStates state)
    {
        _gameStates = state;
    }
}
