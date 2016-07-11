using UnityEngine;
using System.Collections;

public class DelayDestoryToPool : MonoBehaviour {
    public ParticleSystem m_ParticleSystem;
    public float m_DelayTime = 0.5f;
	// Use this for initialization
	void Start () {
	    
	}

    void OnEnable()
    {
        if(m_ParticleSystem != null)
        {
            StopAllCoroutines();
            m_ParticleSystem.Stop();
            m_ParticleSystem.Play();
        }

        StartCoroutine(_DelayDestroy());
    }

	IEnumerator _DelayDestroy()
    {
        yield return new WaitForSeconds(m_DelayTime);

        SpawnerPool.Destroy(this.gameObject);
    }
}
