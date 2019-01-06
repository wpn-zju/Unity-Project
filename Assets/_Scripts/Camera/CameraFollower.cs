using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollower : MonoBehaviour {

    public float lerpWeight;
    public float ShakeTime;
    public float Radio = 1000000.0f;
    private Vector3 mCurPos;
    private float DeFactor = 2;

    public void QuickLookAtPlayer()
    {
        var playerPosition = MyPlayer.instance.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.y / 1000);
    }

    void SyncPositionWithPlayer()
    {
        var currentPosition = (Vector2)transform.position;
        var playerPosition = (Vector2)MyPlayer.instance.transform.position;
        var aimPosition = Vector2.Lerp(currentPosition, playerPosition, lerpWeight);
        transform.position = new Vector3(aimPosition.x, aimPosition.y, transform.position.z);
        mCurPos = transform.position;
    }

    void SetZIndex()
    {
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.y / 1000);
    }

    void FixedUpdate()
    {
        if (MyPlayer.instance != null && !MyPlayer.instance.cameraLock)
        {
            SyncPositionWithPlayer();

            ShakeCamera_Time();
        }
    }

    void ShakeCamera_Time()
    {
        if (ShakeTime > 0)
        {
            transform.position = mCurPos + Random.insideUnitSphere * Radio;
            ShakeTime -= Time.deltaTime * DeFactor;
        }
        else
        {
            ShakeTime = 0;
        }
    }
}
