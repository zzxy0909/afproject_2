using UnityEngine;
using System.Collections;


public class Gui_TopFrame : GuiBase
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

	}
    
    public void ClickOptionMenu()
    {
        // test
        Debug.Log("~~~~~~~~~~~~~~~~~ ClickOptionMenu");

        // GuiManager.Instance.Show<Gui_PauseMenu>(GuiManager.ELayerType.Front, true);

    }
    public void ClickBomb()
    {
        // test
        Debug.Log("~~~~~~~~~~~~~~~~~ ClickBomb");

        // GuiManager.Instance.Show<Gui_PauseMenu>(GuiManager.ELayerType.Front, true);

        GameManager.Instance.Play_Bomb();
    }

    public void UpdateText()
    {
        m_lbScore.text = string.Format(_score_format, GameManager.Instance.m_Score);
        m_lbTopScore.text = string.Format(_score_format, GameManager.Instance.m_TopScore);
        m_lbRoundNum.text = string.Format(_Round_format, GameManager.Instance.m_CurrentRoundNum);
    }
}
