using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogControl : MonoBehaviour
{
    public static DialogControl instance;

    [HideInInspector]
    public Transform last = null;

    [HideInInspector]
    public List<Transform> triggerList = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DiaTrigger")
        {
            triggerList.Add(collision.transform);
            collision.transform.Find("Hint").gameObject.SetActive(true);
        }
    }

    private void FindNearestNpc()
    {
        float dis;

        float minDis = Mathf.Infinity;

        last = null;

        foreach (Transform k in triggerList)
        {
            dis = Vector2.Distance(k.position, transform.position);
            if (last == null)
            {
                last = k;
            }
            if (dis < minDis)
            {
                last = k;
                minDis = dis;
            }
        }
        foreach (Transform k in triggerList)
        {
            if (k == last && !last.Find("Hint").gameObject.activeSelf)
            {

                k.Find("Hint").gameObject.SetActive(true);
            }
            else if (k == last && last.Find("Hint").gameObject.activeSelf)
            {
                continue;
            }
            else
            {
                k.Find("Hint").gameObject.SetActive(false);
            }
        }

        if (last != null && last.GetComponentInParent<Patrol>() == null)
            last = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DiaTrigger")
        {
            triggerList.Remove(collision.transform);
            collision.transform.Find("Hint").gameObject.SetActive(false);
        }
    }

    public int GetDialogNpcId()
    {
        return last.GetComponentInParent<Patrol>().id;
    }

    private void Update()
    {
        for (int i = triggerList.Count - 1; i >= 0; --i)
            if (triggerList[i] == null)
                triggerList.RemoveAt(i);

        FindNearestNpc();
    }
}
