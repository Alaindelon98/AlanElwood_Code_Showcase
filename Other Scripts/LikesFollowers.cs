//-----INSTANT-----//
//This script adds likes and followers and plays a scripted random animation with the numbers
//depending on what photo the player takes in the game.

//Not Only Games Jam

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LikesFollowers : MonoBehaviour
{

    [SerializeField]
    Text likesText, followersText;
    int likesTarget;
    int likesNum, followersNum;

    public int[] likesArray, followersArray;
    public GameManager gm;

    int currentPhoto;
    void Start()
    {
        likesNum = 0;
        followersNum = 17;

        likesText.text = likesNum.ToString() + " Likes";
        followersText.text = followersNum.ToString() + " Following";
    }


    public void TakePhoto(bool isBoring, bool isPersonBoring) {
        
            if (isPersonBoring)
            {
                currentPhoto = 7;
            }
            else if (isBoring)
            {
                currentPhoto = 0;
            }
            else
            {
                switch (gm.m_currentDay)
                {
                    case GameManager.S_Days.Beating: currentPhoto = 6; break;
                    case GameManager.S_Days.Fall: currentPhoto = 2; break;
                    case GameManager.S_Days.FootTrip: currentPhoto = 4; break;
                    case GameManager.S_Days.Gone: currentPhoto = 8; break;
                }
            }
            ChangePhotoText();     
    }



    void ChangePhotoText()
    {
        likesNum = 0;
        likesTarget = likesArray[currentPhoto] + Random.Range(0, 6);
        if (currentPhoto != 0 && currentPhoto != 7)
        {
            //followersNum = followersArray[currentPhoto];
            Debug.Log("Start counting followers");
            StartCoroutine(AddFollowers());


        }
        StartCoroutine(AddLikes());

        likesText.text = likesNum.ToString() + " Likes";
        followersText.text = followersNum.ToString() + " Following";

    }

    IEnumerator AddLikes()
    {
        while (likesNum < likesTarget)
        {
            likesNum += Random.Range(1, 10);


            if (likesNum >= likesTarget)
            {
                likesNum = likesTarget;
                likesText.text = likesNum.ToString() + " Likes";
            }

            else
            {
                likesText.text = likesNum.ToString() + " Likes";

                yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
            }
        }
        yield break;
    }

    IEnumerator AddFollowers()
    {
        while (followersNum < followersArray[currentPhoto])
        {

            followersNum++;





            if (followersNum >= followersArray[currentPhoto])
            {
                followersNum = followersArray[currentPhoto];
                followersText.text = followersNum.ToString() + " Followers";


            }

            else
            {
                followersText.text = followersNum.ToString() + " Followers";


                yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));
            }

            //if (iles)
        }
    
    yield break;
    }
}