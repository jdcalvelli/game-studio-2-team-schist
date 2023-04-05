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
    
    // for adding onBeatCallback listener during correct state
    private bool onBeatCallbackAdded = false;
    // for checking if a player input is inside of beat time
    private double beatTime;
    // this will ultimately be relevant for tweening things
    private double nextBeatTime;
    // setting the tolerance window for acceptable hits
    private double toleranceWindow = 0.5d;

    // flag to only spawn notes after each other
    private bool noteSpawnFlag = true;
    private NoteView currentNoteView = null;
    private GameObject currentNote = null;
    private GameObject nextNote = null;

    //flag to run coroutine only once
    private bool castingRodFlag = false;
    private bool fishCaughtFlag = false;
    
    // doom variable for which fish you get
    // will be incremented by 0.1 for each miss
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

    [SerializeField] private GameObject _notePrefab;

    // Views Setup - FishingManager
    [SerializeField] private GameObject _fishingViewsContainer;

    [SerializeField] private FishingRodView _fishingRodView;
    [SerializeField] private FishView _fishView;
    [SerializeField] private NoteView _noteView;
    [SerializeField] private BeatBarView _beatBarView;

    [SerializeField] GameObject beatBar;
    
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
                // first adding the listener
                AddOnBeatListener();
                IncrementTimer();
                if (inputManager.PrimaryKeyDown() && timer < timeToSinkHook)
                {
                    // check if input is NOT beat
                    if (!CheckInputOnBeat())
                    {
                        // increment doom variable
                        doomVar++;
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
                    // removing listener
                    RemoveOnBeatListener();
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
                    _beatBarView.Animate_BeatBarAppearOrDisappear();
                    //_noteView.Animate_NoteAppear();
                    StartCoroutine(SpawnNoteOnBar());
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
                // as such this should be where the listener gets added for the on beat event
                // then this update is irrelevant, and things get handled in the onbeatcallack
                AddOnBeatListener();

                if (noteSpawnFlag) {
                    StartCoroutine(SpawnNoteOnBar());
                }

                // checking input - should be refactored later
                if (inputManager.PrimaryKeyDown())
                {
                    if (CheckInputOnBeat())
                    {
                        Debug.Log("hit on beat");
                        currentNoteView.Animate_NoteHit();
                        // increment a hit counter 
                        hitCounter++;
                    }
                    else
                    {
                        Debug.Log("missed the beat");
                        currentNoteView.Animate_NoteMiss();
                        // increment a miss counter
                        missCounter++;
                    }
                }
                
                // checking to see if sufficient hits/misses happened to trigger state change
                // using arbitrary hits and misses for now
                if (hitCounter >= 4)
                {
                    _beatBarView.Animate_BeatBarAppearOrDisappear();
                    // switch to fish caught state
                    FishingSubGameState = States.FishingSubGameStates.fishCaught;
                    // remove the beat listener
                    RemoveOnBeatListener();
                    // reset hit counter
                    hitCounter = 0;
                    missCounter = 0;
                }
                else if (missCounter >= 4)
                {
                    _beatBarView.Animate_BeatBarAppearOrDisappear();
                    // switch to fish lost state
                    FishingSubGameState = States.FishingSubGameStates.fishLost;
                    // remove the listener
                    RemoveOnBeatListener();
                    // reset miss counter
                    hitCounter = 0;
                    missCounter = 0;
                }
                break;
            
            case States.FishingSubGameStates.fishCaught:
                Debug.Log("congrats you caught the fish");
                // hide note display and show fish on success
                currentNoteView.Animate_NoteDisappear();
                // this needs to be refactored into a coroutine
                _fishView.Animate_FishCaught();
                
                // reference fish spawner to return a fish and debug it
                // we can ultimately pass this into fish caught animate to determine which fish image we use
                Debug.Log(fishSpawner.GetFish(doomVar));
                
                // restart game
                FishingSubGameState = States.FishingSubGameStates.showFishCaught;
                break;

            case States.FishingSubGameStates.showFishCaught:
                if (fishCaughtFlag == false) {
                    StartCoroutine(ShowFishCaught());
                }
                break;

            case States.FishingSubGameStates.fishLost:
                // restart game
                // hide note display on failure
                if (currentNoteView != null) {
                    currentNoteView.Animate_NoteDisappear();
                }
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

    private IEnumerator SpawnNoteOnBar() {
        noteSpawnFlag = false;
        yield return new WaitForSeconds(1f);
        currentNote = Instantiate(_notePrefab, beatBar.transform);
        currentNote.transform.position = new Vector3(beatBar.transform.position.x + 400f, beatBar.transform.position.y + 450f, 0f);
        currentNoteView = currentNote.GetComponent<NoteView>();
        StartCoroutine(currentNoteView.Animate_MoveNoteAlongBar((float)(nextBeatTime - beatTime)));
        noteSpawnFlag = true;
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
    
        FishingSubGameState = States.FishingSubGameStates.waitingForBite;
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

    #region TimerHelper
    
    private void IncrementTimer()
    {
        // increment passed in timer based on time delta time
        timer += Time.deltaTime;
    }

    #endregion

    #region RhythmRelated
    
    // these two can be refactored into one function later - like UpdateOnBeatListener or something
    private void AddOnBeatListener()
    {
        if (!onBeatCallbackAdded)
        {
            // this grabs the instance of clock script and calls OnBeatCallback on reception of event
            Beat.Clock.Instance.Beat += OnBeatCallback;
            Debug.Log("listener added");
        }

        onBeatCallbackAdded = true;

    }

    #region FishCaughtRelated

    private IEnumerator ShowFishCaught() {
        fishCaughtFlag = true;
        _fishView.Animate_FishCaught();
        yield return new WaitForSeconds(3.5f);
        FishingSubGameState = States.FishingSubGameStates.endSubGame;
        fishCaughtFlag = false;
    }

    #endregion

    private void RemoveOnBeatListener()
    {
        if (onBeatCallbackAdded)
        {
            // this grabs the instance of clock script and remvoves the call to OnBeatCallback on reception of event
            Beat.Clock.Instance.Beat -= OnBeatCallback;
            Debug.Log("listener removed");
        }

        onBeatCallbackAdded = false;
    }
    
    // event is only added when we're in the correct state, at which point we should execute
    // the following logic on each beat
    // we can't make calls to input here bc its on a separate thread it seems
    private void OnBeatCallback(Beat.Args beatArgs)
    {
        // set some timings to be referenced
        // assumption is for now that we only care abt quarter notes - will have to refactor later prob
        beatTime = beatArgs.BeatTime;
        nextBeatTime = beatArgs.NextBeatTime;

        // logs just for testing
        // Debug.Log("-------");
        Debug.Log(beatArgs.BeatVal);
        // Debug.Log(beatArgs.BeatTime);
        // Debug.Log(beatArgs.NextBeatTime);
        // Debug.Log("-------");
    }
    
    // because we cant check for input on alternate threads, what we're going to do is
    // essentially check if the player presses space within the time frame presented
    private bool CheckInputOnBeat()
    {
        // want to check if the dsptime at this point is within a tolerance window of the
        // correct beat time as set by the clock script
        if (AudioSettings.dspTime > beatTime - toleranceWindow 
            && AudioSettings.dspTime < beatTime + toleranceWindow)
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