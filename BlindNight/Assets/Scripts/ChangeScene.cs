using System.Collections;
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

    void Start()
    {
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
        sceneToLoad = "Level " + sceneNum.ToString();
        anim.SetTrigger("DoorOpened");
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
}
