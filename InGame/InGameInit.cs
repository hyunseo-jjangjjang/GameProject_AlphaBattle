using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameInit : Photon.MonoBehaviour {
    public Text ReadyStartText;
    public Camera mainCamera;

    [HideInInspector]
    public GameObject[] Player;
    private GameObject[] PlayerWeapon;

    public Image[] MyWinStatus;
    public Image[] YouWinStatus;

    public Sprite WinSprite;
    void Start()
    {
        Init();
    } 
    void Init()
    {
        for (int i = 0; i < InGameManager.instance.WinCount; i++)
        {
            MyWinStatus[i].sprite = WinSprite;
        }
        for (int i = 0; i < InGameManager.instance.LoseCount; i++)
        {
            YouWinStatus[i].sprite = WinSprite;
        }
        Player = new GameObject[2];

        Vector3 PlayerPos;
        if (PhotonNetwork.player == PhotonNetwork.masterClient)
        {
            PlayerPos = new Vector3(-14.25f, -1f, 0);
        }
        else
        {
            PlayerPos = new Vector3(20.99f, -1f, 0);
        }
        mainCamera.GetComponent<CameraFollow>().PlayerObject = PhotonNetwork.Instantiate("PlayerObject", PlayerPos, Quaternion.identity, 0);
        mainCamera.transform.position = mainCamera.GetComponent<CameraFollow>().PlayerObject.transform.position;
        Player[0] = mainCamera.GetComponent<CameraFollow>().PlayerObject;
      
        StartCoroutine("InGameReadyStart");
    }
    IEnumerator InGameReadyStart()
    {
        ReadyStartText.text = "3";
        ReadyStartText.GetComponent<Animator>().SetTrigger("GO");
        yield return new WaitForSeconds(1.0f);
        ReadyStartText.text = "2";
        ReadyStartText.GetComponent<Animator>().SetTrigger("GO");

        GameObject[] TempPlayer = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(TempPlayer.Length);
        InGameManager.instance._tex.text = "1";
        if (Player[0] != TempPlayer[0])
        {
            Player[1] = TempPlayer[0];
            Player[0] = TempPlayer[1];
            InGameManager.instance._tex.text = "6";
        }
        else
        {
            Player = TempPlayer;
            InGameManager.instance._tex.text = "2222";
        }
        //  InGameManager.instance._tex.text = "2";
        if (Player.Length == 2)
        {
            Player[1].GetComponent<PlayerControl>().enabled = false;
            InGameManager.instance._tex.text = "4";
            Player[1].GetComponentInChildren<Axis>().enabled = false;
        }

        InGameManager.instance._tex.text = "3";
        yield return new WaitForSeconds(1.0f);
        ReadyStartText.text = "1";
        ReadyStartText.GetComponent<Animator>().SetTrigger("GO");
        yield return new WaitForSeconds(1.0f);
        ReadyStartText.text = "Start!!";
        ReadyStartText.GetComponent<Animator>().SetTrigger("GO");
        yield return new WaitForSeconds(0.3f);
        ReadyStartText.enabled = false;
    }
    [PunRPC]
    void EnermyControlOff(PhotonPlayer _player)
    {
        if(_player!= PhotonNetwork.player)
        {

        }
    }
}
