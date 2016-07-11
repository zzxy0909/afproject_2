using UnityEngine;
using System.Collections;


public class Gui_StartPopup : GuiBase
{
    public UILabel m_lbRound;
//    public GameObject[] m_ArrMissionType; 

    public override void SetInspactor_InitTransList()
    {
    }
    
	public override void _OnEnable()
	{
        StartCoroutine(_PlayReady());
    }

    IEnumerator _CheckManager()
    {
        while(true)
        {
            if(EnemyManager.Instance != null
                && GameManager.Instance != null
                && GameManager.Instance.m_isReady == true)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator _PlayReady()
    {
        yield return StartCoroutine(_CheckManager());

//        ShowMissionType((int)GameManager.Instance.m_StageInfo.m_eMissionType);
        m_lbRound.text = GameManager.Instance.m_CurrentRoundNum.ToString();

        EnemyManager.Instance.PlaySpawnReady();

    }

    public void StartGame()
    {
//        Debug.LogWarning("~~~~~~~~~~~~~~~~~ StartGame");
        GuiManager.Instance.Show<Gui_StartPopup>(GuiManager.ELayerType.Front, false);
        GameManager.Instance.StartGame();
    }
    
}
