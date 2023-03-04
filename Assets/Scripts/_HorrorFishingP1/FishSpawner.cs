using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    public Fish[] allFish;
    public Fish[] goodFish;
    public Fish[] badFish;

    public Fish GetFish(float _doom)
    {
        if (Random.Range(0f,1f) < _doom)
        {
            return badFish[Random.Range(0,badFish.Length)];
        }
        else
        {
            return goodFish[Random.Range(0, goodFish.Length)];
        }
    }

}
