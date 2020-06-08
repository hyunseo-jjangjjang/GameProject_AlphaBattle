using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InGameController : Photon.MonoBehaviour {
    public static InGameController InGame;

    public InGameInit InGameInitScript;
    public Text RoundText;
    public Text TimeText;
    private int Time = 60;

    private bool isEnd = false;
    private void Awake()
    {
        if (InGame == null)
            InGame = this;
    }
    void Start()
    {
        Init();
        SoundManger.instance.Play(AudioEnum.START);
    }
    void Init()
    {
        RoundText.text = "ROUND " + InGameManager.instance.CurrentRound;
        Time = 60;
        StartCoroutine("TimePlay");
        InGameInitScript = GetComponent<InGameInit>();
    }
    IEnumerator TimePlay()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            Time--;
            TimeText.text = Time.ToString();
            if(Time == 0)
            {
                Debug.Log("현재 라운드수수수수수" + InGameManager.instance.CurrentRound);
                if (InGameManager.instance.CurrentRound == 3)
                {
                    Debug.Log("333333");
                    if (PhotonNetwork.isMasterClient)
                    {
                        PhotonNetwork.Destroy(InGameManager.instance.gameObject);
                    }
                    else
                    {
                        PhotonNetwork.LeaveRoom();
                    }
                    PhotonNetwork.LoadLevel("mainScene");
                }
                else
                {
                    Debug.Log("44444");
                    InGameManager.instance.CurrentRound++;
                    // 초기화 
                    Init();

                    Cursor.visible = true;
                    PhotonNetwork.LoadLevel("RealMixScene");
                }
            }
        }
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            BulletHit();
        }
    }
    public Image ajaj;
    public void BulletHit()
    {
        GetComponent<PhotonView>().RPC("Attack", PhotonTargets.All, PhotonNetwork.player);
    }

    private void RoundEnd()
    {

    }
    [PunRPC]
    void GameEnd(PhotonPlayer LosePlayer)
    {
        if (!isEnd)
        {
            isEnd = true;
            if (PhotonNetwork.player != LosePlayer)
            {
                SoundManger.instance.Play(AudioEnum.WIN);
                InGameManager.instance.WinCount++;
            }
            else
            {
                SoundManger.instance.Play(AudioEnum.LOSE);
                InGameManager.instance.LoseCount++;
            }
            Debug.Log("현재 라운드수수수수수" + InGameManager.instance.CurrentRound);
            if (InGameManager.instance.CurrentRound == 3)
            {
                Debug.Log("333333");
                if (PhotonNetwork.isMasterClient)
                {
                    PhotonNetwork.Destroy(InGameManager.instance.gameObject);
                }
                else
                {
                    PhotonNetwork.LeaveRoom();
                }
                ajaj.GetComponent<Animator>().SetTrigger("rkrk");
                Cursor.visible = true;
                Invoke("enenenen", 1.0f);
            }
            else
            {
                InventoryMng.instance.UseWea = 1;
                InventoryMng.instance.UseAdj = 0;
                Debug.Log("44444");
                InGameManager.instance.CurrentRound++;
                // 초기화 
                Init();

                ajaj.GetComponent<Animator>().SetTrigger("rkrk");
                Cursor.visible = true;
                Invoke("enen", 1.0f);
            }
        }

    }
    public void enenenen()
    {
        PhotonNetwork.LoadLevel("mainScene");
    }
    public void enen()
    {

        PhotonNetwork.LoadLevel("RealMixScene");
    }

}
