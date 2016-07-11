using UnityEngine;
using System.Collections;

public partial class GameManager : MonoBehaviour {

    public RoundInfo m_RoundInfo;
    public int m_CurrentRoundNum;
    [HideInInspector]
    public int m_LastRoundNum = 1;
    public bool m_isContinue = false;


    //Gui_Bottom _Gui_Bottom;
    Gui_TopFrame _Gui_TopFrame;
    Gui_StartPopup _Gui_StartPopup;

    public void Play_SetupNextRound()
    {
        SetRound_Init_StartPopup(m_CurrentRoundNum + 1);
    }

    // 파일 , 데이터 로딩 후 시작시 호출 된다. // 세이브 데이터 로 설정..
    public void SetRound_Init_StartPopup()
    {
        if(m_isContinue == false)
        {
            SetRound_Init_StartPopup(1);
        }
        else
        {
            SetRound_Init_StartPopup(m_LastRoundNum);
        }
    }
    public void SetRound_Init_StartPopup(int n)
    {   
        SetRound_Init(n);
        _Gui_StartPopup.Show(true);

        GameSaveManager.Instance.Save();
    }
    void Set_LastRound(int n)
    {
        m_LastRoundNum = n;
    }
    

    // 파일 , 데이터 로딩 후 시작시 호출 된다. // 세이브 데이터 로 설정..
    public void SetRound_Init(int n)
    {
        m_CurrentRoundNum = n;
        m_RoundInfo.m_Num = m_CurrentRoundNum;
        Set_LastRound(n);

        // m_StageInfo 가 설정 된후 UI 데이터 설정.
        //if (_Gui_Bottom == null)
        //{
        //    _Gui_Bottom = GuiManager.Instance.Find<Gui_Bottom>();
        //}
        if (_Gui_StartPopup == null)
        {
            _Gui_StartPopup = GuiManager.Instance.Find<Gui_StartPopup>();
        }
        if (_Gui_TopFrame == null)
        {
            _Gui_TopFrame = GuiManager.Instance.Find<Gui_TopFrame>();
        }
        // round 초기화
        //        EnemyManager.Instance.RemoveAll();
        EnemyManager.Instance.PlaySpawnReady();
//        m_isStageMissionClear = false;
//        ClearMissionKillValue();
        
        PlayResume();

        m_PlayerController.RoundInit();

        //===========================
        SetRoundData_current();
        _Gui_TopFrame.UpdateText();

        m_isReady = true;
        m_GameState = GameState.PlayGame;  // 앞단계는 GameState.Startup 혹은 GameOver 일수 있음.
    }
    
    public void SetRoundData_current()
    {
        // order
        SetOrderType();
//        _Gui_Bottom.SetStageOrder();
    }
    void SetOrderType()
    {
        //int typeNum = (m_StageInfo.m_Num - 1) % 2; //===벨런스 레시피수에 따라 정함.
        //int count = ((int)((m_StageInfo.m_Num - 1) * 0.1f) % 3 + 1); //===벨런스  30판 5, 31->3... 4... 5
        //switch (typeNum)
        //{
        //    case 0:
        //        m_StageInfo.m_eMissionType = E_MissionType.type1;
        //        m_StageInfo.m_ArrMissionMonMaxCounter = new int[3] { count, count, count };
        //        m_StageInfo.m_ArrMissionMonKillCounter = new int[3] { 0, 0, 0 };
        //        m_StageInfo.m_ArrMissionMonId = new string[3] { "mon211", "mon201", "mon202" }; //===벨런스 관련 정리 필요.
        //        break;
        //    case 1:
        //        m_StageInfo.m_eMissionType = E_MissionType.type2;
        //        m_StageInfo.m_ArrMissionMonMaxCounter = new int[4] { count, count, count, count };
        //        m_StageInfo.m_ArrMissionMonKillCounter = new int[4] { 0, 0, 0, 0 };
        //        m_StageInfo.m_ArrMissionMonId = new string[4] { "mon203", "mon207", "mon206", "mon209" }; //===벨런스 관련 정리 필요. 토마토, 패드, 빵, 불고기 소스
        //        break;
        //}
    }

    public void Set_RoundStart()
    {
        EnemyManager.Instance.PlaySpawnStart();
        m_PlayerController.RoundStart();
    }

    void ClearMissionKillValue()
    {
        //for (int i = 0; i < m_StageInfo.m_ArrMissionMonKillCounter.Length; i++)
        //{
        //    m_StageInfo.m_ArrMissionMonKillCounter[i] = 0;
        //}
    }

}
