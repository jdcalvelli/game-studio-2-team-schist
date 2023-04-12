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
    
    private void StoreInCooler() {
        // Add logic for storing fish in cooler
        if (inputManager.PrimaryKeyDown()) {
            //Play store in cooler animation from caught fish view *here*
            Debug.Log("Fish has been stored in the cooler");
            _cleaningSubGameState = States.CleaningSubGameStates.endSubGame;
        }
    }
}
