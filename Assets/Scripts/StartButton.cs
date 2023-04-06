using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    public string sceneName;
    //GameManager _gameManager;
    public void StartGame(){
        //_gameManager = FindObjectOfType<GameManager>();
        Player.resetHPTo(10);
        SceneManager.LoadScene(sceneName);
    }
}
