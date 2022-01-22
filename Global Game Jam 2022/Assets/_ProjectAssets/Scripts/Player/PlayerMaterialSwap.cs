using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Controller))]
public class PlayerMaterialSwap : MonoBehaviour
{
    Player_Controller pCoontrol;
    [Tooltip("List for Renderers in Helmet")] [SerializeField] List<Renderer> helmetObjects;
    [Tooltip("List for Renderers in Armor1")] [SerializeField] List<Renderer> armor1Objects;
    [Tooltip("List for Renderers in Armor2")] [SerializeField] List<Renderer> armor2Objects;
    [Tooltip("Originial Material for DivinityArmor1")] [SerializeField] Material divineAMaterial1;
    [Tooltip("Originial Material for DivinityArmor2")] [SerializeField] Material divineAMaterial2;
    [Tooltip("Originial Material for DivinityHelmet")] [SerializeField] Material divineHelmetMaterial;
    [Tooltip("Originial Material for EvilArmor1")] [SerializeField] Material evilAMaterial1;
    [Tooltip("Originial Material for EvilArmor2")] [SerializeField] Material evilAMaterial2;
    [Tooltip("Originial Material for EvilHelmet")] [SerializeField] Material evilHelmetMaterial;


    private void Awake()
    {
        pCoontrol = GetComponent<Player_Controller>();
    }
    void Update()
    {
        if (pCoontrol.demonForm)
        {
            foreach(Renderer r in helmetObjects)
            {
                r.material = evilHelmetMaterial;
            }
            foreach (Renderer r in armor1Objects)
            {
                r.material = evilAMaterial1;
            }
            foreach (Renderer r in armor2Objects)
            {
                r.material = evilAMaterial2;
            }
        }
        else if(!pCoontrol.demonForm)
        {
            foreach (Renderer r in helmetObjects)
            {
                r.material = divineHelmetMaterial;
            }
            foreach (Renderer r in armor1Objects)
            {
                r.material = divineAMaterial1;
            }
            foreach (Renderer r in armor2Objects)
            {
                r.material = divineAMaterial2;
            }
        }
    }
}
