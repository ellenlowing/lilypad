using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeafConductor : MonoBehaviour
{
    public static LeafConductor instance { get; private set; }

    [SerializeField]
    float songBpm;

    [SerializeField]
    float clipLength;

    [SerializeField]
    float firstBeatOffset;

    [SerializeField, Range(0f, 8f)]
    public float beatsShownInAdvance;

    [SerializeField]
    GameObject player;
    
    float secPerBeat;
    float songPosition;
    public float songPositionInBeats;
    float dspSongTime;
    float [] notes;
    public int nextIndex;
    // AudioSource musicSource;

    float gameScore = 0f;

    // [SerializeField]
    // TextMeshProUGUI scoreText;

    Vector3 prevNotePosition;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    void Start()
    {
        // musicSource = GetComponent<AudioSource>();
        // clipLength = musicSource.clip.length;
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        // musicSource.Play();
        nextIndex = 0;
        notes = new float[(int)Mathf.Floor(clipLength)];
        for(int i = 0; i < Mathf.Floor(clipLength); i++)
        {
            notes[i] = (float)i;
        }
    }

    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPosition / secPerBeat;
        
        if(nextIndex < notes.Length && notes[nextIndex] < (songPositionInBeats + beatsShownInAdvance))
        {
            
            player.GetComponent<MovePlayer>().Jump();

            nextIndex++;
        }
    }

    public void AddScore(float score) {
        gameScore += score;
        // scoreText.SetText("{0:0}", gameScore);
    }
}
