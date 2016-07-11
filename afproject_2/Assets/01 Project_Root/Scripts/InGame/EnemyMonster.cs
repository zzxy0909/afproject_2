using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyMonster : MonoBehaviour {
    public Movement_Obj m_Movement;
    public int m_AddScore = 20;

	// Use this for initialization
	void Start () {
        if(m_Movement == null)
        {
            m_Movement = GetComponent<Movement_Obj>();
        }
	
	}
    float _speed = 2f;
    float _yOffset = -1f;
    public void Play_Target()
    {
        Vector3 pos = GameManager.Instance.m_PlayerController.m_Trans.position + new Vector3(0f, _yOffset, 0f);
        //if (m_Movement == null)
        //{
        //    m_Movement = GetComponent<Movement_Obj>();
        //}
        //m_Movement.Set_StartMovement(pos, () =>
        //    {
        //        SpawnerPool.Destroy(this.gameObject);
        //    });
        this.transform.DOKill();
        this.transform.DOMove(pos, _speed).SetSpeedBased().OnComplete(() =>
        {
            SpawnerPool.Destroy(this.gameObject);
        });


        StopAllCoroutines();
        StartCoroutine(_Play_Target());
    }
    float _CheckTime_Player = 1f;
    IEnumerator _Play_Target()
    {
        while(true)
        {
            yield return new WaitForSeconds(_CheckTime_Player);
            Vector3 pos = GameManager.Instance.m_PlayerController.m_Trans.position + new Vector3(0f, _yOffset, 0f); 
            // m_Movement.m_TargetVector = pos;
            this.transform.DOKill();
            this.transform.DOMove(pos, _speed).SetSpeedBased().OnComplete(() =>
            {
                SpawnerPool.Destroy(this.gameObject);
            });
        }
    }
    
    public void Stop()
    {
        // m_Movement.m_bGotoTarget = false;

        this.transform.DOKill();
    }

    public void Kill()
    {
        Stop();
        GameManager.Instance.AddScore(m_AddScore);

        SpawnerPool.Destroy(this.gameObject);
    }
}
