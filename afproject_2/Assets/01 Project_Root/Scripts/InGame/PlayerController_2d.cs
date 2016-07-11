using UnityEngine;
using System.Collections;

[System.Serializable]
public class wt_Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController_2d : MonoBehaviour
{
    public GameObject m_pfPlayerBolt;
    public GameObject m_pfBumbEffect;
    public float m_boltTime = 1f;
    public bool m_isAutoShotAndDragFlag = true;

    public float m_speed = 1.5f;
	public wt_Boundary m_boundary;

    public int m_bombCount = 3;
    public float m_bombRange = 5f;

    Rigidbody2D _Rigidbody2D;
    public Transform m_Trans;
    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Trans = this.transform;
    }

    //============================================ This stores the finger that's currently dragging this GameObject
    private Lean.LeanFinger draggingFinger;
    protected virtual void OnEnable()
    {
        // Hook into the OnFingerDown event
        Lean.LeanTouch.OnFingerDown += OnFingerDown;

        // Hook into the OnFingerUp event
        Lean.LeanTouch.OnFingerUp += OnFingerUp;
    }

    protected virtual void OnDisable()
    {
        // Unhook the OnFingerDown event
        Lean.LeanTouch.OnFingerDown -= OnFingerDown;

        // Unhook the OnFingerUp event
        Lean.LeanTouch.OnFingerUp -= OnFingerUp;
    }

    protected void LateUpdate()
    {
        // If there is an active finger, move this GameObject based on it
        if (draggingFinger != null 
            && m_isAutoShotAndDragFlag == true)
        {
            Vector2 delta = new Vector2(draggingFinger.DeltaScreenPosition.x, 0);
            Lean.LeanTouch.MoveObject(m_Trans, delta); // draggingFinger.DeltaScreenPosition);

            m_Trans.position = new Vector3
            (
                Mathf.Clamp(m_Trans.position.x, m_boundary.xMin, m_boundary.xMax),
                Mathf.Clamp(m_Trans.position.y, m_boundary.yMin, m_boundary.yMax),
                0.0f
            );
        }
    }

    public void OnFingerDown(Lean.LeanFinger finger)
    {
        draggingFinger = finger;
    }

    public void OnFingerUp(Lean.LeanFinger finger)
    {
        // Was the current finger lifted from the screen?
        if (finger == draggingFinger)
        {
            // Unset the current finger
            draggingFinger = null;
        }
    }

    public void RoundInit()
    {
        m_isAutoShotAndDragFlag = false;

    }
    public void RoundStart()
    {
        m_isAutoShotAndDragFlag = true;

    }

    void PlayShot()
    {
        SpawnerPool.Spawn(m_pfPlayerBolt, m_Trans.position, m_Trans.rotation);

    }

    float _nextFire = 0;
    void Update ()
	{
        if (Time.time > _nextFire
            && m_isAutoShotAndDragFlag == true
            )
        {
            _nextFire = Time.time + m_boltTime;
            PlayShot();
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    _isDrag = true;
        //    _oldMousePos = Input.mousePosition;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    _isDrag = false;
        //}
	}

    //    bool _isDrag = false;
    //    Vector2 _oldMousePos = Vector2.zero;
    //    public float moveHorizontal;
    //    public float moveVertical;
    //    float move_ratioX = 1; // (0.16f * 1.5f);
    //    float move_ratioY = (0.16f * 1.5f);
    //   void FixedUpdate ()
    //{
    //       if (_Rigidbody2D != null && _Trans != null)
    //       {
    //           moveHorizontal = Input.GetAxis("Horizontal");
    //           //moveVertical =  Input.GetAxis("Vertical");

    //           if (_isDrag == true)
    //           {

    //               moveHorizontal = (Input.mousePosition.x - _oldMousePos.x) * move_ratioX;
    //               //moveVertical = (Input.mousePosition.y - _oldMousePos.y) * move_ratioY;
    //               _oldMousePos = Input.mousePosition;
    //           }

    //           moveVertical = 0; // 좌우만 입력 처리.

    //           Vector2 movement = new Vector2(moveHorizontal, moveVertical);

    //           _Rigidbody2D.velocity = movement * m_speed;

    //           _Trans.position = new Vector3
    //           (
    //               Mathf.Clamp(_Trans.position.x, m_boundary.xMin, m_boundary.xMax),
    //               Mathf.Clamp(_Trans.position.y, m_boundary.yMin, m_boundary.yMax),
    //               0.0f
    //           );
    //       }
    //}

    bool _is_Play_Bomb = false;
    public void Play_Bomb()
    {
        if(_is_Play_Bomb == false)
        {
            _is_Play_Bomb = true;
            StartCoroutine(_Play_Bomb());
        }
        
    }
    IEnumerator _Play_Bomb()
    {
        SpawnerPool.Spawn(m_pfBumbEffect, m_Trans.position, Quaternion.identity);

        Collider2D[] col = Physics2D.OverlapCircleAll(m_Trans.position, m_bombRange);
        float loopTime = 0.05f / col.Length;
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].CompareTag("Enemy_Base"))
            {
                ContactObject_Enemy cont = col[i].GetComponent<ContactObject_Enemy>();
                cont._EnemyController.Damage_Calc(999);
                yield return new WaitForSeconds(loopTime);
            }
        }

        _is_Play_Bomb = false;
    }
}
