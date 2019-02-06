using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollector : MonoBehaviour {

    public Text cacaNumber, goatText, barrelText, spoonText;
    public int collectedGoats = 0;
    private bool collectedBarrel, collectedSpoon, endGame;
    public Image darkSpoon, lightSpoon, darkBarrel, lightBarrel, endBox, retryBox;
    public Button endButton, retryButton;
    public CameraFollow cameraScript;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if ((collectedGoats >= 5 && collectedBarrel && collectedSpoon))                               //descomentar para activar fin rápido
        //if ((collectedGoats >= 5 && collectedBarrel && collectedSpoon) || Input.GetKeyDown(KeyCode.L))  //comentar para desactivar fin rápido
        {
            endGame = true;
            endBox.gameObject.SetActive(true);
            endButton.gameObject.SetActive(true);
            //cameraScript.enabled = false;
            //GetComponent<PlayerController>().enabled = false;

            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
        }


    }

    public void AddGoat()
    {
        collectedGoats++;
        cacaNumber.text = collectedGoats.ToString();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cabra")
        {
            goatText.gameObject.SetActive(true);

        }

        else if (other.tag == "Barril")
        {
            barrelText.gameObject.SetActive(true);
        }

        else if (other.tag == "Cuchara")
        {
            spoonText.gameObject.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cabra")
        {
            goatText.gameObject.SetActive(false);

        }

        else if (other.tag == "Barril")
        {
            barrelText.gameObject.SetActive(false);

        }

        else if (other.tag == "Cuchara")
        {
            spoonText.gameObject.SetActive(false);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.tag == "Cabra")
            {
                //FADE OUT

                //collectedGoats++;

                //cacaNumber.text = collectedGoats.ToString();

                goatText.gameObject.SetActive(false);

                other.enabled = false;
                other.GetComponent<Goat>().GetCollected();

            }

            else if (other.tag == "Barril")
            {
                collectedBarrel = true;
                darkBarrel.gameObject.SetActive(false);
                lightBarrel.gameObject.SetActive(true);

                barrelText.gameObject.SetActive(false);

                Destroy(other.gameObject);

            }

            else if (other.tag == "Cuchara")
            {
                collectedSpoon = true;
                darkSpoon.gameObject.SetActive(false);
                lightSpoon.gameObject.SetActive(true);

                spoonText.gameObject.SetActive(false);

                Destroy(other.gameObject);


            }
            if ((collectedGoats >= 5 && collectedBarrel && collectedSpoon ))

            {
                Debug.Log("End " + endGame);
                endGame = true;
                endBox.gameObject.SetActive(true);
                endButton.gameObject.SetActive(true);

                Cursor.visible = !Cursor.visible;
                Debug.Log(Cursor.visible);
            }
        }

        
    }

}
