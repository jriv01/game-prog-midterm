using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialButton : MonoBehaviour
{
    public string sceneName;
    GameManager _gameManager;
    public void Tutorial(){
        _gameManager = FindObjectOfType<GameManager>();
        SceneManager.LoadScene(sceneName);
    }
}
