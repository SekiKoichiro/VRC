using UnityEngine;
using UnityEngine.UI;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;


public class Fire : UdonSharpBehaviour
{
    public GameObject bullet;
    public Transform muzzle;
    public bool hantei;

    void Start()
    {
        
    }
    public void OnPickupUseDown(GameObject player) //AutoHoldがYesのPickupオブジェクトをつかんでいる時、トリガーを引いた際に呼ばれる関数　ownerはpicupした人に移る
    {
        hantei = true;
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "fire");
    }
    public void fire()
    {
        GameObject bullets = VRCInstantiate(bullet);
        Vector3 force;
        force = this.gameObject.transform.forward * 800;
        bullets.GetComponent<Rigidbody>().AddForce(force);
        bullets.transform.position = muzzle.position;
        if (hantei)
        {
            bullets.GetComponent<Bullet>().hantei = true;//同期された球が複数ある　全てDamageを行ってしまうため　hanteiを変え発射した人のみでdamageを行う
            hantei = false;
        }

    }
}
