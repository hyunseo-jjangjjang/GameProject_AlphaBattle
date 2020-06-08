using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FarmingPercent : MonoBehaviour {
    public Text MyPercent;
    public Text YouPercent;

    [HideInInspector]
    public int MyBreakNum = 0;
    [HideInInspector]
    public int YouBreakNum = 0;

    public static FarmingPercent instacne;

    private void Start()
    {
        if(instacne == null)
        {
            instacne = this;
        }
    }

    public void BreakUpdate()
    {
        GetComponent<PhotonView>().RPC("BreakNumUpdate", PhotonTargets.All, PhotonNetwork.player);
    }
    [PunRPC]
    public void BreakNumUpdate(PhotonPlayer _player)
    {
        if(_player == PhotonNetwork.player)
        {
            MyBreakNum += 1;
            MyPercent.text = "나의 진행률 : " + (float)MyBreakNum / 2.0f * 100.0f + "%";
        }
        else
        {
            YouBreakNum += 1;
            YouPercent.text = "남의 진행률 : " + (float)YouBreakNum / 2.0f * 100.0f + "%";
        }
    }
    
    
}
