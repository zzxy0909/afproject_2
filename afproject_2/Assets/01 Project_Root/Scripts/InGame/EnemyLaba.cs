using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyLaba : MonoBehaviour {
    public float m_StartLoopTime = 1.5f;
    public float m_LoopOffset = 0.5f;
    public float m_LoopTime;
    public float m_MoveOffset = 0.5f;
    public float m_MoveTime = 0.5f;
    public int m_SpawnStep = 10;
    public float m_CheckBase_Radius = 1f;
    public bool m_isPlayLoop = false;
    int _CurrentStep = 0;

    float _MaxLeft, _MaxRight;

    Transform _Trans;
	// Use this for initialization
	void Start () {
        _Trans = transform;

    }
    
    public void Reset_Play()
    {
        if(_Trans == null)
            _Trans = transform;
        //StopAllCoroutines();
        //StartCoroutine(_Reset_Play());
        Reset();
        Play();
    }
    public void Reset()
    {
        if (_Trans == null)
            _Trans = transform;
        //StopAllCoroutines();
        //StartCoroutine(_Reset());

        Init_Reset();
    }
    //IEnumerator _Reset()
    //{
    //    m_isPlay = false;
    //    yield return StartCoroutine(CheckManager());
    //    Init_Reset();
    //}
    //IEnumerator _Reset_Play()
    //{
    //    yield return StartCoroutine(_Reset());

    //    Play();
    //}

    public void Stop()
    {
        m_isPlayLoop = false;
    }
    public void Resume()
    {
        m_isPlayLoop = true;
    }
    public void Play()
    {
        m_isPlayLoop = true;
    }

    float _nextTime = 0;
    void Update()
    {
        if(m_isPlayLoop == true)
        {
            float currtime = Time.time;
            if (_nextTime <= currtime)
            {
                SetLoopTime();
                _nextTime = currtime + m_LoopTime;

                SwitchMove();
                _CurrentStep++;
                CheckSpawnStep();
            }
        }
    }

    GameObject SpawnEnemyMonster()
    {
        //ObjectCache cache = SpawnerPool.Get_ObjectCache(EnemyManager.Instance.m_EnemyMonsterPrefab);
        //if(cache.GetNextObjectInCache() == null)

        GameObject obj = SpawnerPool.Spawn(EnemyManager.Instance.m_EnemyMonsterPrefab, _Trans.position, Quaternion.identity);
        if(obj != null)
        {
            EnemyMonster mon = obj.GetComponent<EnemyMonster>();
            mon.Play_Target();
        }
        return obj;
    }
    GameObject SpawnEnemyBase()
    {
        GameObject obj = SpawnerPool.Spawn(EnemyManager.Instance.m_EnemyBasePrefab, _Trans.position, Quaternion.identity);
        EnemyController mon = obj.GetComponent<EnemyController>();
        mon.PlayInit();
        return obj;
    }

    void CheckSpawnStep()
    {
        if(_CurrentStep >= m_SpawnStep)
        {
            if(CheckBase() == false)
            {
                int r = Random.Range(0, 11); // 10% 로 몬스터 출연
                if(r == 0)
                {
                    GameObject obj = SpawnEnemyMonster();
                    if(obj == null)
                    {
                        SpawnEnemyBase();
                    }
                }
                else
                {
                    SpawnEnemyBase();
                }
                this.gameObject.SetActive(false);
            }else
            {
                // _CurrentStep = 0;
                GameObject obj = SpawnEnemyMonster();
                if (obj == null)
                {
                    _CurrentStep = 0;
                }else
                {
                    this.gameObject.SetActive(false);
                }
            }
            
        }
    }
    bool CheckBase()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(_Trans.position, m_CheckBase_Radius);
        for (int i = 0; i < col.Length; i++)
        {
            if(col[i].CompareTag("Enemy_Base"))
            {
                return true;
            }
        }
        return false;
    }
    public void Set_SpawnStep(int n)
    {
        m_SpawnStep = n;
    }
	void Init_Reset()
    {
        m_isPlayLoop = false;

        _Trans.localPosition = Vector3.zero;
        _CurrentStep = 0;

        SetLoopTime();

        _MaxLeft = EnemyManager.Instance.m_EnemySpawner.m_Boundary.xMin;
        _MaxRight = EnemyManager.Instance.m_EnemySpawner.m_Boundary.xMax;
    }
    void SetLoopTime()
    {
        m_LoopTime = Random.Range(m_StartLoopTime, m_StartLoopTime + m_LoopOffset);
    }

    IEnumerator CheckManager()
    {
        while(true)
        {
            if(EnemyManager.Instance != null)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    
	void SwitchMove()
    {
        int r = Random.Range(0, 3);
        switch(r)
        {
            case 0:
                MoveLeft();
                break;
            case 1:
                MoveRight();
                break;
            case 2:
                MoveDown();
                break;
            default:
                MoveDown();
                break;

        }
    }

    void MoveLeft()
    {
        if(_MaxLeft >= _Trans.position.x)
        {
            int r = Random.Range(0, 2);
            if(r == 0)
            {
                MoveRight();
            }else
            {
                MoveDown();
            }
            return;
        }
        _Trans.DOKill();
        _Trans.DOMoveX(_Trans.position.x - m_MoveOffset, m_MoveTime);
    }
    void MoveRight()
    {
        if (_MaxRight <= _Trans.position.x)
        {
            int r = Random.Range(0, 2);
            if (r == 0)
            {
                MoveLeft();
            }
            else
            {
                MoveDown();
            }
            return;
        }

        _Trans.DOKill();
        _Trans.DOMoveX(_Trans.position.x + m_MoveOffset, m_MoveTime);
    }
    void MoveDown()
    {
        _Trans.DOKill();
        _Trans.DOMoveY(_Trans.position.y - m_MoveOffset, m_MoveTime);
    }
}
