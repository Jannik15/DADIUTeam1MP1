﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ChangeScene : MonoBehaviour
{
    Animator anim;
    Animator fadeAnim;
    [HideInInspector] public string sceneToLoad;
    public UnityEvent sceneEvent;
    CharacterMovement player;

    void Start()
    {
        if(PlayerPrefs.GetString("savedLevel") == "") {
            PlayerPrefs.SetString("savedLevel", "Level 0");
        }
        player = FindObjectOfType<CharacterMovement>();
        anim = GetComponent<Animator>();
        fadeAnim = GameObject.Find("BlackFade").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            sceneEvent.Invoke();
    }

    public void PlayAnimation(int sceneNum)
    {
        anim.SetTrigger("DoorOpened");
        sceneToLoad = "Level " + sceneNum.ToString();

        PlayerPrefs.SetString("savedLevel", sceneToLoad);

        AkSoundEngine.PostEvent("Play_Door_Open", gameObject);
        player.FreezePlayer();
    }

    public void FadeToNextScene()
    {
        if (sceneToLoad != null)
            fadeAnim.SetTrigger("FadeOut");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        sceneToLoad = null;
    }

    public void SetSceneToLoad(string sceneName)
    {
        sceneToLoad = sceneName;
    }


    public void LoadSavedScene() {
        string savedLevel = PlayerPrefs.GetString("savedLevel");
        SceneManager.LoadScene(savedLevel, LoadSceneMode.Single);

    }


}
