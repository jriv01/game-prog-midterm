using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        // Once you quit, the levels will be relocked.
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
