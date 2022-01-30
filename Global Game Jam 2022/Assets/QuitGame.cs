using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayWretch();
    }
    public void QuitTheGame()
    {
        Application.Quit();
    }
}
