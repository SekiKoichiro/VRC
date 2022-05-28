
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Portal : UdonSharpBehaviour
{
    public GameObject portal;
    public bool before;
    public Vector3 position;
    public Quaternion rotation;

    void Start()
    {
        GameObject dd;//既存のポータルを消すための処理
        if (this.gameObject.name == "Portal1(Clone)")
        {
            dd = GameObject.Find("Portal1(Clone)");//プレイヤーを登録したKanriを見つける
            Portal pp = dd.GetComponent<Portal>();
            pp.destroy();
        }
        if (this.gameObject.name == "Portal2(Clone)")
        {
            dd = GameObject.Find("Portal2(Clone)");//プレイヤーを登録したKanriを見つける
            Portal pp = dd.GetComponent<Portal>();
            pp.destroy();
        }
        before = true;

    }
    void OnPlayerTriggerEnter(VRCPlayerApi player)
    {

        if (this.gameObject.name == "Portal1(Clone)")
        {
            portal = GameObject.Find("Portal2(Clone)");//プレイヤーを登録したKanriを見つける
            //portal = GameObject.Find("spawn2(Clone)");//プレイヤーを登録したKanriを見つける
        }
        if (this.gameObject.name == "Portal2(Clone)")
        {
            portal = GameObject.Find("Portal1(Clone)");//プレイヤーを登録したKanriを見つける
            //portal = GameObject.Find("spawn1(Clone)");//プレイヤーを登録したKanriを見つける

        }
        Vector3 tmp1 = portal.transform.position;
        Quaternion tmp2 = portal.transform.rotation;
        Quaternion p = Quaternion.Euler(0f, 180f, 0f);
        Quaternion tmp3;

        tmp3 = tmp2 * p;
        tmp1.y += 0.5f;
        //tmp1.x += 1.2f * Mathf.Cos(tmp2.y + 90);//float sin = Mathf.Sin(Time.time);
        //tmp1.z -= 1.2f * Mathf.Sin(tmp2.y + 90);//float sin = Mathf.Sin(Time.time);
        //tmp2.y *= -1;


        player.TeleportTo(tmp1, tmp3);//宣言で登録したオブジェの場所 portal.transform.rotation
    }
    public void destroy()
    {
        if (before){
            Destroy(this.gameObject);
        }
    }
}
