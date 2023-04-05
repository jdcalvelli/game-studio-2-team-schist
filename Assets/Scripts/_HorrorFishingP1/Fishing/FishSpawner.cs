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

    public Fish GetFish(float _doom)
    {
        
        InitializeFish();
        
        // we have to standardize how doom variable will work - i really think we have to remove the rhythm on the way down - jd
        if (Random.Range(0f,1f) < _doom)
        {
            return goodFish[Random.Range(0, goodFish.Count)];
        }
        else
        {
            return badFish[Random.Range(0,badFish.Count)];
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
