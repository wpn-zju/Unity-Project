using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Patrol : MonoBehaviour
{
    public SceneCharacterType type;
    public int id;
    public NpcState state;
    public List<string> animations = new List<string>();
    public List<Vector2> point = new List<Vector2>();
    public List<float> movingSpeed = new List<float>();
    public List<float> waitingTime = new List<float>();

    private List<Vector2> point2 = new List<Vector2>();
    private List<float> moveTime = new List<float>();

    private int count;
    private Vector2 spawnPoint;

    private void Start()
    {
        if (type == SceneCharacterType.npc)
            return;

        count = 0;
        spawnPoint = transform.position;

        for (int i = 0; i < point.Count; ++i)
        {
            moveTime.Add(Vector2.Distance(point[i], Vector2.zero) / movingSpeed[i]);
            point2.Add(spawnPoint + point[i]);
        }

        if (point.Count == 0)
            GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, animations[0], true);
        else if ((point.Count == animations.Count) && (movingSpeed.Count == waitingTime.Count) && (point.Count == movingSpeed.Count))
            StartCoroutine(Move(count));
    }

    private IEnumerator Move(int control)
    {
        if (control == point.Count)
            control = 0;

        GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, animations[control], true);

        while ((Vector2)transform.position != point2[control])
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, point2[control], movingSpeed[control]);
            if (newPos.x - transform.position.x >= 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
            transform.position = newPos;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(waitingTime[control]);

        control++;

        StartCoroutine(Move(control));
    }
}