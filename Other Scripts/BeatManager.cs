//Down Beat
//This script manages the passage of time in an isolated way so it isn't affected by FPS variations and this way the beat is always stable.
//It then calculates when the next beat will come to activate events on those specific samples and, of course, loop the song in a seamless way.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{

    public float bpm;
    public int beatUnit = 4;
    public int barLength = 4;
    public int strongBeat = 1;
    private float bps;
   
    public AudioSource childSource;
    public AudioSource downbeat;

    public enum BeatType
    {
        NoBeat,
        DownBeat,
        FourthBeat,
        EighthBeat,
        SixteenthBeat
    }

    public static int fourthNotesCounter;
    public static int eighthNotesCounter;
    public static int sixteenthNotesCounter;
    public static bool OnBeat, OnEighthBeat, OnSixteenthBeat,kickIn;
    public static BeatType currentBeat;
    public static float barDuration;
    public static float currentSample;
    public static float audioFrequency;
    public static AudioSource muffledSource;

    public static AudioSource mySource;


    public float delay;
    public float loopSampleRange;
    private float firstBeatRange;

    float nextFourthSample, nextEighthSample, nextSixteenthSample;

    bool nextBeatIsOut;


    void Awake()
    {
        mySource = GetComponent<AudioSource>();
        muffledSource = childSource;
        fourthNotesCounter = barLength;
        eighthNotesCounter = barLength * 2;
        sixteenthNotesCounter = barLength * 4;
        bps = bpm / 60;

        nextFourthSample = delay * mySource.clip.frequency;
        nextEighthSample = delay * mySource.clip.frequency;
        nextSixteenthSample = delay * mySource.clip.frequency;

        OnBeat = false;
        OnEighthBeat = false;
        OnSixteenthBeat = false;
        currentBeat = BeatType.NoBeat;

        barDuration = 1 / bps * barLength;
        firstBeatRange = (delay * mySource.clip.frequency) + (1 / bps * mySource.clip.frequency);

        audioFrequency = mySource.clip.frequency;
    }

    void Update()
    {
        if (mySource.isPlaying)
        {
            currentSample = mySource.timeSamples;

            if (nextBeatIsOut && mySource.timeSamples < firstBeatRange)
            {
                nextFourthSample = delay * mySource.clip.frequency;
                nextEighthSample = delay * mySource.clip.frequency;
                nextSixteenthSample = delay * mySource.clip.frequency;


                nextBeatIsOut = false;
            }
            if (mySource.timeSamples > nextFourthSample)
            {
                CountFourthBeat();
                CountEighthBeat();
                CountSixteenthBeat();
                
                OnBeat = true;
                OnEighthBeat = true;
                OnSixteenthBeat = true;

                currentBeat = BeatType.FourthBeat;

                if (fourthNotesCounter == 1)
                {
                    currentBeat = BeatType.DownBeat;
                    downbeat.Play();
                }
            }
            else if (mySource.timeSamples > nextEighthSample)
            {
                CountEighthBeat();
                CountSixteenthBeat();

                currentBeat = BeatType.EighthBeat;

                OnBeat = false;
                OnEighthBeat = true;
                OnSixteenthBeat = true;

            }

            else if (mySource.timeSamples > nextSixteenthSample)
            {
                CountSixteenthBeat();

                currentBeat = BeatType.SixteenthBeat;

                OnSixteenthBeat = true;
                OnBeat = false;
                OnEighthBeat = false;

            }
            else
            {

                currentBeat = BeatType.NoBeat;
                OnBeat = false;
                OnEighthBeat = false;
                OnSixteenthBeat = false;
            }

            if (Mathf.Abs(nextFourthSample - mySource.clip.samples) < loopSampleRange && !nextBeatIsOut)
            {
                nextBeatIsOut = true;
            }

            currentSample = mySource.timeSamples;
        }
        else
        {
            if (kickIn)
            {
                mySource.Play();
                childSource.Play();
            }
        }
    }

    void CountFourthBeat()
    {

        nextFourthSample += 1 / bps * mySource.clip.frequency;


        if (fourthNotesCounter == barLength)
        {
            fourthNotesCounter = 1;
        }
        else
        {
            fourthNotesCounter++;
        }

    }
    void CountEighthBeat()
    {
        nextEighthSample += 0.5f / bps * mySource.clip.frequency;

        if (eighthNotesCounter == barLength*2)
        {
            eighthNotesCounter = 1;
        }
        else
        {
            eighthNotesCounter++;
        }

    }

    void CountSixteenthBeat()
    {
        nextSixteenthSample += 0.25f / bps * mySource.clip.frequency;

        if (sixteenthNotesCounter == barLength * 4)
        {
            sixteenthNotesCounter = 1;
        }
        else
        {
            sixteenthNotesCounter++;
        }


    }

    public static void ChangeSong(bool isMuffled)
    {
        if(isMuffled)
        {
            
            mySource.mute = true;
            muffledSource.mute = false;
        }

        else
        {
            muffledSource.mute = true;
            mySource.mute = false;
        }
    }

}