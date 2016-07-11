using UnityEngine;
using System.Collections;

public partial class GameManager : MonoBehaviour {

    public long m_Score;
    public long m_TopScore;

    public void AddScore(int n)
    {
        m_Score += n;
        if(m_TopScore <= m_Score)
        {
            m_TopScore = m_Score;
        }

        _Gui_TopFrame.UpdateText();
    }

    public void Play_Bomb()
    {
        m_PlayerController.Play_Bomb();
        
    }
}
