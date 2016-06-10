using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    const string START_SCENE_NAME = "main";

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            this.loadStartScene();
    }

    private void loadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(START_SCENE_NAME, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}