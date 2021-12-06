using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string SceneName;
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(SceneName);
    }
}
