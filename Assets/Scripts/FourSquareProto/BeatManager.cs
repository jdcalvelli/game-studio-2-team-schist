using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{

    public int BPM = 120;

    // this means that the tolerance is 12.5% of the interval between beats, i.e. 1/8th of a note on either side. So it's a 1/4-long tolerance
    public float tolerance = .125f;
    public float interval;

    //Lower and upper bound are the timestamps (as a portion of the interval) that a cue needs to be in in order to trigger
    public float lowerBound;
    public float upperBound;



    // Start is called before the first frame update
    void Start()
    {
        interval = 60 / BPM;
        lowerBound = 1 - tolerance;
        upperBound = tolerance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool OnBeat()
    {
        bool b = false;
        float _ipart = GetIntervalPart();
        if (_ipart > lowerBound || _ipart < upperBound) 
        {
            b = true;
        }

        return b;

    }

    public float GetIntervalPart()
    {
        //Calculates how far into the "interval" we are by taking the fractional part of the current interval; the returned value is as a portion of the interval, e.g. .12 is 12% 
        float _t = Time.time;
        int _seconds = (int)_t;
        float _part = _t - _seconds;
        
        return _part;
    }

}
