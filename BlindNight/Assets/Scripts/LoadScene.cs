using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    ChangeScene changeScene;

    private void Start()
    {
        changeScene = FindObjectOfType<ChangeScene>();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(changeScene.sceneToLoad, LoadSceneMode.Single);
        changeScene.sceneToLoad = null;
    }
}
