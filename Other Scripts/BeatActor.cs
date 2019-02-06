//Down Beat
//This script acts as parent for all actors in the game that move to the beat and is very reliant on BeatManager.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatActor : MonoBehaviour
{
    [HideInInspector]
    public AudioSource mySource;

    public BeatManager.BeatType beatType;
    public List<int> beatList;
    public AudioClip actSound;
    public int waitBarInterval = 1;
    public bool startImmediately;

    protected bool severalBeats;
    protected int singleBeat;
    protected bool actOnBeat = true;

    protected Vector3 savedPosition;

    [HideInInspector]
    public  int waitBarCounter = 0;
    private bool actOnBar;
    private bool waitForBars;

    protected void SetBehavior()
    {
        mySource = GetComponent<AudioSource>();

        if (beatList.Count > 0)
        {
            if (beatList.Count > 1)
            {
                severalBeats = true;
            }
            else
            {
                singleBeat = beatList[0];
            }
        }
        else
        {
            if (beatType != BeatManager.BeatType.DownBeat)
            {
                actOnBeat = false;
            }

        }

        if (waitBarInterval > 1)
        {
            waitForBars = true;
        }

        if(startImmediately)
        {
            waitBarCounter = waitBarInterval;
        }

        SaveSettings();
    }

    protected void SetPitch()
    {
        if (mySource)
        {
            if (beatType == BeatManager.BeatType.FourthBeat)
            {
                mySource.pitch = 1;
            }
            else if (beatType == BeatManager.BeatType.EighthBeat)
            {
                mySource.pitch = 2;
            }
            else if (beatType == BeatManager.BeatType.SixteenthBeat)
            {
                mySource.pitch = 4;
            }
        }
    }

    protected virtual void SaveSettings()
    {
        savedPosition = gameObject.transform.position;
    }

    protected virtual void LoadSettings()
    {
        gameObject.transform.position = savedPosition;


        if (startImmediately)
        {
            waitBarCounter = waitBarInterval;
        }

        else
        {
            waitBarCounter = 0;
        }

    }

    protected void PlaySound()
    {
        mySource.PlayOneShot(actSound);
    }

    protected void StopSound()
    {
        mySource.loop = false;
        mySource.Stop();
        
    }

    protected void LoopSound()
    {
        mySource.loop = true;
        mySource.clip = actSound;
        mySource.Play();
    }


    protected bool BeatListener()
    {
        if (BeatManager.currentBeat != BeatManager.BeatType.NoBeat)
        {
            if(waitForBars) {
                if (BeatManager.currentBeat == BeatManager.BeatType.DownBeat)
                {
                    waitBarCounter++;

                    if (waitBarCounter >= waitBarInterval)
                    {
                        actOnBar = true;
                        waitBarCounter = 0;
                    }
                    else
                    {
                        if (actOnBar)
                            actOnBar = false;
                    }


                }
            }

            if (!waitForBars || (waitForBars && actOnBar))
            {
                if (severalBeats)
                {
                    foreach (int i in beatList)
                    {
                        if (CheckBeat(i))
                        {
                            return true;
                        }
                    }
                }
                else if (CheckBeat(singleBeat))
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected bool CheckBeat(int i)
    {
        if (beatType == BeatManager.BeatType.DownBeat && BeatManager.currentBeat == BeatManager.BeatType.DownBeat)
        {
            return true;
        }
        else if (beatType == BeatManager.BeatType.FourthBeat && (BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat))
        {
            if (BeatManager.fourthNotesCounter == i)
            {
                return true;
            }
        }

        else if (beatType == BeatManager.BeatType.EighthBeat && (BeatManager.currentBeat == BeatManager.BeatType.EighthBeat || BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat))
        {
            if (BeatManager.eighthNotesCounter == i)
            {
                return true;
            }
        }

        else if (beatType == BeatManager.BeatType.SixteenthBeat && (BeatManager.currentBeat == BeatManager.BeatType.SixteenthBeat || BeatManager.currentBeat == BeatManager.BeatType.EighthBeat || BeatManager.currentBeat == BeatManager.BeatType.FourthBeat || BeatManager.currentBeat == BeatManager.BeatType.DownBeat))
        {
            if (BeatManager.sixteenthNotesCounter == i)
            {
                return true;
            }
        }

        return false;
    }
}
