using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   public void SetFade()
    {
        anim.SetBool("ChangedScenes", true);
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
