using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenButtons : MonoBehaviour
{
     Animator anim;
    bool game, credits, controls;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        

    }
    public void SetFade()
    {
        anim.SetBool("ChangedScene", true);
    }
    public void ChangeToGame()
    {
        game = true;
    }
    public void ChangeToCredits()
    {
        credits = true;
    }
    

    public void ChangeToControls()
    {
        controls = true;
    }
    public void SceneChange()
    {
        if (game)
        {
            SceneManager.LoadScene("GameScene");
            game = false;
        }
        if (credits)
        {
            SceneManager.LoadScene("Credits");
            credits = false;
        }
        if (controls)
        {
            SceneManager.LoadScene("Control");
            controls = false;
        }
    }
}
