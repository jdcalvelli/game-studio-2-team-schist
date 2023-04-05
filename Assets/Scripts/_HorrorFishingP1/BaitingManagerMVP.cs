using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitingManagerMVP : MonoBehaviour
{
    public enum baitingSubGameStates
    {
        startSubGame,
        baitHook,
        endSubGame,
    }

    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CanvasManager canvasManager;

    [SerializeField] private GameObject _baitingViewsContainer;

    [SerializeField] private BaitingViewMVP _baitingView;

    public baitingSubGameStates _baitingSubGameState = baitingSubGameStates.startSubGame;

    public void BaitingSubGameUpdate() {

        switch (_baitingSubGameState) {
            case baitingSubGameStates.startSubGame:
                _baitingViewsContainer.SetActive(true);
                canvasManager.ActivateText(CanvasManager.textPositions.bottomCenter);
                canvasManager.SetText(CanvasManager.textPositions.bottomCenter, "PRESS SPACE TO BAIT");
                if (inputManager.PrimaryKeyDown()) {
                    _baitingSubGameState = baitingSubGameStates.baitHook;
                }
                break;
            case baitingSubGameStates.baitHook:
                canvasManager.DeactivateText(CanvasManager.textPositions.bottomCenter);
                StartCoroutine(BaitHook());
                break;
            case baitingSubGameStates.endSubGame:
                _baitingViewsContainer.SetActive(false);
                _baitingView.ResetBaitView();

                EndSubGame();

                break;
        }
    }

    private IEnumerator BaitHook() {
        _baitingView.Animate_BaitHook();
        yield return new WaitForSeconds(2f);
        _baitingSubGameState = baitingSubGameStates.endSubGame;
    }

    private void EndSubGame() {
        // set game manager flag
        gameManager.hasBaited = true;

        // move to central global state
        gameManager.SetGameState(States.GameStates.onBoat);
    }
}
