using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
public class NetworkManager : Photon.MonoBehaviour {
    public GameObject NetworkFailedObject;
    public Text NetworkTimeEndText;
    private bool isNetworking = true;
    void Update()
    {
        NetworkStateCheck();
    }

    private void NetworkStateCheck()
    {
        if (PhotonNetwork.connectionState == ConnectionState.Disconnected && isNetworking)
        {
            NetworkFailed();
        }
    }

    public void NetworkFailed()
    {
        isNetworking = false;
        NetworkFailedObject.SetActive(true);
        StartCoroutine("NetworkTimeEnd");
    }
    IEnumerator NetworkTimeEnd()
    {
        for (int i = 10; i > 0; i--)
        {
            NetworkTimeEndText.text = i + "초 후 게임이 종료됩니다.";
            yield return new WaitForSeconds(1.0f);
        }
        Managers.Game.GameEnd();
    }
}
