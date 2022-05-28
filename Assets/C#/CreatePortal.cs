
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CreatePortal : UdonSharpBehaviour
{

    public GameObject portal;
    public int set;//portal順番に出す
    void Start()
    {
        if (set == 1)
        {
            portal = GameObject.Find("Portal1");//プレイヤーを登録したKanriを見つける

        }
        if (set == 2)
        {
            portal = GameObject.Find("Portal2");//プレイヤーを登録したKanriを見つける

        }
    }
    void OnTriggerEnter(Collider other)
    {
        GameObject bullets = VRCInstantiate(portal);
        bullets.transform.position = this.gameObject.transform.position;
        bullets.transform.rotation = this.gameObject.transform.rotation;
        Destroy(this.gameObject);

    }
    public void Create()
    {

    }
}
