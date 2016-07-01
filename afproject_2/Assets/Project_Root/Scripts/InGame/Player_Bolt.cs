using UnityEngine;
using System.Collections;

public class Player_Bolt : MonoBehaviour {
    public bool _AutoStart = true;
    public float _StartDelay = 0f;
    public float speed;
    public bool _IsPlayMove = false;

    Rigidbody2D _Rigidbody2D;

    void OnEnable()
    {
        if(_Rigidbody2D == null)
            _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Rigidbody2D.velocity = Vector2.zero;
        _IsPlayMove = false;

        if (_AutoStart == true)
        {
            PlayMove();
        }
    }
    //void OnDestory()
    //{
    //    _IsPlayMove = false;
    //}
    public void PlayMove()
    {
        Invoke("SetPlayMove", _StartDelay);
    }
    void SetPlayMove()
    {
        _IsPlayMove = true;
        _Rigidbody2D.velocity = (new Vector2(0, 1) * speed);
    }

    //void Update()
    //{
    //    if (_IsPlayMove == true)
    //    {
    //        //Vector3 move_dir = new Vector3(1, 0, 0);
    //        //transform.localPosition = transform.localPosition + (move_dir * speed * Time.deltaTime);
            
    //    }
    //}
}
