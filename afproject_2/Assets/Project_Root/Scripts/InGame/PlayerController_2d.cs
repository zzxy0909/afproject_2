using UnityEngine;
using System.Collections;

[System.Serializable]
public class wt_Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController_2d : MonoBehaviour
{
	public float m_speed = 1.5f;
	public wt_Boundary m_boundary;

    Rigidbody2D _Rigidbody2D;
    Transform _Trans;
    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Trans = this.transform;
    }
	void Update ()
	{
		
        if (Input.GetMouseButtonDown(0))
        {
            _isDrag = true;
            _oldMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isDrag = false;
        }
	}
    
    bool _isDrag = false;
    Vector2 _oldMousePos = Vector2.zero;

    public float moveHorizontal;
    public float moveVertical;
    float move_ratioX = 1; // (0.16f * 1.5f);
//    float move_ratioY = (0.16f * 1.5f);
    void FixedUpdate ()
	{
        if (_Rigidbody2D != null && _Trans != null)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            //moveVertical =  Input.GetAxis("Vertical");

            if (_isDrag == true)
            {
                
                moveHorizontal = (Input.mousePosition.x - _oldMousePos.x) * move_ratioX;
                //moveVertical = (Input.mousePosition.y - _oldMousePos.y) * move_ratioY;
                _oldMousePos = Input.mousePosition;
            }

            moveVertical = 0; // 좌우만 입력 처리.

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            _Rigidbody2D.velocity = movement * m_speed;

            _Trans.position = new Vector3
            (
                Mathf.Clamp(_Trans.position.x, m_boundary.xMin, m_boundary.xMax),
                Mathf.Clamp(_Trans.position.y, m_boundary.yMin, m_boundary.yMax),
                0.0f
            );
        }
	}
   
}
