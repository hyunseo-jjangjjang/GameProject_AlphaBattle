using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    [PunRPC]
    void BulletDestory()
    {
        Destroy(gameObject);
    }
}
