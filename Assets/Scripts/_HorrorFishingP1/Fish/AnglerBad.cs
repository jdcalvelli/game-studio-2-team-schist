using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Fish;

public class AnglerBad : Fish
{

    new Alignment alignment = Alignment.Bad;

    
    
    public AnglerBad()
    {
        this.alignment = Alignment.Bad;
    }

}
