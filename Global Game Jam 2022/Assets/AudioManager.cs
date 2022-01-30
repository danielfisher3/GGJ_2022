using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip laugh, wretch,gameMusic;
    public static AudioManager instance;
    AudioSource aSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        aSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(SceneManager.GetActiveScene()== SceneManager.GetSceneByName("GameScene"))
        {
            aSource.enabled = false;
        }
        else
        {
            aSource.enabled = true;
        }
    }

    public void Laugh()
    {
        AudioSource.PlayClipAtPoint(laugh, Camera.main.transform.position,1.0f);
    }
    public void PlayWretch()
    {
        AudioSource.PlayClipAtPoint(wretch, Camera.main.transform.position, 1.0f);
    }
}
