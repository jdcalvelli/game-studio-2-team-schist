using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{

    public int BPM = 120;

    // tolerance is a percentage of the interval size; toleranceNum will be the actual size of the interval

    public float tolerance = .125f;
    public float interval;
    public float toleranceNum;

    //Lower and upper bound are the timestamps (as a portion of the interval) that a cue needs to be in in order to trigger
    public float lowerBound;
    public float upperBound;

    // the last interval that was acted on
    public int lastInterval;

    //OnBeat returns 'true' if it's called in a frame that is within the tolerance window on either side of the beat.
    public bool OnBeat()
    {
        
        bool b = false;
        int _thisInterval = GetIntervalBeatCount();
        if (lastInterval < _thisInterval)
        {
            
            float _ipart = GetIntervalPart();
            if (_ipart > lowerBound || _ipart < upperBound)
            {
                b = true;
                           }
            lastInterval= _thisInterval;
        }
        return b;

    }

    //Returns the current distance into the active interval as a fraction of the interval
    public float GetIntervalPart()
    {
        float _t = Time.time/interval;
        int _ipart = (int)_t;
        float _fpart = _t - _ipart;
        
        return _fpart;
    }

    //Returns a count of intervals that have passed for the purposes of determining whether we're allowed to act yet
    public int GetIntervalBeatCount()
    {
        
        //adds toleranceNum so that back-half of an interval doesn't get treated as the same as the front-half.
        float _t = (Time.time + toleranceNum)/interval;
        int _ipart = (int)_t;
        Debug.Log(_ipart);
        return _ipart;
    }


    //Call this from the GameManager start function
    public void Initialize()
    {
        interval = 60f / BPM;
        lowerBound = 1 - tolerance;
        upperBound = tolerance;
        toleranceNum = tolerance * interval;
        lastInterval = 0;
    }
   


}
