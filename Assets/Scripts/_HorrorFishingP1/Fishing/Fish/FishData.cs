using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class FishData : ScriptableObject
{

    [SerializeField] public enum Alignment
    {
        GOOD,
        BAD,
    };
    public Sprite sprite;
    [SerializeField] public Alignment alignment;
    

}
