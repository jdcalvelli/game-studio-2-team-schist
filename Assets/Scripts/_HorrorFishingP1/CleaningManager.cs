using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningManager : MonoBehaviour
{

    public States.CleaningSubGameStates _cleaningSubGameState = States.CleaningSubGameStates.startSubGame;


    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CanvasManager canvasManager;

    [SerializeField] private GameObject _cleaningViewsContainer;

    [SerializeField] private CleaningViewMVP _cleaningView;

    public void CleaningSubGameUpdate() {
        switch (_cleaningSubGameState)
        {
            case (States.CleaningSubGameStates.startSubGame):
                _cleaningViewsContainer.SetActive(true);
                canvasManager.ActivateText(CanvasManager.textPositions.bottomCenter);
                canvasManager.SetText(CanvasManager.textPositions.bottomCenter, "PRESS SPACE TO UNHOOK");
                // Press spacebar to unhook fish
                if (inputManager.PrimaryKeyDown()) {
                        //Play animation of closeup of fish on hook *here*
                        _cleaningSubGameState = States.CleaningSubGameStates.unhookFish;
                }
                break;

            case (States.CleaningSubGameStates.unhookFish):
                canvasManager.DeactivateText(CanvasManager.textPositions.bottomCenter);
                StartCoroutine(UnhookFish());
                break;
            
            case (States.CleaningSubGameStates.pickUpKnife):
            // Maybe exclude this state and add the animation to the start of the subgame or the unhooking ?
                if (inputManager.PrimaryKeyDown()) {
                    //Play animation of knife being picked up from offscreen *here*
                    Debug.Log("Knife has been picked up");
                    _cleaningSubGameState = States.CleaningSubGameStates.cutHead;
                }
                break;

            case (States.CleaningSubGameStates.cutHead):
                CutHead();
                break;

            case (States.CleaningSubGameStates.sliceBelly):
                SliceBelly();
                break;

            case (States.CleaningSubGameStates.pullInnards):
                PullInnards();
                break;

            case (States.CleaningSubGameStates.shaveScales):
                ShaveScales();
                break;

            case (States.CleaningSubGameStates.storeInCooler):
                StoreInCooler();
                break;
                //end subgame
            
            case (States.CleaningSubGameStates.endSubGame):
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
    }

    private IEnumerator UnhookFish() {
        _cleaningView.Animate_UnhookFish();
        yield return new WaitForSeconds(2f);
        // bypass rest of states and go straight to end
        _cleaningSubGameState = States.CleaningSubGameStates.endSubGame;
    }

    private void CutHead() {
        // Add cutting head logic
        if (inputManager.PrimaryKeyDown()) {
            //Play head cutting animation from caught fish view *here*
            Debug.Log("The fish has been beheaded.");

            //Move state change outside once head cutting is fleshed out
            _cleaningSubGameState = States.CleaningSubGameStates.sliceBelly;
        }
    }

    private void SliceBelly() {
        // Add belly slicing logic
        if (inputManager.PrimaryKeyDown()) {
            //Play belly slicing animation from caught fish view *here*
            Debug.Log("Its belly has been sliced open.");

            // Move state change outside after fleshing out
            _cleaningSubGameState = States.CleaningSubGameStates.pullInnards;
        }
    }

    private void PullInnards() {
        // Add pull innards logic
        if (inputManager.PrimaryKeyDown()) {
            //Play innard pulling animation from caught fish view *here*
            Debug.Log("The fish has been gutted.");

            // Move state change outside after fleshing out
            _cleaningSubGameState = States.CleaningSubGameStates.shaveScales;
        }
    }

    private void ShaveScales() {
        // Add scale shaving logic
        if (inputManager.PrimaryKeyDown()) {
            //Play scale shaving animation from caught fish view *here*
            Debug.Log("Scales have been carved off the fish.");

            // Move state change outside after fleshing out
            _cleaningSubGameState = States.CleaningSubGameStates.storeInCooler;
        }
    }

    private void StoreInCooler() {
        // Add logic for storing fish in cooler
        if (inputManager.PrimaryKeyDown()) {
            //Play store in cooler animation from caught fish view *here*
            Debug.Log("Fish has been stored in the cooler");
            _cleaningSubGameState = States.CleaningSubGameStates.endSubGame;
        }
    }
}
