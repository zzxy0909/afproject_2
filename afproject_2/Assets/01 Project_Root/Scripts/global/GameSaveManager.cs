using UnityEngine;
using System.Collections;

public class GameSaveManager : MonoBehaviour {

    #region Singleton

    private static GameSaveManager _instance = null;

    public static GameSaveManager Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    #endregion Singleton

    public enum E_SaveTagId
    {
        LastRoundNum,
        TopScore,
        
        None,
    }
    public string m_GameSaveFile01 = "GameData01.dat";
    string _FileTagFormat = "{0}?tag={1}";

    public void OnApplicationQuit()
    {
        Save();
    }

    /* We also save on application pause in iOS, as OnAppicationQuit isn't always called */
	public void OnApplicationPause()
	{
// #if UNITY_IPHONE && !UNITY_EDITOR
		Save();
// #endif
    }

    public void Save()
    {
        // 실행후 모든 설정이 완료되고 Save가 가능 한가? 기본 스테이지 설정 완료후 확인.
        if (GameManager.Instance == null || GameManager.Instance.m_isReady == false)
            return;

        ES2.Save(GameManager.Instance.m_CurrentRoundNum, string.Format(_FileTagFormat, m_GameSaveFile01, E_SaveTagId.LastRoundNum.ToString()));
        ES2.Save(GameManager.Instance.m_TopScore, string.Format(_FileTagFormat, m_GameSaveFile01, E_SaveTagId.TopScore.ToString()));

    }
    public void ClearFile()
    {
        if (ES2.Exists(m_GameSaveFile01))
        {
            ES2.Delete(m_GameSaveFile01);
        }
    }
    void Load01()
    {
        string last_round_tag = string.Format(_FileTagFormat, m_GameSaveFile01, E_SaveTagId.LastRoundNum.ToString());
        if (ES2.Exists(last_round_tag))
        {
            GameManager.Instance.m_LastRoundNum = ES2.Load<int>(last_round_tag); // string.Format(_FileTagFormat, m_GameSaveFile01, E_SaveTagId.LastRoundNum.ToString())); // m_GameSaveFile01 + "?tag=stageNum");
        }
        string top_score_tag = string.Format(_FileTagFormat, m_GameSaveFile01, E_SaveTagId.TopScore.ToString());
        if (ES2.Exists(top_score_tag))
        {
            GameManager.Instance.m_TopScore = ES2.Load<long>(top_score_tag);
        }
        
    }

    IEnumerator _CheckManager()
    {
        while(GameManager.Instance == null)
        {
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator _SetData()
    {
        yield return StartCoroutine(_CheckManager());

        // If there's scene object data to load
        if (ES2.Exists(m_GameSaveFile01))
        {
            Load01();
        }

        GameManager.Instance.SetRound_Init_StartPopup();
    }

    // Use this for initialization
    void Start () {
        
        StartCoroutine(_SetData());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
