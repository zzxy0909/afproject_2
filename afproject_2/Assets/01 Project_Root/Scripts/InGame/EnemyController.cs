using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyController : MonoBehaviour {

    //==== info
//    [HideInInspector]
    public EnemyInfo m_Info;
    public EnemyLaba[] m_arrLaba;
    public int m_LabaSpawnStep = 5;

    public Transform m_backRoot;

    Transform _Trans;

    public void PlayInit()
    {
        m_Info.m_StartHP = 1; // (int)(((stageNum * stageNum) - 1) / 2) + 6;  //===벨런스 관련 정리 필요. = INT((C7 - 1) / 2) + 6 .......  =INT(((C7*C7)-1)/2) +6
        m_Info.m_DropId = 1; // 1: 10점,  //===벨런스 관련 정리 필요. =50 +((C7-1)*4)*30  <===== = 10 + (C7 * C7) * 30 - 30
        m_Info.m_Die = false;
        m_Info.m_HP = m_Info.m_StartHP;

        Set_ActiveAllLava();

        PlayBackImage();
    }
    float _backScale = 0.5f;
    float _backScaleOffset = 1f;
    float _backscale_dur = 0.3f;
    float _backscale_durOffset = 0.5f;
    void PlayBackImage()
    {
        m_backRoot.DOKill();

        float rScale = Random.Range(_backScale, _backScale + _backScaleOffset);
        float rscale_dur = Random.Range(_backscale_dur, _backscale_dur + _backscale_durOffset);

        m_backRoot.localScale = new Vector3(rScale, rScale, rScale);

        m_backRoot.DOScaleY(rScale + (rScale*0.1f), rscale_dur).SetLoops(-1, LoopType.Yoyo);

    }
    void Set_ActiveAllLava()
    {
        for(int i=0; i<m_arrLaba.Length; i++)
        {
            m_arrLaba[i].gameObject.SetActive(true);  // 증식후 false 된것.
            m_arrLaba[i].Set_SpawnStep(m_LabaSpawnStep);
            m_arrLaba[i].Reset_Play();
        }
    }
    public void Disappear()
    {
        // 사라지는 이팩트 추가 필요..
        SpawnerPool.Destroy(this.gameObject);
        m_Info.m_Die = true;
    }
    public void Set_Stop()
    {
        for (int i = 0; i < m_arrLaba.Length; i++)
        {
            m_arrLaba[i].Stop();
        }
    }

    public void Damage_Calc(int n)
    {
        m_Info.m_HP -= n;
        if (m_Info.m_HP <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
//        StopMove();
        // 죽는 이팩트 필요.
//        MonsterManager.Instance.PlayKill(m_Info.m_id, m_Trans.position);
        // StartCoroutine(_PlayKill());

        m_Info.m_HP = 0;
        m_Info.m_Die = true;

        GameManager.Instance.AddScore(Get_KillScore());

        SpawnerPool.Destroy(this.gameObject);

        EnemyManager.Instance.Play_CheckClearEnemy();
    }
    int Get_KillScore()
    {
        int rtn = 0;
        switch(m_Info.m_DropId)
        {
            case 1:
                rtn = 10;
                break;
        }
        return rtn;
    }


    // 스폰된 지역이 데드라인 지났다면 끝.
    IEnumerator CheckGameOver()
    {
        yield return StartCoroutine(CheckManager());

        if(_Trans.position.y <= GameManager.Instance.m_PlayerController.m_Trans.position.y )
        {
            // Set_Stop();

            GameManager.Instance.PlayGameOverPopup();
        }
    }
    IEnumerator CheckManager()
    {
        while (true)
        {
            if (EnemyManager.Instance != null
                && GameManager.Instance != null)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void OnEnable()
    {
        if (_Trans == null)
            _Trans = transform;
        StopAllCoroutines();

        StartCoroutine ( CheckGameOver() );
    }
    // Use this for initialization
    void Start () {
	
	}
	

}
