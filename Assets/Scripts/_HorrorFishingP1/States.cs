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
    
}
