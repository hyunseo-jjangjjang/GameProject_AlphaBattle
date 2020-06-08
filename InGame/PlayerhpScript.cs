using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerhpScript : MonoBehaviour {
    [HideInInspector]
    public float HP;
    [HideInInspector]
    public float EHP;

    public Image MyHpImage;
    public Image YouHpImage;
    void Start () {
        HP = 20.0f;
        EHP = 20.0f;
	}
    void HpImageUpdate()
    {
        MyHpImage.transform.localScale = new Vector2(HP / 20.0f, 1);
        YouHpImage.transform.localScale = new Vector2(EHP / 20.0f, 1);
    }
    [PunRPC]
    void Attack(PhotonPlayer AttackPlayer)
    {
        if (PhotonNetwork.player == AttackPlayer)
        {
            HP--;
            if (HP <= 0)
            {
                GetComponent<InGameInit>().Player[0].GetComponent<Animator>().SetBool("Run", false);
                GetComponent<InGameInit>().Player[0].GetComponent<Animator>().SetBool("Jump", false);
                GetComponent<InGameInit>().Player[0].GetComponent<Animator>().SetBool("DIE", true);
                SoundManger.instance.Play(AudioEnum.DEATH);
                Invoke("fnfn", 3.0f);
            }
        }
        else
        {
            EHP--;
        }
        HpImageUpdate();
    }

    public void fnfn()
    {
        GetComponent<PhotonView>().RPC("GameEnd", PhotonTargets.All, PhotonNetwork.player);
    }
}
