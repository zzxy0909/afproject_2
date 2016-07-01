using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ContactObject_Enemy : MonoBehaviour {

    // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }

    void SetDelayRemove(GameObject obj)
    {
        _lstTriggerEnter_Obj.Add(obj);
        StartCoroutine(DelayAction(1f, () =>
        {
            _lstTriggerEnter_Obj.Remove(obj);
        }));
    }
    List<GameObject> _lstTriggerEnter_Obj = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D other)
    {
        if (
             _lstTriggerEnter_Obj.Contains(other.gameObject) == true
             || other.CompareTag("Untagged")
            // || (other.CompareTag() == "MeleeCheck")  필요없는 테그일 경우 바로 빠진다.
            )
        {
            return;
        }


        Collider2D tmp = other;
        SetDelayRemove(other.gameObject);

        if (other.CompareTag("Player_Bolt"))
        {
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~ Player_Bolt!!!");
        }
    }

}

