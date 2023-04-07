using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{

    // arrays have an unalterable length - determined at compile time, jd switched these to lists so that it works as intended.
    public List<Fish> allFish;
    public List<Fish> goodFish;
    public List<Fish> badFish;

    public Fish GetFish(int _doom)
    {
        
        InitializeFish();
        
        // right now, doom is incrementing by 1 each time you input, and is 0 if you miss everything
        
        if (Random.Range(0f,1f) <= (float)_doom / 10)
        {
            return badFish[Random.Range(0,badFish.Count)];
        }
        else
        {
            return goodFish[Random.Range(0, goodFish.Count)];
        }
    }


    //use this at the beginning to sort the fish from the allFish array

    public void InitializeFish()
    {
        // in the event that the sorting hasnt already happened, do the sort
        if (goodFish.Count == 0 && badFish.Count == 0)
        {
            foreach(Fish fish in allFish)
            {
                if (fish.GetAlignment() == Fish.Alignment.GOOD)
                {
                    goodFish.Add(fish);
                }
                else
                {
                    badFish.Add(fish);
                }
            }
        }
    }

}
