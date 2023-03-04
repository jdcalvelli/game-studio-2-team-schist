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
        storeInCooler,
        endSubGame;
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
                break;

            case (cleaningSubGameStates.unhookFish):
                UnhookFish();
                break;
            
            case (cleaningSubGameStates.pickUpKnife):
            // Maybe exclude this state and add the animation to the start of the subgame or the unhooking ?
                if (inputManager.PrimaryKeyDown()) {
                    //Play animation of knife being picked up from offscreen *here*
                    Debug.Log("Knife has been picked up");
                    _cleaningSubGameState = cleaningSubGameStates.cutHead;
                }
                break;

            case (cleaningSubGameStates.cutHead):
                CutHead();
                break;

            case (cleaningSubGameStates.sliceBelly):
                SliceBelly();
                break;

            case (cleaningSubGameStates.pullInnards):
                PullInnards();
                break;

            case (cleaningSubGameStates.shaveScales):
                ShaveScales();
                break;

            case (cleaningSubGameStates.storeInCooler):
                StoreInCooler();
                _cleaningSubGameState = cleaningSubGameStates.endSubGame;
                break;
                //end subgame
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

            // Move state change outside after fleshing out
            _cleaningSubGameState = cleaningSubGameStates.pullInnards;
        }
    }

    private void PullInnards() {
        // Add pull innards logic
        if (inputManager.PrimaryKeyDown()) {
            //Play innard pulling animation from caught fish view *here*
            Debug.Log("The fish has been gutted.");

            // Move state change outside after fleshing out
            _cleaningSubGameState = cleaningSubGameStates.shaveScales;
        }
    }

    private void ShaveScales() {
        // Add scale shaving logic
        if (inputManager.PrimaryKeyDown()) {
            //Play scale shaving animation from caught fish view *here*
            Debug.Log("Scales have been carved off the fish.");

            // Move state change outside after fleshing out
            _cleaningSubGameState = cleaningSubGameStates.storeInCooler;
        }
    }

    private void StoreInCooler() {
        // Add logic for storing fish in cooler
        if (inputManager.PrimaryKeyDown()) {
            //Play store in cooler animation from caught fish view *here*
            Debug.Log("Fish has been stored in the cooler");
        }
    }
}
