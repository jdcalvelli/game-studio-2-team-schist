using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creating a public class to reference the enum types that we will use in game manager
public class States
{
    // general states for the game
    // start and end are not being used but are here for context for now
    public enum GameStates
    {
        gameStart,
        onBoat,
        isBaiting,
        isFishing,
        isCleaning,
        gameEnd
    }

    public enum BaitingSubGameStates
    {
        startSubGame,
        baitHook,
        endSubGame,
    }

    // states related to fishing minigame
    public enum FishingSubGameStates
    {
        startSubGame,
        castingRod,
        rhythmDown,
        waitingForBite,
        biteRegistered,
        rhythmUp,
        fishCaught,
        fishLost,
        showFishCaught,
        endSubGame,
    }

    // cleaning subgame states
    public enum CleaningSubGameStates
    {
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

}
