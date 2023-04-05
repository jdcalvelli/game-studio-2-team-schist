using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitingManagerMVP : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CanvasManager canvasManager;

    [SerializeField] private GameObject _baitingViewsContainer;

    [SerializeField] private BaitingViewMVP _baitingView;

    public States.BaitingSubGameStates BaitingSubGameState = States.BaitingSubGameStates.startSubGame;

    public void BaitingSubGameUpdate() {

        switch (BaitingSubGameState) {
            case States.BaitingSubGameStates.startSubGame:
                _baitingViewsContainer.SetActive(true);
                canvasManager.ActivateText(CanvasManager.textPositions.bottomCenter);
                canvasManager.SetText(CanvasManager.textPositions.bottomCenter, "PRESS SPACE TO BAIT");
                if (inputManager.PrimaryKeyDown()) {
                    BaitingSubGameState = States.BaitingSubGameStates.baitHook;
                }
                break;
            case States.BaitingSubGameStates.baitHook:
                canvasManager.DeactivateText(CanvasManager.textPositions.bottomCenter);
                StartCoroutine(BaitHook());
                break;
            case States.BaitingSubGameStates.endSubGame:
                _baitingViewsContainer.SetActive(false);
                _baitingView.ResetBaitView();

                EndSubGame();

                break;
        }
    }

    private IEnumerator BaitHook() {
        _baitingView.Animate_BaitHook();
        yield return new WaitForSeconds(2f);
        BaitingSubGameState = States.BaitingSubGameStates.endSubGame;
    }

    private void EndSubGame() {
        // set game manager flag
        gameManager.hasBaited = true;

        // move to central global state
        gameManager.SetGameState(States.GameStates.onBoat);
    }
}
