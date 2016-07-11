using UnityEngine;
using System.Collections;


public class Gui_FailPopup : GuiBase
{
    public UILabel m_lbScore;
    public UILabel m_lbTopScore;
    public UILabel m_lbRoundNum;

    string _score_format = "{0:###,###,###,##0}";
    string _Round_format = "{0:#,##0}";
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
            if(EnemyManager.Instance != null && GameManager.Instance != null)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator _PlayReady()
    {
        yield return StartCoroutine(_CheckManager());

        UpdateText();
    }

    public void ClickRetry()
    {
        this.Show(false);
        GameManager.Instance.PlayResume();
        EnemyManager.Instance.RemoveAll();

        GameManager.Instance.SetRound_Init_StartPopup();
    }
    public void UpdateText()
    {
        m_lbScore.text = string.Format(_score_format, GameManager.Instance.m_Score);
        m_lbTopScore.text = string.Format(_score_format, GameManager.Instance.m_TopScore);
        m_lbRoundNum.text = string.Format(_Round_format, GameManager.Instance.m_CurrentRoundNum);
    }
}
