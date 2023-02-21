using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    // states related to fishing minigame
    // could be moved into another class later
    private enum fishingSubGameStates
    {
        startSubGame,
        rodCast,
        waitingForBite,
        biteRegistered,
        rhythmicReeling,
        fishCaught,
        fishLost,
    }

    private fishingSubGameStates _fishingSubGameState = fishingSubGameStates.startSubGame;

    // for hook setting - expose to editor perhaps
    private float hookSetTimer = 0f;
    private float timeToSetHook = 1f;

    private InputManager inputManager;

    //an initialization class that sets up the correct managers
    public void Initialize(InputManager _im)
    {
        inputManager = _im;
    }

    // creating fishing update that will be called by game manager
    public void FishingSubGameUpdate()
    {
        switch (_fishingSubGameState)
        {
            case fishingSubGameStates.startSubGame:
                // on spacebar press cast the line
                // once again, this should be moved into an input manager
                if (inputManager.PrimaryKeyDown())
                {
                    CastRod();
                }
                break;
            
            case fishingSubGameStates.rodCast:
                // unused now but might be relevant later
                break;
            
            case fishingSubGameStates.waitingForBite:
                // over time increase the chances of getting a bite
                Debug.Log("waiting for bite");
                WaitingForBite();
                break;
            
            case fishingSubGameStates.biteRegistered:
                // you have a certain amount of time to hook the fish
                BiteRegistered();
                break;
            
            case fishingSubGameStates.rhythmicReeling:
                // this is ultimately where the rhythmic element will come in
                // because this isnt figured out yet, i'm just sending it straight to you caught the fish if you
                // set the hook
                _fishingSubGameState = fishingSubGameStates.fishCaught;
                break;
            
            case fishingSubGameStates.fishCaught:
                Debug.Log("congrats you caught the fish");
                // restart game
                _fishingSubGameState = fishingSubGameStates.startSubGame;
                break;
            
            case fishingSubGameStates.fishLost:
                // restart game
                _fishingSubGameState = fishingSubGameStates.startSubGame;
                break;
        }
    }
    
    // when fishing, there should be a chance for a bite, then player needs to hook, then they need to reel

    private void CastRod()
    {
        // this should also ultimately trigger anims on the fishingRodView - remember model view controller :D
        // all it does for now is switch between states
        _fishingSubGameState = fishingSubGameStates.rodCast;
        Debug.Log("line cast!");
        _fishingSubGameState = fishingSubGameStates.waitingForBite;
    }

    private void WaitingForBite()
    {
        // every five-ish seconds
        if (Mathf.FloorToInt(Time.time) % 5 == 0)
        {
            if (CheckFishIsBiting())
            {
                Debug.Log("you have a bite, now set the hook");
                _fishingSubGameState = fishingSubGameStates.biteRegistered;
            }
        }
    }

    private bool CheckFishIsBiting()
    {
        // 50 50 chance of catching a fish every 3 seconds
        // odds can, and probably should be amended later
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BiteRegistered()
    {
        // you have 1 second to secure the hook before you lose the fish
        hookSetTimer += Time.deltaTime;

        if (hookSetTimer > timeToSetHook)
        {
            Debug.Log("fish lost");
            // go to fishlost state
            _fishingSubGameState = fishingSubGameStates.fishLost;
            // reset hook set timer
            hookSetTimer = hookSetTimer - timeToSetHook;
        }
        else
        {
            // listen for space bar input
            // should go into input manager
            if (inputManager.PrimaryKeyDown())
            {
                // fish has been set
                Debug.Log("hook has been set, now reel");
                _fishingSubGameState = fishingSubGameStates.rhythmicReeling;
            }
        }
    }
}