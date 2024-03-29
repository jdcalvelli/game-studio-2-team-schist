using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{

    public States.FishingSubGameStates FishingSubGameState = States.FishingSubGameStates.startSubGame;

    // timers - expose to editor perhaps
    private float timer = 0f;
    private float timeToSetHook = 1f;
    private float timeToSinkHook = 5f;
    private float waitingForBiteOffsetTimer = 1.5f;
    
    // for checking if a player input is inside of beat time
    private double beatTime;
    // this will ultimately be relevant for tweening things
    private double nextBeatTime;
    // length of a beat
    private double beatDuration;
    // setting the tolerance window for acceptable hits
    private double toleranceWindow = 0.5d;

    //flag to run coroutine only once
    private bool castingRodFlag = false;
    private bool fishCaughtFlag = false;
    
    // doom variable for which fish you get
    // will be incremented by 1 for each miss
    private int doomVar = 0;

    // for hit counting
    private int hitCounter;
    // for miss counting
    private int missCounter;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CanvasManager canvasManager;

    // grab reference to fishspawner
    [SerializeField] private FishSpawner fishSpawner;

    // Views Setup - FishingManager
    [SerializeField] private GameObject _fishingViewsContainer;

    [SerializeField] private FishingRodView _fishingRodView;
    [SerializeField] private FishView _fishView;

    private void Start()
    {
        // subscribing to on beat events, we will check for the correct state inside of the onbeatcallback
        Beat.Clock.Instance.Beat += OnBeatCallback;
        Debug.Log(" FISHING MANAGER: ON BEAT listener added");
    }

    // creating fishing update that will be called by game manager
    public void FishingSubGameUpdate()
    {
        switch (FishingSubGameState)
        {
            case States.FishingSubGameStates.startSubGame:
                // on spacebar press cast the line
                // once again, this should be moved into an input manager
                _fishingViewsContainer.SetActive(true);
                canvasManager.SetText(CanvasManager.textPositions.bottomLeft, "PRESS SPACE TO CAST");
                canvasManager.ActivateText(CanvasManager.textPositions.bottomLeft);
                if (inputManager.PrimaryKeyDown())
                {
                    FishingSubGameState = States.FishingSubGameStates.castingRod;
                }
                break;
            
            case States.FishingSubGameStates.castingRod:
                canvasManager.DeactivateText(CanvasManager.textPositions.bottomLeft);
                if (castingRodFlag == false) {
                    StartCoroutine(CastRod());
                }
                break;
            
            case States.FishingSubGameStates.rhythmDown:
                // need to drop hook on rhythm
                IncrementTimer();
                if (inputManager.PrimaryKeyDown() && timer < timeToSinkHook)
                {
                    // check if input is on beat
                    // if so decrement doom variable
                    // doom variable by default will go up every beat
                    // see in beat callback below
                    if (CheckInputOnBeat())
                    {
                        // increment doom variable
                        doomVar--;
                    }
                }
                else if (timer > timeToSinkHook)
                {
                    // just printing doom variable for now
                    Debug.Log(doomVar);
                    // move to the next state to wait for bite
                    FishingSubGameState = States.FishingSubGameStates.waitingForBite;
                    // resetting timer
                    timer -= timeToSinkHook;
                }
                break;
            
            case States.FishingSubGameStates.waitingForBite:
                // over time increase the chances of getting a bite
                // Debug.Log("waiting for bite");
                if (waitingForBiteOffsetTimer <= 0f) {
                    WaitingForBite();
                }
                else {
                    waitingForBiteOffsetTimer -= Time.deltaTime;
                }
                break;
            
            case States.FishingSubGameStates.biteRegistered:
                // you have a certain amount of time to hook the fish
                // this gets caught by dotween but should ultimately be refactored to only be called once later
                waitingForBiteOffsetTimer = 1.5f;
                IncrementTimer();
                if (inputManager.PrimaryKeyDown() && timer < timeToSetHook)
                {
                    // fish has been set
                    Debug.Log("hook has been set, now reel");
                    FishingSubGameState = States.FishingSubGameStates.rhythmUp;
                    // reset hook set timer
                    timer -= timeToSetHook;
                }
                else if (timer > timeToSetHook)
                {
                    Debug.Log("fish lost");
                    // go to fishlost state
                    FishingSubGameState = States.FishingSubGameStates.fishLost;
                    // reset hook set timer
                    timer -= timeToSetHook;
                }
                break;
            
            case States.FishingSubGameStates.rhythmUp:
                // this is ultimately where the rhythmic element will come in

                // checking input - should be refactored later
                if (inputManager.PrimaryKeyDown())
                {
                    if (CheckInputOnBeat())
                    {
                        Debug.Log("hit on beat");
                        // increment a hit counter 
                        hitCounter++;
                    }
                    else
                    {
                        Debug.Log("missed the beat");
                        // increment a miss counter
                        missCounter++;
                    }
                }
                
                // checking to see if sufficient hits/misses happened to trigger state change
                // using arbitrary hits and misses for now
                if (hitCounter >= 4)
                {
                    // switch to fish caught state
                    FishingSubGameState = States.FishingSubGameStates.fishCaught;
                    // reset hit counter
                    hitCounter = 0;
                    missCounter = 0;
                }
                else if (missCounter >= 4)
                {
                    // switch to fish lost state
                    FishingSubGameState = States.FishingSubGameStates.fishLost;
                    // reset miss counter
                    hitCounter = 0;
                    missCounter = 0;
                }
                break;
            
            case States.FishingSubGameStates.fishCaught:
                Debug.Log("congrats you caught the fish");

                // reference fish spawner to return a fish and debug it
                // we can ultimately pass this into fish caught animate to determine which fish image we use
                Debug.Log(fishSpawner.GetFish(doomVar));
                
                // restart game
                FishingSubGameState = States.FishingSubGameStates.showFishCaught;
                break;

            case States.FishingSubGameStates.showFishCaught:
                // reset the doom Var
                doomVar = 0;
                
                if (fishCaughtFlag == false) {
                    StartCoroutine(ShowFishCaught(fishSpawner.GetFish(doomVar)));
                }
                break;

            case States.FishingSubGameStates.fishLost:
                // reset the doomVar
                doomVar = 0;
                // restart game
                FishingSubGameState = States.FishingSubGameStates.startSubGame;
                break;
            
            case States.FishingSubGameStates.endSubGame:

                _fishingViewsContainer.SetActive(false);

                EndSubGame();

                break;
        }
    }

    private void EndSubGame() {
        // set game manager flag
        gameManager.hasFished = true;

        // move to central global state
        gameManager.SetGameState(States.GameStates.onBoat);
    }

    // when fishing, there should be a chance for a bite, then player needs to hook, then they need to reel

    #region RodCastRelated

    private IEnumerator CastRod()
    {
        // this should also ultimately trigger anims on the fishingRodView - remember model view controller :D
        // all it does for now is switch between states
        // Play fishing rod view animation
        castingRodFlag = true;
        _fishingRodView.Animate_CastRod();
        yield return new WaitForSeconds(_fishingRodView.rodCastTimer + _fishingRodView.lineFlyTimer);
    
        // we want to move to rhythm down now
        FishingSubGameState = States.FishingSubGameStates.rhythmDown;
        castingRodFlag = false;
        canvasManager.DeactivateText(CanvasManager.textPositions.bottomLeft);
        Debug.Log("line cast!");
    }

    #endregion

    #region WaitingForBiteRelated
    
    private void WaitingForBite()
    {
        // every five-ish seconds
        if (Mathf.FloorToInt(Time.time) % 5 == 0)
        {
            if (CheckFishIsBiting())
            {
                _fishingRodView.Animate_FishIsBiting();
                Debug.Log("you have a bite, now set the hook");
                FishingSubGameState = States.FishingSubGameStates.biteRegistered;
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
    
    #endregion

    #region FishCaughtRelated

    private IEnumerator ShowFishCaught(Fish fish) {
        fishCaughtFlag = true;
        _fishView.Animate_FishCaught(fish);
        yield return new WaitForSeconds(3.5f);
        FishingSubGameState = States.FishingSubGameStates.endSubGame;
        fishCaughtFlag = false;
    }

    #endregion
    
    #region TimerHelper
    
    private void IncrementTimer()
    {
        // increment passed in timer based on time delta time
        timer += Time.deltaTime;
    }

    #endregion
    
    #region RhythmRelated

    // event is only added when we're in the correct state, at which point we should execute
    // the following logic on each beat
    // we can't make calls to input here bc its on a separate thread it seems
    private void OnBeatCallback(Beat.Args beatArgs)
    {
        // set some timings to be referenced
        // assumption is for now that we only care abt quarter notes - will have to refactor later prob
        beatTime = beatArgs.BeatTime;
        nextBeatTime = beatArgs.NextBeatTime;
        beatDuration = nextBeatTime - beatTime;
        
        // logs just for testing
        // Debug.Log("-------");
        // Debug.Log(beatArgs.BeatVal);
        // Debug.Log(beatArgs.BeatTime);
        // Debug.Log(beatArgs.NextBeatTime);
        // Debug.Log("-------");

        // state checker inside of onbeatcallback to cut out like 50 lines of redundant code
        switch (FishingSubGameState)
        {
            case States.FishingSubGameStates.rhythmDown:
                // call the rod rotate function backwards to simulate going down
                _fishingRodView.Animate_RodSpin(-360, beatDuration);
                // call the color shift
                _fishingRodView.Animate_ShiftBeatColor(beatDuration);
                
                // increment doom variable by default
                doomVar++;
                Debug.Log(doomVar);
                break;
            case States.FishingSubGameStates.rhythmUp:
                // call the rod rotate function going forward
                _fishingRodView.Animate_RodSpin(360, beatDuration);
                // call the color shift
                _fishingRodView.Animate_ShiftBeatColor(beatDuration);
                break;
        }
    }
    
    // because we cant check for input on alternate threads, what we're going to do is
    // essentially check if the player presses space within the time frame presented
    private bool CheckInputOnBeat()
    {
        // want to check if the dsptime at this point is within a tolerance window of the
        // correct beat time as set by the clock script
        if (AudioSettings.dspTime > nextBeatTime - toleranceWindow 
            && AudioSettings.dspTime < nextBeatTime + toleranceWindow)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
    
}