
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CreateGun : UdonSharpBehaviour
{
    public GameObject bullet;
    public Transform muzzle;

    void Start()
    {
        if (this.gameObject.name =="CreateSMG")
        {
            bullet = GameObject.Find("SMG");//プレイヤーを登録したKanriを見つける
        }
        if (this.gameObject.name == "CreatePortalGun")
        {
            bullet = GameObject.Find("PortalGun");//プレイヤーを登録したKanriを見つける
        }
    }
    public void Interact() //AutoHoldがYesのPickupオブジェクトをつかんでいる時、トリガーを引いた際に呼ばれる関数　ownerはpicupした人に移る
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "fire");
    }
    public void fire()
    {
        GameObject bullets = VRCInstantiate(bullet);
        Vector3 force;
        force = this.gameObject.transform.forward;
        bullets.GetComponent<Rigidbody>().AddForce(force);
        bullets.transform.position = muzzle.position;

    }
}
