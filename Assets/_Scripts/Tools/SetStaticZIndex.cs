using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetStaticZIndex : MonoBehaviour {
    void ChangeZIndex()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.y/1000);
    }

    private void Awake()
    {
#if UNITY_EDITOR
        runInEditMode = true;
#endif
        ChangeZIndex();
    }

    private void Update()
    {
        ChangeZIndex();
    }
}