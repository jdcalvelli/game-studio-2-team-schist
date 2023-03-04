using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Rhythm : MonoBehaviour
{
    //Possibly unnecessary
    //If used, change GameObject to Fish once class is implemented
    GameObject attachedFish;

    // A rhythm contains a list of quarter, eighth and sixteenth notes
    // I probably shouldn't have set this up for more than quarter notes
    int[] quarterSet = new int[4];
    //int[] eighthSet = new int[8];
    //int[] sixteenthSet = new int[16];

    public void SetQuarter(int[] args) {
        if (args.Length != 4) {
            throw new ArgumentException("Incorrect number of SetQuarter parameters. Expects exactly 4", nameof(args));
        }
        else {
            for (int i = 0; i < quarterSet.Length; i++) {
                quarterSet[i] = args[i];
            }
        }
    }



}
