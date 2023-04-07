using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RockPath : MonoBehaviour
{
    public string levelToLoad;
    public int nextSceneToLoad;

    void Start(){
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            SceneManager.LoadScene(levelToLoad);
            if(nextSceneToLoad > PlayerPrefs.GetInt("levelAt")){
                PlayerPrefs.SetInt("levelAt", nextSceneToLoad);
            }
        }
    }

}
