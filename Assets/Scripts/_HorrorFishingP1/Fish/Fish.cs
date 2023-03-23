using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    //a prototype class that all specific fish are derived from

    #region declarations

    [SerializeField] protected Sprite sprite;
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

    #endregion


}
