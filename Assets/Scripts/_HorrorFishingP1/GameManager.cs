using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // create a var with gameState enum type to track state
    private States.GameStates _gameStates;
    
    // get references to subgame managers
    [SerializeField] private FishingManager fishingManager;
    [SerializeField] private InputManager inputManager;
    void Start()
    {
        // tells the fishing manager how to get inputs
        //actually somewhat wondering if it's better to do this through the GameManager, just pass things back and forth. Maybe not.
        fishingManager.Initialize(inputManager);
        // on game start set state to be onBoat for now
        _gameStates = States.GameStates.onBoat;
    }

    // for now, switch statement that will check which state we are in and do things based on state
    void Update()
    {
        // debug log current state just for testing purposes
        //Debug.Log(_gameStates);
        
        // starting switch statement that will be checking every frame for current state of program
        // this will probably be refactored into using a lean class based state manager
        switch (_gameStates)
        {
            
            case States.GameStates.gameStart:
                // this state will could be used for intro cutscene, main menu, etc
                break;
            
            case States.GameStates.onBoat:
                // this state will be used for navigation between the areas of the game
                // ie the fishing part, the baiting part, and the putting things in the cooler part
                // need to check for input to say that we are going to move into fishing mini game (this should be moved)
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("sat down to fish");
                    _gameStates = States.GameStates.isFishing;
                }
                break;
            
            case States.GameStates.isFishing:
                // this state will be used for informing the game that we are currently in the fishing "mini game"
                // ie
                // call fishing manager update
                fishingManager.FishingSubGameUpdate();
                break;
            
            case States.GameStates.gameEnd:
                // this state will be used for telling the game that it's over
                break;
        }
    }
}
