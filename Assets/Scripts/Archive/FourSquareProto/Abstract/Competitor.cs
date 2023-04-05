using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Competitor : MonoBehaviour
{
    public abstract void HitBallTo(Action<BallController.BallLocation> callback);
}
