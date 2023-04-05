using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    //a prototype class that all specific fish are derived from

    #region declarations

    // used for setting the correct FishView
    public Sprite sprite;
    public enum Alignment
    {
        BAD,
        GOOD,
    }

    [SerializeField] protected Alignment alignment;


    #endregion


#region AccessorMethods

    public virtual Alignment GetAlignment()
    {
        Alignment _a = alignment;
        return _a;
    }

    public virtual bool IsGood()
    {
        if (alignment == Alignment.GOOD)
        {
            return true;

        }
        else return false;
    }

    #endregion


}
