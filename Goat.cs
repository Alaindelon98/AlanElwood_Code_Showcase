using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goat : MonoBehaviour {

    private AudioSource _audioSource;
    //private Renderer _renderer;
    private Transform goatParts;
    public AudioClip[] goatCallingSounds;

    public GameObject player;

    public Camera myCam;
    public GameObject playerRender;
    public PlayerController moveScript;
    public PlayerCollector collectScript;
    public Animation goatShakeAnim;

    private float callTimer;

    private bool shakingGoat;
	

	void Start () {
        _audioSource = GetComponent<AudioSource>();
        //_renderer = GetComponent<Renderer>();
        goatParts = transform.GetChild(2);
        myCam = gameObject.transform.GetChild(0).GetComponent<Camera>();
        goatShakeAnim = gameObject.transform.GetChild(1).GetComponent<Animation>();
        callTimer = Random.Range(3, 10);
        playerRender = player.transform.GetChild(0).gameObject;
        moveScript = player.GetComponent<PlayerController>();
        collectScript = player.GetComponent<PlayerCollector>();
    }
	
	void Update () {

        callTimer -= Time.deltaTime;

        if(callTimer <= 0)
        {
            int num = Random.Range(0, 2);
            _audioSource.PlayOneShot(goatCallingSounds[num]);
            callTimer = Random.Range(3, 10);

        }

        if(shakingGoat && !goatShakeAnim.isPlaying)
        {
            StopCollection();
        }
    }

    public void GetCollected()
    {
        //_renderer.enabled = false;
        foreach (Transform child in goatParts)
        {
            child.GetComponent<MeshRenderer>().enabled = false;
        }

        playerRender.SetActive(false);
        moveScript.enabled = false;
        //Camera.main.enabled = false;
        
        myCam.gameObject.SetActive(true);

        //FADE IN
        goatShakeAnim.gameObject.SetActive(true);
        goatShakeAnim.Play();

        shakingGoat = true;

    }

    private void StopCollection()
    {
        //FADE OUT

        myCam.enabled = false;
        collectScript.AddGoat();
        //collectScript.collectedGoats++;
        //collectScript.text = collectScript.collectedGoats.ToString();
        //Camera.main.gameObject.SetActive(true);
        Destroy(goatShakeAnim.gameObject, 3f); //fade out time
        Destroy(gameObject);
        shakingGoat = false;
        playerRender.SetActive(true);
        moveScript.enabled = true;
        //FADE IN MAIN
    }
}
