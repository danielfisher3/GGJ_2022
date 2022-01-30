using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageFadeSceneswap : MonoBehaviour
{
    [SerializeField] Image imageToFade;
    float timeElapsed;
    [SerializeField]float lerpDuration = 3.0f;
    float startValue = 0;
    float endValue = 255;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
       

    }
}
