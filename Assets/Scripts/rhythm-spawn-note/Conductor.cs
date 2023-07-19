using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Conductor : MonoBehaviour
{
    public static Conductor instance { get; private set; }

    [SerializeField]
    float songBpm;

    [SerializeField]
    float clipLength;

    [SerializeField]
    float firstBeatOffset;

    [SerializeField, Range(0f, 8f)]
    public float beatsShownInAdvance;

    [SerializeField, Range(0f, 8f)]
    public float noteDistance;

    [SerializeField]
    GameObject notePrefab;

    [SerializeField]
    Transform notesContainerTransform;

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
            // Spawn note per beat
            GameObject note = Instantiate(notePrefab, notesContainerTransform);
            note.GetComponent<SpawnNote>().SetBeat(notes[nextIndex]);
            note.GetComponent<SpawnNote>().SetIndex(nextIndex);

            Vector3 newPosition = Vector3.zero;
            if(nextIndex > 0) {
                newPosition = GetRandomNotePosition(prevNotePosition);
            } 
            note.transform.localPosition = newPosition;
            prevNotePosition = newPosition;

            // Prompt player to move
            player.GetComponent<PlayerControl>().Move();

            nextIndex++;
        }
    }

    Vector3 GetRandomNotePosition(Vector3 prevNotePosition) {
        float randX = 0, randY = 0;
        while(randX == 0 && randY == 0) {
            randX = Mathf.Round(Random.Range(0f, 1f));
            randY = Mathf.Round(Random.Range(0f, 1f));
        }
        return new Vector3(prevNotePosition.x + randX * noteDistance, prevNotePosition.y + randY * noteDistance, prevNotePosition.z);
    }

    public void AddScore(float score) {
        gameScore += score;
        // scoreText.SetText("{0:0}", gameScore);
    }
}
