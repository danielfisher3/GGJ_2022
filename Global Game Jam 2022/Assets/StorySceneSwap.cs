using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneSwap : MonoBehaviour
{
    Animator anim;
    float timeToChange = 10.0f;
    float Timer = 0;
    bool storystarted = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (storystarted)
        {
            Timer += Time.deltaTime;
            if(Timer >= timeToChange)
            {
                anim.SetBool("ChangeToNext", true);
                storystarted = false;
                Timer = 0;
            }
        }
    }
    public void ChangeSceneTOGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
