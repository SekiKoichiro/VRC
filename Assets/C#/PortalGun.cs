using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PortalGun : UdonSharpBehaviour
{
    public GameObject bullet;
    public Transform muzzle;
    private int set;//setでportal1,2を順番に切り替える
    void Start()
    {
        set = 1;
    }
    public void OnPickupUseDown(VRCPlayerApi player) //AutoHoldがYesのPickupオブジェクトをつかんでいる時、トリガーを引いた際に呼ばれる関数　ownerはpicupした人に移る
    {
        fire(player);
    }
    public void fire(VRCPlayerApi player)
    {
        GameObject bullets = VRCInstantiate(bullet);
        Vector3 force;
        force = this.gameObject.transform.forward * 400;
        bullets.GetComponent<Rigidbody>().AddForce(force);
        bullets.transform.position = muzzle.position;
        bullets.transform.rotation = this.gameObject.transform.rotation;

        //bullets.transform.rotation = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation;


        switch (set)
        {
            case 1:
                bullets.GetComponent<CreatePortal>().set = 1;
                set = 2;
                break;
            case 2:
                bullets.GetComponent<CreatePortal>().set = 2;
                set = 1;
                break;
            default:
                break;
        }

    }
}
