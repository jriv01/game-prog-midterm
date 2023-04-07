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
    Player _player;
    TMP_Text scoreUI;
    TMP_Text healthUI;
    TMP_Text gunAmmoUI;
    TMP_Text rockAmmoUI;
    TMP_Text dynaAmmoUI;


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
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<TextMeshProUGUI>();
        gunAmmoUI = GameObject.FindGameObjectWithTag("GunUI").GetComponent<TextMeshProUGUI>();
        rockAmmoUI = GameObject.FindGameObjectWithTag("RockUI").GetComponent<TextMeshProUGUI>();
        dynaAmmoUI = GameObject.FindGameObjectWithTag("DynaUI").GetComponent<TextMeshProUGUI>();
        scoreUI.text = "SCORE: " + score;
        healthUI.text = "HEALTH: " + health;
        SceneManager.activeSceneChanged += SceneChange;
    }

    private void SceneChange(Scene current, Scene next) {
        if(next.name == "Easy") {
            health = 100;
            score = 0;
        }

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        scoreUI = GameObject.FindGameObjectWithTag("ScoreUI").GetComponent<TextMeshProUGUI>();
        healthUI = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<TextMeshProUGUI>();
        gunAmmoUI = GameObject.FindGameObjectWithTag("GunUI").GetComponent<TextMeshProUGUI>();
        rockAmmoUI = GameObject.FindGameObjectWithTag("RockUI").GetComponent<TextMeshProUGUI>();
        dynaAmmoUI = GameObject.FindGameObjectWithTag("DynaUI").GetComponent<TextMeshProUGUI>();
        scoreUI.text = "SCORE: " + score;
        healthUI.text = "HEALTH: " + health;
    }

    public void AddScore(int value) {
        score += value;
        scoreUI.text = "SCORE: " + score;
    }

    public void ResetScore(int value) {
        score = 0;
        scoreUI.text = "SCORE: " + score;
    }

    public void resetHealth() {
        health = 100;
        print("resetHealth");
    }

    public void TakeDamage(int value) {
        health -= value; 
        health = Mathf.Clamp(health, 0, 100);

        StartCoroutine(_player.DamageTaken());

        if(health == 0) {
            SceneManager.LoadScene("Fail");
        }

        healthUI.text = "HEALTH: " + health;
    }

    public void HealPlayer(int value) {
        health += value;
        health = Mathf.Clamp(health, 0, 100);
        healthUI.text = "HEALTH: " + health;
    }

    void Update()
    {
        scoreUI.text = "SCORE: " + score;
        healthUI.text = "HEALTH: " + health;
        if(gunAmmoUI != null && _player.weaponType == "pistol") {
            gunAmmoUI.text = "" + _player.bulletCount;
        } else if(gunAmmoUI != null) {
            gunAmmoUI.text = "---";
        }


        if(rockAmmoUI != null) {
            rockAmmoUI.text = "" + _player.rockCount;
        }

        if(dynaAmmoUI != null) {
            dynaAmmoUI.text = "" + _player.dynamiteCount;
        }


#if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }
}
