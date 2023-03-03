using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitingManager : MonoBehaviour
{
    private enum baitingSubGameStates
    {
        startBaitingGame,
        baiting,
        hookBaited,
    }


    public void BaitingSubGameUpdate()
    {
        switch (_baitingSubGameState)

        {
            case baitingSubGameStates.startBaitingGame:
                //we're ready to begin the process of baiting the hook
             
                break;

            case baitingSubGameStates.baiting:
                //we're actively baiting the hook
                break;

            case baitingSubGameStates.hookBaited:
                //the hook has been baited, and we're ready to move to fishing
                break;

        }    
    }


}
