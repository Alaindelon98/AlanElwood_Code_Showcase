using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSchangeScript : MonoBehaviour {

    public bool forceIt;

    private void Update()
    {
        ForceVisible(forceIt);
    }
    public void StartAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void VisibleCursor(bool visible)
    {
        Cursor.visible = visible;
        if(visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void ForceVisible(bool forceIt)
    {
        if (forceIt)
        {
            if (Input.GetMouseButtonDown(0) && !Cursor.visible)
            {
                VisibleCursor(true);
            }
        }
    }
}
