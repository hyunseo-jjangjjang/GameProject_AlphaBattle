using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InGameManager : Photon.MonoBehaviour {
    #region 인스턴스
    public static InGameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;

    }
    #endregion
    public Text WinLoseText;
    private int _CurrentRound = 1;
    public int CurrentRound { get { return _CurrentRound; } set { _CurrentRound = value;

        } }

    private int _WinCount = 0;
    public int WinCount { get { return _WinCount; } set { _WinCount = value;
            WinLoseTextUpdate();
        } }

    private int _LoseCount = 0;
    public int LoseCount { get { return _LoseCount; } set { _LoseCount = value;
            WinLoseTextUpdate();
        } }

    void WinLoseTextUpdate()
    {
        WinLoseText.text = WinCount + "승 - " + LoseCount + "패";
    }

    public Text _texttext;
    public Text _tex;
}
