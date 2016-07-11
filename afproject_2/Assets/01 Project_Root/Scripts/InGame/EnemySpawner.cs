using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject m_pfEnemyBase = null;
    public float m_loopDelay = 5f;
    public float m_StartDelay = 0f;
    public int m_DefaultStartCount = 3;
    public Rect m_Boundary;  // 1 단위로 렌덤 그리드 스폰 기준. x -3, 3, w:6    y 6, -1  H:7
    public bool m_isPlaying = false;

    Transform m_trans;
	// Use this for initialization
	void Start () {
        m_trans = this.transform;
        if (m_pfEnemyBase == null)
        {
            m_pfEnemyBase = EnemyManager.Instance.m_EnemyBasePrefab;
        }
    }
    void OnEnable()
    {
        if (m_pfEnemyBase == null)
        {
            m_pfEnemyBase = EnemyManager.Instance.m_EnemyBasePrefab;
        }
        StartSpawn();
    }
    public void StartSpawn()
    {
        m_DefaultStartCount = 2 + GameManager.Instance.m_CurrentRoundNum; //==== 벨런스항목

        StopAllCoroutines();
        StartCoroutine(_StartSpawn());
    }
    IEnumerator _StartSpawn()
    {
        yield return new WaitForSeconds(m_StartDelay);
        //       m_Count = m_DefaultCount;
        for (int i = 0; i < m_DefaultStartCount; i++)
        {
            PlaySpawn();
        }

        // yield return StartCoroutine(_LoopSpawn());

        Set_NextTime();
        PlayLoop();
    }

    void PlayLoop()
    {
        m_isPlaying = true;
    }

    void Set_NextTime()
    {
        _nextTime = Time.time + m_loopDelay;
    }

    float _nextTime = 0;
    void Update()
    {
        if(m_isPlaying == true)
        {
            if(Time.time >= _nextTime)
            {
                Set_NextTime();
                PlaySpawn();
            }
        }
    }
    public void Stop()
    {
        m_isPlaying = false;
    }
    
    IEnumerator _LoopSpawn()
    {   
        while (true)
        {
            yield return new WaitForSeconds(m_loopDelay);
            PlaySpawn();
        }
    }

    void PlaySpawn()
    {
        GameObject select_prefab = m_pfEnemyBase;
        if (select_prefab != null)
        {
            GameObject obj = SpawnerPool.Spawn(select_prefab, Get_SpawnPosition(), m_trans.rotation);
            EnemyController mon = obj.GetComponent<EnemyController>();
            mon.PlayInit();
        }
    }

    Vector3 Get_SpawnPosition()
    {
        float rx = Random.Range(m_Boundary.xMin, m_Boundary.xMax);
        float ry = Random.Range(m_Boundary.yMin, m_Boundary.yMax);

        return new Vector3(rx, ry, 0);
    }

}
