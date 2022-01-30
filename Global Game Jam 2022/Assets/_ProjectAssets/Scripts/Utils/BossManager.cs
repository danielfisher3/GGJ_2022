using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BossManager : MonoBehaviour
{
    public bool juggernautActivated = false;
    public bool ogreActivated = false;
    public bool angelActivated = false;
    public bool mutantActivated = false;
    float Timer = 0;
    [SerializeField] GameObject bossSlider;
    [SerializeField] GameObject juggerPrefab;
    [SerializeField] GameObject ogrePrefab;
    [SerializeField] GameObject angelPrefab;
    [SerializeField] GameObject mutantPrefab;

    [SerializeField] GameObject jActivator;
    [SerializeField] GameObject oActivator;
    [SerializeField] GameObject aActivator;
    [SerializeField] GameObject mActivator;
    Animator aWallAnimator =  null;
    [SerializeField] GameObject jArenaWall;
    [SerializeField] GameObject oArenaWall = null;
    [SerializeField] GameObject aArenaWall;
    [SerializeField] GameObject mArenaWall = null;

    [SerializeField] JuggerNautAI jAI;
    [SerializeField] AngelAI aAI;
    [SerializeField] OgreAI oAI;
    [SerializeField] MutantAI mAI;

    [SerializeField] GameObject checkMark1, checkMark2, checkMark3, checkMark4;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        ActivationBosses();
        if (juggernautActivated)
        {
            if (jAI.dead == true)
            {
                aWallAnimator.SetBool("JWallDown", true);

                bossSlider.SetActive(false);
                juggernautActivated = false;
                checkMark1.SetActive(true);
            }
        }
        if (angelActivated)
        {
            if(aAI.dead == true)
            {
                aWallAnimator.SetBool("AWallDown", true);
                bossSlider.SetActive(false);
                angelActivated = false;
                checkMark2.SetActive(true);
            }
        }
        if (ogreActivated)
        {
            if(oAI.dead == true)
            {
                aWallAnimator.SetBool("OWallDown", true);
                bossSlider.SetActive(false);
                ogreActivated = false;
                checkMark3.SetActive(true);
            }
        }
        if (mutantActivated)
        {
            if(mAI.dead == true)
            {
                aWallAnimator.SetBool("MWallDown", true);
                bossSlider.SetActive(false);
                mutantActivated = false;
                checkMark4.SetActive(true);
            }
        }

    }

    private void ActivationBosses()
    {
        if (juggernautActivated)
        {
            jActivator.SetActive(false);
            jArenaWall.SetActive(true);
            juggerPrefab.SetActive(true);
            aWallAnimator = jArenaWall.GetComponent<Animator>();
        }

        if (mutantActivated)
        {
            mActivator.SetActive(false);
            mArenaWall.SetActive(true);
            mutantPrefab.SetActive(true);
            aWallAnimator = mArenaWall.GetComponent<Animator>();
            

        }

        if (angelActivated)
        {
            aActivator.SetActive(false);
            aArenaWall.SetActive(true);
            angelPrefab.SetActive(true);
            aWallAnimator = aArenaWall.GetComponent<Animator>();

        }

        if (ogreActivated)
        {
            oActivator.SetActive(false);
            oArenaWall.SetActive(true);
            ogrePrefab.SetActive(true);
            aWallAnimator = oArenaWall.GetComponent<Animator>();

        }

        if(jAI.dead && aAI.dead && mAI.dead && oAI.dead)
        {
            Timer += Time.deltaTime;
            if (Timer >= 5.0f)
            {
                SceneManager.LoadScene("DeathKing");
                Timer = 0;
            }
        }
    }
}
