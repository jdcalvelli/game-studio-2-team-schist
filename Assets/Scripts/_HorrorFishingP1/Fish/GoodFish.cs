using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoodFish : Fish
{
    new Alignment alignment = Alignment.Good;



    public GoodFish()
    {
        this.alignment = Alignment.Good;
    }


    

}
