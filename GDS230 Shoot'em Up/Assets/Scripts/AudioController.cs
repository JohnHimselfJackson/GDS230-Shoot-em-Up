using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    Scene myScene;
    int myIndex;
    public AudioSource myAudioSource;
    public AudioClip menuClip;
    public AudioClip secondClip;
    public AudioClip gameClip;
    public AudioClip creditsClip;


    void Awake()
    {
        myScene = SceneManager.GetActiveScene();
        myIndex = myScene.buildIndex;
        Debug.Log(myAudioSource.clip.name);
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        HandleAudio();
        Debug.Log(myIndex);
        myScene = SceneManager.GetActiveScene();
        myIndex = myScene.buildIndex;
    }

    void HandleAudio()
    {
        switch (myIndex)
        {
            case 5:
                NotDead();
                myAudioSource.clip = creditsClip;
                break;
            case 4:
                NotDead();
                myAudioSource.clip = gameClip;
                break;
            case 3:
                NotDead();
                myAudioSource.clip = secondClip;
                break;
            case 2:
                NotDead();
                myAudioSource.clip = secondClip;
                break;
            case 1:
                NotDead();
                myAudioSource.clip = menuClip;
                break;
            case 0:
                NotDead();
                myAudioSource.clip = menuClip;
                break;
            default:
                break;
        }
    }

    void NotDead()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
