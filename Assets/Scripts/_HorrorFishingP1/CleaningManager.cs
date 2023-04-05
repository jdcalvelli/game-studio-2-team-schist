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
        endSubGame,
    }

    private cleaningSubGameStates _cleaningSubGameState = cleaningSubGameStates.startSubGame;


    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CanvasManager canvasManager;

    [SerializeField] private GameObject _cleaningViewsContainer;

    [SerializeField] private CleaningViewMVP _cleaningView;

    public void CleaningSubGameUpdate() {
        switch (_cleaningSubGameState)
        {
            case (cleaningSubGameStates.startSubGame):
                _cleaningViewsContainer.SetActive(true);
                canvasManager.ActivateText(CanvasManager.textPositions.bottomCenter);
                canvasManager.SetText(CanvasManager.textPositions.bottomCenter, "PRESS SPACE TO UNHOOK");
                // Press spacebar to unhook fish
                if (inputManager.PrimaryKeyDown()) {
                        //Play animation of closeup of fish on hook *here*
                        _cleaningSubGameState = cleaningSubGameStates.unhookFish;
                }
                break;

            case (cleaningSubGameStates.unhookFish):
                canvasManager.DeactivateText(CanvasManager.textPositions.bottomCenter);
                StartCoroutine(UnhookFish());
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
                break;
                //end subgame
            
            case (cleaningSubGameStates.endSubGame):
                _cleaningViewsContainer.SetActive(false);
                _cleaningView.ResetCleaningView();

                EndSubGame();

                break;
        }
    }

    private void EndSubGame() {
        // set game manager flag
        gameManager.hasCleaned = true;

        // move to central global state
        gameManager.SetGameState(States.GameStates.onBoat);

        // reset subgame state
        _cleaningSubGameState = cleaningSubGameStates.startSubGame;
    }

    private IEnumerator UnhookFish() {
        _cleaningView.Animate_UnhookFish();
        yield return new WaitForSeconds(2f);
        // bypass rest of states and go straight to end
        _cleaningSubGameState = cleaningSubGameStates.endSubGame;
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
            _cleaningSubGameState = cleaningSubGameStates.endSubGame;
        }
    }
}
