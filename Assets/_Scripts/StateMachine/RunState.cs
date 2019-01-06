using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class RunState : BaseState
{
    public RunState(StateManager mgr)
    {
        this.mgr = mgr;
    }

    public override void EnterState(StateType state)
    {
        base.EnterState(state);
    }

    public override void Update()
    {
        base.Update();
        if (Vector2.Distance(Vector2.zero, mgr.obj.GetComponent<Rigidbody2D>().velocity) < 0.1f)
        {
            EnterState(StateType.idle);
        }
    }
}