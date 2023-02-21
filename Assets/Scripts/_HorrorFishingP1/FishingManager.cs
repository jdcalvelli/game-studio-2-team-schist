using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    // states related to fishing minigame
    // could be moved into another class later
    private enum fishingSubGameStates
    {
        startSubGame,
        rodCast,
        waitingForBite,
        biteRegistered,
        rhythmicReeling,
        fishCaught,
        fishLost,
    }

    private fishingSubGameStates _fishingSubGameState = fishingSubGameStates.startSubGame;

    // for hook setting - expose to editor perhaps
    private float hookSetTimer = 0f;
    private float timeToSetHook = 1f;
    
    // for adding onBeatCallback listener during correct state
    private bool onBeatCallbackAdded = false;
    // for checking if a player input is inside of beat time
    private double beatTime;
    // this will ultimately be relevant for tweening things
    private double nextBeatTime;
    // setting the tolerance window for acceptable hits
    private double toleranceWindow = 0.5d;
    
    // for hit counting
    private int hitCounter;
    // for miss counting
    private int missCounter;
    
    // creating fishing update that will be called by game manager
    public void FishingSubGameUpdate()
    {
        switch (_fishingSubGameState)
        {
            case fishingSubGameStates.startSubGame:
                // on spacebar press cast the line
                // once again, this should be moved into an input manager
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CastRod();
                }
                break;
            
            case fishingSubGameStates.rodCast:
                // unused now but might be relevant later
                break;
            
            case fishingSubGameStates.waitingForBite:
                // over time increase the chances of getting a bite
                // Debug.Log("waiting for bite");
                WaitingForBite();
                break;
            
            case fishingSubGameStates.biteRegistered:
                // you have a certain amount of time to hook the fish
                // fix the call to input within this function to be outside of it on same level as other input calls
                // for easier refactoring later
                BiteRegistered();
                break;
            
            case fishingSubGameStates.rhythmicReeling:
                // this is ultimately where the rhythmic element will come in
                // as such this should be where the listener gets added for the on beat event
                // then this update is irrelevant, and things get handled in the onbeatcallack
                AddOnBeatListener();
                // checking input - should be refactored later
                if (Input.GetKeyDown(KeyCode.Space))
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
                    _fishingSubGameState = fishingSubGameStates.fishCaught;
                    // remove the beat listener
                    RemoveOnBeatListener();
                    // reset hit counter
                    hitCounter = 0;
                }
                else if (missCounter >= 4)
                {
                    // switch to fish lost state
                    _fishingSubGameState = fishingSubGameStates.fishLost;
                    // remove the listener
                    RemoveOnBeatListener();
                    // reset miss counter
                    missCounter = 0;
                }
                break;
            
            case fishingSubGameStates.fishCaught:
                Debug.Log("congrats you caught the fish");
                // restart game
                _fishingSubGameState = fishingSubGameStates.startSubGame;
                break;
            
            case fishingSubGameStates.fishLost:
                // restart game
                _fishingSubGameState = fishingSubGameStates.startSubGame;
                break;
        }
    }
    
    // when fishing, there should be a chance for a bite, then player needs to hook, then they need to reel

    #region RodCastRelated

    private void CastRod()
    {
        // this should also ultimately trigger anims on the fishingRodView - remember model view controller :D
        // all it does for now is switch between states
        _fishingSubGameState = fishingSubGameStates.rodCast;
        Debug.Log("line cast!");
        _fishingSubGameState = fishingSubGameStates.waitingForBite;
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
                Debug.Log("you have a bite, now set the hook");
                _fishingSubGameState = fishingSubGameStates.biteRegistered;
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

    #region BiteRegisteredRelated
    
    private void BiteRegistered()
    {
        // you have 1 second to secure the hook before you lose the fish
        hookSetTimer += Time.deltaTime;

        if (hookSetTimer > timeToSetHook)
        {
            Debug.Log("fish lost");
            // go to fishlost state
            _fishingSubGameState = fishingSubGameStates.fishLost;
            // reset hook set timer
            hookSetTimer = hookSetTimer - timeToSetHook;
        }
        else
        {
            // listen for space bar input
            // should go into input manager
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // fish has been set
                Debug.Log("hook has been set, now reel");
                _fishingSubGameState = fishingSubGameStates.rhythmicReeling;
            }
        }
    }

    #endregion

    #region RhythmicReelingRelated
    
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

        // logs just for testing
        // Debug.Log("-------");
        Debug.Log(beatArgs.BeatVal);
        // Debug.Log(beatArgs.BeatTime);
        // Debug.Log(beatArgs.NextBeatTime);
        // Debug.Log("-------");
    }

    #endregion
    
}