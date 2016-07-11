using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyManager : MonoBehaviour {

    #region Singleton

    private static EnemyManager _instance = null;

    public static EnemyManager Instance
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

    public GameObject m_EnemyBasePrefab;
    public GameObject m_EnemyMonsterPrefab;
    public GameObject m_EnemyNetPrefab;
    //public GameObject[] m_ArrMonsterPrefabs;
    public EnemySpawner m_EnemySpawner;
    //public GameObject m_pfKillEffect01;
    //public GameObject m_pfKillEffect_coin;
    //[HideInInspector]
    //public Vector3 m_HitEffectOffset = new Vector3(0, 1, 0);
    public void PlaySpawnReady()
    {
        m_EnemySpawner.gameObject.SetActive(false);
    }
    public void PlaySpawnStart()
    {
        m_EnemySpawner.gameObject.SetActive(true);
    }

    //public void PlayKill(string id, Vector3 pos_base)
    //{
    //    if(GameManager.Instance.m_isGameInputWaiting == true)
    //    {
    //        return;
    //    }

    //    StartCoroutine(_PlayKill( id, pos_base) );
    //}
    //IEnumerator _PlayKill(string id, Vector3 pos_base)
    //{
    //    if (_Gui_Bottom == null)
    //    {
    //        _Gui_Bottom = GuiManager.Instance.Find<Gui_Bottom>();
    //    }

    //    SpawnerPool.Spawn(m_pfKillEffect_coin, pos_base + m_HitEffectOffset, m_pfKillEffect_coin.transform.rotation);

    //    float fduration = 0.7f;
    //    float stop_effect_offset = 0.3f;
    //    int ix_update = GameManager.Instance.UpdateKillMissionMonster(id);
    //    if (ix_update >= 0)
    //    {
    //        Transform move_pos = _Gui_Bottom.Get_UIOrderSlotController().GetSlotPossion(ix_update);

    //        GameObject obj = GuiManager.Instance.SetSpawnGui_WorldPoint(GuiManager.ELayerType.Front, m_pfKillEffect01, pos_base + m_HitEffectOffset);
    //        obj.GetComponent<ParticleSystem>().Play();
    //        obj.transform.DOMove(new Vector3(move_pos.position.x, move_pos.position.y, obj.transform.position.z), fduration);

    //        yield return new WaitForSeconds(fduration + stop_effect_offset);
    //        GuiSpawnerPool.Destroy(obj);
    //    }


    //}

    public void RemoveAll()
    {
        ObjectCache cache = SpawnerPool.Get_ObjectCache(m_EnemyBasePrefab);
        for (int i = 0; i < cache.objects.Length; i++)
        {
            if (cache.objects[i].activeSelf == true)
            {
                EnemyController mon = cache.objects[i].GetComponent<EnemyController>();
                mon.Disappear();
            }
        }
    }
    public void Play_CheckClearEnemy()
    {
        if(CheckClearEnemy() == true)
        {
            // 다음 라운드
            //GameManager.Instance.PlayPause();
            //RemoveAll(); // 생략 해도 됨.
            //GameManager.Instance.PlayResume();

            GameManager.Instance.Play_ClearPopup();

        }
    }

    public bool CheckClearEnemy()
    {
        ObjectCache cache = SpawnerPool.Get_ObjectCache(m_EnemyBasePrefab);
        for (int i = 0; i < cache.objects.Length; i++)
        {
            if (cache.objects[i].activeSelf == true)
            {
                EnemyController mon = cache.objects[i].GetComponent<EnemyController>();
                if(mon.m_Info.m_Die == false)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void Stop_All()
    {
        ObjectCache cache = SpawnerPool.Get_ObjectCache(m_EnemyBasePrefab);
        for (int i = 0; i < cache.objects.Length; i++)
        {
            if (cache.objects[i].activeSelf == true)
            {
                EnemyController mon = cache.objects[i].GetComponent<EnemyController>();
                mon.Set_Stop();
            }
        }
    }
}
