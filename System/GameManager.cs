using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour {
    public void GameEnd()
    {
        Application.Quit();
    }
}
