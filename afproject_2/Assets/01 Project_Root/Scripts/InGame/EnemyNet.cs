using UnityEngine;
using System.Collections;

public class EnemyNet : MonoBehaviour {
    GameObject m_pfEnemyNet;
    public Transform m_EndTrans;
    public float m_NextNodeTime = 2f;
    public Rect m_Boundary;
    public float m_minRot = -30f;
    public float m_maxRot = 30f;

    public GameObject m_objNextNet;

    void OnEnable()
    {
        if (m_pfEnemyNet == null)
            m_pfEnemyNet = EnemyManager.Instance.m_EnemyNetPrefab;
        DelaySpawnNet();
    }
    void DelaySpawnNet()
    {
        StopAllCoroutines();

        StartCoroutine(_DelaySpawnNet());
    }
    IEnumerator _DelaySpawnNet()
    {
        float x = Random.Range(m_Boundary.xMin, m_Boundary.xMax);
        float y = Random.Range(m_Boundary.yMin, m_Boundary.yMax);
        float r = Random.Range(m_minRot, m_maxRot);

        Debug.Log("~~~~~~~~~~~~~~~~~~~ m_Boundary.xMin, m_Boundary.xMax :" + m_Boundary.xMin + ", " + m_Boundary.xMax);
        
        m_EndTrans.localPosition = new Vector3(x, y, 0f);
        m_EndTrans.localRotation = Quaternion.Euler(0,0,r);

        yield return new WaitForSeconds(m_NextNodeTime);

        m_objNextNet = SpawnerPool.Spawn(m_pfEnemyNet, m_EndTrans.position, m_EndTrans.rotation);
    }
    // Use this for initialization
    void Start () {
        if (m_pfEnemyNet == null)
            m_pfEnemyNet = EnemyManager.Instance.m_EnemyNetPrefab;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
