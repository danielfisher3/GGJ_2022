using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    Player_Controller pControl;
    float Timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        pControl = FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pControl.dead)
        {
            Timer += Time.deltaTime;
            if(Timer >= 5.0f)
            {
                SceneManager.LoadScene("Death");
                Timer = 0;
            }
        }
    }
}
