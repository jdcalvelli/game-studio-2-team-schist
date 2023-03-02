using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningManager : MonoBehaviour
{
    private enum cleaningSubGameStates {
        startSubGame,
        unhookFish,
        pickUpKnife,
        cutHead,
        sliceBelly,
        pullInnards,
        shaveScales,
        storeInCooler
    }

    private cleaningSubGameStates _cleaningSubGameState = cleaningSubGameStates.startSubGame;

    private InputManager inputManager;

    public void Initialize(InputManager _im)
    {
        inputManager = _im;
    }

    public void CleaningSubGameUpdate() {
        switch (_cleaningSubGameState)
        {
            case (cleaningSubGameStates.startSubGame):
                // Press spacebar to unhook fish
                if (inputManager.PrimaryKeyDown()) {
                        //Play animation of closeup of fish on hook *here*
                        Debug.Log("The fish stares back with a hook through its mouth");
                        _cleaningSubGameState = cleaningSubGameStates.unhookFish;
                }

            case (cleaningSubGameStates.unhookFish):
                UnhookFish();
            
            case (cleaningSubGameStates.pickUpKnife):
                if (inputManager.PrimaryKeyDown()) {
                    //Play animation of knife being picked up from offscreen *here*
                    _cleaningSubGameState = cleaningSubGameStates.cutHead;
                }

            case (cleaningSubGameStates.cutHead):
                CutHead();

            case (cleaningSubGameStates.sliceBelly):
                SliceBelly();

            case (cleaningSubGameStates.pullInnards):

            case (cleaningSubGameStates.shaveScales):

            case (cleaningSubGameStates.storeInCooler):
        }
    }

    private void UnhookFish() {
        // Add fish unhooking logic, full subgame or just button press?
        // Probably change later, but spacebar for now
        if (inputManager.PrimaryKeyDown()) {
            //Play unhooking animation from hook view *here*
            Debug.Log("Hook removed from fish");

            //Move state change outside once unhooking is fleshed out
            _cleaningSubGameState = cleaningSubGameStates.pickUpKnife;
        }
    }

    private void CutHead() {
        // Add cutting head logic
        if (inputManager.PrimaryKeyDown()) {
            //Play head cutting animation from caught fish view *here*
            Debug.Log("The fish has been beheaded.");

            //Move state change outside once head cutting is fleshed out
            _cleaningSubGameState = cleaningSubGameStates.sliceBelly;
        }
    }

    private void SliceBelly() {
        // Add belly slicing logic
        if (inputManager.PrimaryKeyDown()) {
            //Play belly slicing animation from caught fish view *here*
            Debug.Log("Its belly has been sliced open.");

            // ""
            _cleaningSubGameState = cleaningSubGameStates.pullInnards;
        }
    }
}
