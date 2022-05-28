
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Collections;
using System.Collections.Generic;


public class Bullet : UdonSharpBehaviour
{
    public GameObject aa;
    public bool hantei;
    int damage = 3;
    //[UdonSynced]//同期する
    //private string name;

    void Start()
    {
        if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
    }

    void OnPlayerTriggerEnter (VRCPlayerApi player)
    {

        if (hantei)
        {
            Kanri tmp = GameObject.Find("Kanri").GetComponent<Kanri>(); ;//プレイヤーを登録したKanriを見つける

            tmp.Damage(player.displayName, damage);

            //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move0));
            //name = player.displayName;
        }
        //tmp.Display();

        hantei = false;
        Destroy(this.gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
