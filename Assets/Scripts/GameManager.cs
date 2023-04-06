using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int health = 100;
    GameObject _player;

    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.activeSceneChanged += SceneChange;
    }

    private void SceneChange(Scene current, Scene next) {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void AddScore(int value) {
        score += value;
    }

    public void ResetScore(int value) {
        score = 0;
    }

    public void TakeDamage(int value) {
        health -= value; 
        health = Mathf.Clamp(health, 0, 100);

        StartCoroutine(_player.GetComponent<Player>().DamageTaken());

        if(health == 0) {
            SceneManager.LoadScene("Fail");
        }
    }

    void Update()
    {
#if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }
}
