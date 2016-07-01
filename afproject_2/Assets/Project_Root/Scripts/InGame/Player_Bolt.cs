using UnityEngine;
using System.Collections;

public class Player_Bolt : MonoBehaviour {
    public bool _AutoStart = true;
    public float _StartDelay = 0f;
    public float speed;
    public bool _IsPlayMove = false;

    Rigidbody2D _Rigidbody2D;

    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();

        //if (_isLocalMover == false)
        //{
        //    if (_isMulti == false)
        //    {
        //        Vector2 movement = new Vector2(1, 0);
        //        _Rigidbody2D.velocity = movement * speed;
        //    }
        //    else
        //    {
        //        Vector2 movement = new Vector2(_speedX, _speedY);
        //        _Rigidbody2D.velocity = movement;
        //    }
        //}
        //else
        //{

        //}

        if (_AutoStart == true)
        {
            PlayMove();
        }
    }
    public void PlayMove()
    {
        Invoke("SetPlayMove", _StartDelay);
    }
    void SetPlayMove()
    {
        _IsPlayMove = true;
    }

    void Update()
    {
        if (_IsPlayMove == true)
        {
            //Vector3 move_dir = new Vector3(1, 0, 0);
            //transform.localPosition = transform.localPosition + (move_dir * speed * Time.deltaTime);

            _Rigidbody2D.velocity = (new Vector2(0, 1) * speed);
        }
    }
}
