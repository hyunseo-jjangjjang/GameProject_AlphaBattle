using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NetworkMatch : Photon.PunBehaviour {
    public static readonly string InGameScene = "inGameScene";
    public static readonly string MainMenuScene = "mainScene";

    bool isConnecting;

    void Awake()
    {
        NetworkInit();
    }
    void NetworkInit()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    #region 매칭 시스템 
    public void Connect()
    {
        isConnecting = true;
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            PhotonNetwork.ConnectUsingSettings("1");
        }
    }
    public Image ajaj;
    IEnumerator Matching()
    {
        while (true)
        {
            yield return null;
            Debug.Log("매칭 중입니다");

            if (PhotonNetwork.room.PlayerCount == 2)
            {
                /// 씬이동 
                SoundManger.instance.Play(AudioEnum.CATCH);
                ajaj.GetComponent<Animator>().SetTrigger("rkrk");
                Invoke("Ciofenseiofn", 1.0f);
                break;
            }
        }
    }
    public void Ciofenseiofn()
    {
        PhotonNetwork.LoadLevel("FarmingScene");
    }
    public override void OnJoinedRoom()
    {
        StartCoroutine("Matching");
    }
    #endregion

    #region 네트워크 설정


    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, null);
    }

    public override void OnDisconnectedFromPhoton()
    {
        isConnecting = false;
    }


    #endregion
}
