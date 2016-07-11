using UnityEngine;
using System.Collections;

public class Movement_Obj : MonoBehaviour {
	
	public bool m_bGotoTarget = false;
    public Transform m_TargetPos;
    public Vector3 m_TargetVector;
    public float m_fStopDistance = 0.0f;
	public float m_fMovingSpeed = 4f;
    System.Action _OnMoveEndCall = null;
    Transform m_Trans;
    public void Set_StartMovement(Transform trans_target, System.Action call = null)
    {
        _OnMoveEndCall = call;
        m_TargetPos = trans_target;

        m_bGotoTarget = true;
    }
    public void Set_StartMovement(Vector3 vec_target, System.Action call )
    {
        _OnMoveEndCall = call;
        m_TargetPos = null;
        m_TargetVector = vec_target;

        m_bGotoTarget = true;
    }

    // Use this for initialization
    void Start () {
        m_Trans = transform;

    }
	
	float fCheckTime = 0.1f;
	float fUpdateTime = 0.0f;
	// Update is called once per frame
	void Update () {
	
		if(m_bGotoTarget == true)
		{
			fUpdateTime += Time.deltaTime;
			if(fCheckTime < fUpdateTime)
			{
				CheckMovingDistance();				
				fUpdateTime = 0.0f;
			}
			
            if(m_TargetPos == null)
            {
                MoveToTarget(m_TargetVector);
            }
            else
            {
                MoveToTarget(m_TargetPos.position);
            }
			
			
		}
	}
	
    public void OnFinish()
	{
		//-------
        if(_OnMoveEndCall != null)
        {
            _OnMoveEndCall();
        }

		m_bGotoTarget = false;
		

	}
	void CheckMovingDistance()
	{
        Vector3 vec_Check;
        if (m_TargetPos == null)
        {
            vec_Check = m_TargetVector;
        }
        else
        {
            vec_Check = m_TargetPos.position;
        }


        float targdis = Vector3.Distance(m_Trans.position, vec_Check);
		
		if(targdis > m_fStopDistance )
		{
						
		}else{
			OnFinish();
		}
		
	}
	void MoveToTarget(Vector3 posTarget)
	{
        if(posTarget.x < m_Trans.position.x)
        {
            m_Trans.localScale = new Vector3( Mathf.Abs( m_Trans.localScale.x ) * -1f, m_Trans.localScale.y, m_Trans.localScale.z);
        }else
        {
            m_Trans.localScale = new Vector3(Mathf.Abs(m_Trans.localScale.x) * 1f, m_Trans.localScale.y, m_Trans.localScale.z);

        }

        float targdis = Vector3.Distance(m_Trans.position, posTarget);
		if(targdis > m_fStopDistance )
		{
            // Vector3 move_dir = m_Trans.forward;	
            Vector3 move_dir = (posTarget - m_Trans.position).normalized;
            m_Trans.position += move_dir * m_fMovingSpeed * Time.deltaTime;

            //			m_Trans.position = Vector3.Lerp(m_Trans.position , posTarget , _fMovingSpeed * Time.deltaTime); //  Time.deltaTime*30.0f);
        }

    }
}
