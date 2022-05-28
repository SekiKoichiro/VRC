using UnityEngine;
using UnityEngine.UI;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;

public class Kanri : UdonSharpBehaviour
{
    [UdonSynced]//同期する
    public int[] HP;
    [UdonSynced]//同期する
    private int a;

    public Text text;
    public GameObject Spawn;
    private int maxPlayers = 4;
    public VRCPlayerApi[] elements;
    private VRCPlayerApi tmp;

    private string owner;
    private bool first;



    void Start()
    {
        elements = new VRCPlayerApi[maxPlayers];
        HP = new int[maxPlayers];
    }
    void Update()
    {
        Display();
        owner = Networking.GetOwner(gameObject).displayName;


    }
    public void OnPlayerJoined(VRCPlayerApi player)//入った人のみにオーナーを渡す
    {

    }
    public void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        Add(player);
    }
    public void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        Remove(player);
    }
    // FIXME: I want to using List<T> :(
    private void Add(VRCPlayerApi element)
    {
        for (int i = 0;i < maxPlayers;i++)
        {
            if (elements[i] == null)//上から探し空いているところに入れる
            {
                //if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                HP[i] = 10;
                elements[i] = element;
                break;
            }
        }
    }

    private void Remove(VRCPlayerApi element)
    {

        for (int i = 0; i < maxPlayers; i++)
        {
            if (elements[i] != null) //elements[o]がnoneだとバグる
            {
                if (elements[i].displayName == element.displayName)//退席したプレイヤーをVRCPlayerApi[]から外す
                {
                    // if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                    HP[i] = 0;
                    elements[i] = null;
                    break;
                }
            }

        }
    }

    public void Display()
    {
        string[] tmp;
        tmp = new string[maxPlayers];
        for (int i = 0; i < maxPlayers; i++)
        {
            if (elements[i] != null)//名前読み取る
            {
                tmp[i] = elements[i].displayName;
            }
            else//tmpが空だとtext更新できないので適当にnone入れる
            {
                tmp[i] = "None";
            }
        }

        text.text = (tmp[0] + "  HP = " + HP[0] + "\n" + tmp[1] + "  HP = " + HP[1] + "\n" + tmp[2] + "  HP = " + HP[2] + "\n" + tmp[3] + "  HP = " + HP[3] + "\n"+"OwnerName " + owner+"\n");

    }
    public void Damage(string name,int damage)//玉から名前とダメージを取得し名前を参照しダメージ与える
    {

        for (int i = 0; i < 4; i++)
        {
            if (elements[i].displayName == name)//当たった人の名前探す
            {
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                HP[i] -= damage;//Kanriかオーナーでないと同期変数HPをいじることができない
                                //tmp.Damage(player.displayName, damage);
                if (HP[i] <= 0)
                {
                    //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move));
                    //a = i;
                    switch (i)//オーナーをテレポートする人に渡す
                    {
                        case 0:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move0));
                            break;
                        case 1:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move1));
                            break;
                        case 2:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move2));
                            break;
                        case 3:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move3));
                            break;
                        default:
                            break;
                    }
                }

                break;
            }
        }

    }
    public void Move0()
    {
        elements[0].TeleportTo(Spawn.transform.position, Spawn.transform.rotation);//宣言で登録したオブジェの場所
    }
    public void Move1()
    {
        elements[1].TeleportTo(Spawn.transform.position, Spawn.transform.rotation);//宣言で登録したオブジェの場所
    }
    public void Move2()
    {
        elements[2].TeleportTo(Spawn.transform.position, Spawn.transform.rotation);//宣言で登録したオブジェの場所
    }
    public void Move3()
    {
        elements[3].TeleportTo(Spawn.transform.position, Spawn.transform.rotation);//宣言で登録したオブジェの場所
    }
    private void Memo()
    {
        //if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        if (Networking.IsOwner(this.gameObject))
        {
            elements[a].TeleportTo(Spawn.transform.position, Spawn.transform.rotation);//宣言で登録したオブジェの場所
        }
        else
        {
            // オブジェクトの所有者にOnClickメソッドを処理するように依頼するコード
            //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move));
        }
        //Networking.LocalPlayer
        for (int i = 0; i < maxPlayers; i++)
        {
            if (elements[i].displayName == name)//当たった人の名前探す
            {

                //HP[i] -= damage;
                if (HP[i] <= 0)
                {
                    //SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move));
                    //a = i;
                    switch (i)//オーナーをテレポートする人に渡す
                    {
                        case 0:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move0));
                            break;
                        case 1:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move1));
                            break;
                        case 2:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move2));
                            break;
                        case 3:
                            Networking.SetOwner(elements[i], this.gameObject);
                            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.Owner, nameof(Move3));
                            break;
                        default:
                            break;
                    }
                }
                break;
            }

        }
    }


    //if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
}
