using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : MonoBehaviour
{
    public bool juggernautActivated = false;
    public bool ogreActivated = false;
    public bool angelActivated = false;
    public bool mutantActivated = false;
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
            }
        }
        if (angelActivated)
        {
            if(aAI.dead == true)
            {
                aWallAnimator.SetBool("AWallDown", true);
                bossSlider.SetActive(false);
                angelActivated = false;
            }
        }
        if (ogreActivated)
        {
            if(oAI.dead == true)
            {
                aWallAnimator.SetBool("OWallDown", true);
                bossSlider.SetActive(false);
                ogreActivated = false;
            }
        }
        if (mutantActivated)
        {
            if(mAI.dead == true)
            {
                aWallAnimator.SetBool("MWallDown", true);
                bossSlider.SetActive(false);
                mutantActivated = false;
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
    }
}
