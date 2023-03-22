using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitingAssetGenerator : MonoBehaviour
{
    [SerializeField] private GameObject baitingBackground;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject hook;

    public void BaitingAssetGenerate()
    {
        Instantiate(baitingBackground);
        Instantiate(hand);
        Instantiate(hook);
        
    }
}
