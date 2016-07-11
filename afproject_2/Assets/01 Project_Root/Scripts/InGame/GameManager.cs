using UnityEngine;
using System.Collections;

public partial class GameManager : MonoBehaviour {
    #region Singleton

    private static GameManager _instance = null;

    public static GameManager Instance
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

    public enum GameState
    {
        Startup, // 로딩 
        PlayGame, // m_isReady = true 된 상테. 게임 라운드 시작 파업.
        GameOver, // 플레이어가 죽었을때.
    }

    public PlayerController_2d m_PlayerController;

    public bool m_isPause = false;
    public bool m_isReady = false; // 기본 라운드 설정후 시작 준비가 되었나?

    public GameState m_GameState = GameState.Startup;

    public static void DelayAction(float wait_time, System.Action call)
    {
        _instance.StartCoroutine(_DelayAction(wait_time, call));
    }
    static IEnumerator _DelayAction(float wait_time, System.Action call)
    {
        yield return new WaitForSeconds(wait_time);
        if (call != null)
        {
            call();
        }
    }

    // UI에서 시작 버튼을 누르면 호출 한다.
    public void StartGame()
    {
        Set_RoundStart();
    }

    public void Play_ClearPopup()
    {
        // 라운드 클리어 팝업 없음.
        
        Play_SetupNextRound();
    }
    IEnumerator _PlayClearPopup()
    {
        yield return new WaitForSeconds(0.8f);
//        Gui_ClearPopup popup = GuiManager.Instance.Show<Gui_ClearPopup>(GuiManager.ELayerType.Front, true);
    }

    public void PlayGameOverPopup()
    {
        if(m_GameState == GameState.PlayGame)
        {
            m_GameState = GameState.GameOver;

            m_PlayerController.RoundInit();  // 총쏘는 거 멈춤.
            EnemyManager.Instance.m_EnemySpawner.Stop();
            EnemyManager.Instance.Stop_All();

            GuiManager.Instance.Show<Gui_FailPopup>(true);
        }else
        {
            Debug.Log("~~~~~~~~~~~ No GameState.PlayGame ");
        }
    }
    IEnumerator _PlayFailPopup()
    {
       yield return new WaitForSeconds(0.5f);
//        Gui_FailPopup popup = GuiManager.Instance.Show<Gui_FailPopup>(GuiManager.ELayerType.Front, true);
    }
    
    public void PlayPause()
    {
        m_isPause = true;
        Time.timeScale = 0;
    }
    public void PlayResume()
    {
        m_isPause = false;
        Time.timeScale = 1;
    }

    public static void QuitGame()
    {
        Application.Quit();
    } 
}
