using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

#if UNITY_EDITOR
// UNITY_EDITOR 런타임시 OnEnable이 두번 호출 됨.
[ExecuteInEditMode]
#endif
public class GuiBase : MonoBehaviour
{
    protected JSONNode							_params = null;

    //    public Dictionary<string, Transform> _usedTransList = new Dictionary<string, Transform>();
    public GuiManager.ELayerType m_eLayerType = GuiManager.ELayerType.Back;
    public List<string> _usedTransListKey = new List<string>();
    public List<Transform> _usedTransListObject = new List<Transform>();

#if UNITY_EDITOR
    public bool _isInitTransList = true;
    void OnEnable_Editor()
    {
        if (_isInitTransList == false) return;
        _isInitTransList = false;
        SetInspactor_InitTransList();
    }
#endif
	public void Set_InitTransListKey(string strkey, EventDelegate.Callback callback)
	{
		// key - object 쌍으로 설정.
		_usedTransListKey.Add(strkey); _usedTransListObject.Add(this.transform.FindChild(strkey));
		Get_usedTrans(strkey).GetComponent<UIButton>().onClick.Clear();
		EventDelegate.Add(Get_usedTrans(strkey).GetComponent<UIButton>().onClick, callback);
	}
    public void Set_InitTransListKey(string strkey)
    {
        // key - object 쌍으로 설정.
        _usedTransListKey.Add(strkey); _usedTransListObject.Add(this.transform.FindChild(strkey));
    }

    public Transform Get_usedTrans(string str)
    {
        for (int i = 0; i < _usedTransListKey.Count; i++)
        {
            if (_usedTransListKey[i] == str)
            {
                return _usedTransListObject[i];
            }
        }
        return null;
    }

    public virtual void SetInspactor_InitTransList()
    {

    }
    //=====================================================

    protected void OnEnable()
    {
#if UNITY_EDITOR
        OnEnable_Editor();
#endif
        _OnEnable();
    }

    public virtual void _OnEnable()
    {

    }

    public void SetParameter(JSONNode pParams)
    {
        _params = pParams;
    }
    
    public void Show(bool b)
    {
        GuiManager.Instance.Show_Name<GuiBase>(this.GetType().ToString(), m_eLayerType, b, _params);
    }
}
