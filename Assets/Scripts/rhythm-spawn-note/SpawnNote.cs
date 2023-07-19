using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{
    public float beat { get; private set; }
    public int index { get; private set; }

    void Start()
    {
        
    }

    void Update()
    {
        if(index == Conductor.instance.nextIndex-1) 
        {
            float songPositionNormalized = Conductor.instance.songPositionInBeats - Mathf.Floor(Conductor.instance.songPositionInBeats);
            transform.localScale = new Vector3(songPositionNormalized, songPositionNormalized, songPositionNormalized);
        }
    }

    public void SetBeat(float _beat) 
    {
        beat = _beat;
    }

    public void SetIndex(int _index) {
        index = _index;
    }
}
