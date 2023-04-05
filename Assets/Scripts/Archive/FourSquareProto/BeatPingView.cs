using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatPingView : MonoBehaviour
{

    public void ChangeColor(Color color)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
    }

}
