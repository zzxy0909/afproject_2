using UnityEngine;
using System.Collections;
using DG.Tweening;

public class blinkCokor : MonoBehaviour {
    public bool m_isPlay = false;
    public Color[] m_Color = new Color[]{ Color.red, Color.white };
    public SpriteRenderer m_SpriteRenderer;
    public float m_durationTime = 1f;
	// Use this for initialization
	void Start () {
    }

    float _nextTime = 0f;
	// Update is called once per frame
	void Update () {

        if (Time.time > _nextTime
            && m_isPlay == true
            )
        {
            _nextTime = Time.time + m_durationTime;
            ToggleColor();
        }
    }

    int _ix = 0;
    void ToggleColor()
    {
        if(_ix == 0)
        {
            _ix = 1;
        }
        else
        {
            _ix = 0;
        }

        m_SpriteRenderer.color = m_Color[_ix];
    }
}
