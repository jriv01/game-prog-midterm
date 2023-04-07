using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource _audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            _audioSource = GetComponent<AudioSource>();
            SceneManager.activeSceneChanged += SceneChange;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SceneChange(Scene current, Scene next) {
        if(next.name == "Fail") {
            Destroy(gameObject);
        }
    }
}
