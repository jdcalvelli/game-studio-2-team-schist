using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaitingManager : MonoBehaviour
{
    private float timer;
    private float holdingTimer;
    private float phraseLength = 5f;
    private float startTime = 1f;
    private float holdingTime = 2f;
    private float missBornTime = .5f;

    public TextMeshPro text1;
    public TextMeshPro text2;
    
    private TextMeshPro test1;
    private TextMeshPro test2;
    
    [SerializeField] private BaitingAssetGenerator _generator;

    private GameObject baitingHolder;
    
    private enum baitingSubGameStates
    {
        startBaitingGame,
        baiting,
        hookBaited,
        endSubGame,
    }

    private baitingSubGameStates _baitingSubGameState = baitingSubGameStates.startBaitingGame;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    

    public void BaitingSubGameUpdate()
    {
        switch (_baitingSubGameState)

        {
            case baitingSubGameStates.startBaitingGame:
                //we're ready to begin the process of baiting the hook
                
                //this state is going to generate all the tokens, timer for baiting mini
                //and visually tells player: we are about to start!
                //instantiate the tokens for baiting mini games
                GenerateTokens();
                //initialize the timer
                timer = 0f;
                //prompt for the start of baiting rhythm pops out
                BaitStartAni();
                
                _baitingSubGameState = baitingSubGameStates.baiting;
                break;

            case baitingSubGameStates.baiting:
                //In this state, players try to align the hook with worm
                
                //one round is 5 sec
                //rhythm symbol recycles again and again according to timer
                timer += Time.deltaTime;
                test1.text = "Timer: " + timer.ToString();

                if (timer >= phraseLength)
                {
                    timer = 0f;
                    Debug.Log("recycle loop");
                }

                //if player hit the symbol in specific point (1±0.5 sec)
                //they successfully get into the hook-baiting period
                if (inputManager.PrimaryKeyDown())
                {
                    if (timer > startTime - missBornTime && timer < startTime + missBornTime)
                    {
                        _baitingSubGameState = baitingSubGameStates.hookBaited;
                    }else
                    {
                        timer = 0f;
                        Debug.Log("ENTER FAIL! move the symbol to the initial point!");
                    }
                }
                
                //else if they hit too early or too late
                //they fail, the timer is reset
                
                //TODO: add some animation here so the narrative team knows there can be some story
                
                break;

            case baitingSubGameStates.hookBaited:
                //In this state, players try to thread the whole worm through the hook
                //which says, they need to hold the button for 2±0.5 sec 
                
                holdingTimer += Time.deltaTime;
                test2.text = "HoldingTimer: " + holdingTimer.ToString();
                
                //if the player release the button too late
                //mini-holding game immediately fails,get back to the last state
                if (holdingTimer >= holdingTime + missBornTime)
                {
                    //get back to the beginning
                    timer = 0f;
                    holdingTimer = 0f;
                    Debug.Log("HOLD TOO LONG!");
                    _baitingSubGameState = baitingSubGameStates.baiting;
                }
                
                //if the player release the button in specific point (2±0.5 sec)
                //they finish baiting and get into end state
                if (inputManager.PrimaryKeyUp())
                {
                    if (holdingTimer >= holdingTime - missBornTime)
                    {
                        _baitingSubGameState = baitingSubGameStates.endSubGame;
                    }
                    else
                    {
                        //get back to the beginning
                        timer = 0f;
                        holdingTimer = 0f;
                        Debug.Log("LEAVE FAIL! move the symbol to the initial point!");
                        _baitingSubGameState = baitingSubGameStates.baiting;
                    }
                }
                //else if they release too early
                //they fail, the timer is reset
                
                //TODO: animation here
                
                break;
            
            case baitingSubGameStates.endSubGame:
                // bubble up a call to game manager to change state
                gameManager.SetGameState(States.GameStates.isFishing);
                Destroy(baitingHolder);
                break;

        }    
    }

   
    
    private void GenerateTokens()
    {
        baitingHolder = new GameObject("Baiting Holder");
        
        test1 = Instantiate(text1);
        test2 = Instantiate(text2);

        _generator.BaitingAssetGenerate();
        //test1.transform.SetParent(baitingHolder.transform);
        //test2.transform.SetParent(baitingHolder.transform);
        
    }

    private void BaitStartAni()
    {
        Debug.Log("START!");
    }
}
