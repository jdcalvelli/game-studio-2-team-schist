using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] private TextAsset inkAsset;
    private Story _inkStory;

    // reference to singleton
    public static NarrativeManager Instance;
    
    private void Awake()
    {
        // singleton insurance, only one present
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        // setting story
        _inkStory = new Story(inkAsset.text);
    }

    // Start is called before the first frame update
    void Start()
    {
        // example usage of api exposed
        SetKnotAndStitch("introText", "01");
        while (_inkStory.canContinue)
        {
            Debug.Log(ContinueStory());
        }
    }

    public void SetKnotAndStitch(string knot, string stitch)
    {
        _inkStory.ChoosePathString($"{knot}.{stitch}");
    }

    public string ContinueStory()
    {
        return _inkStory.Continue();
    }
}
