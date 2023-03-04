using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fish
{

    //a prototype class that all specific fish are derived from

    #region declarations

    protected Sprite sprite;
    public enum Alignment
    {
        Bad,
        Good,
    }

    protected Alignment alignment;
    protected string name;

    #endregion


#region AccessorMethods

    public virtual Alignment GetAlignment()
    {
        Alignment _a = alignment;
        return _a;
    }

    #endregion


}
