using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(NetworkManager))]

public class Managers : Photon.MonoBehaviour {
    public static GameManager _gamemanager;
    public static GameManager Game
    { get { return _gamemanager; } }

    public static NetworkManager _networkmanager;
    public static NetworkManager Network
    { get { return _networkmanager; }}

    void Awake()
    {
        _gamemanager = GetComponent<GameManager>();
        _networkmanager = GetComponent<NetworkManager>();
        DontDestroyOnLoad(this.gameObject);
    }
}
