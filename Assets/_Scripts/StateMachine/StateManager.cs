using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    public GameObject obj;
    public StateType type;
    public BaseState state;
    public RunState runState;
    public IdleState idleState;

    public StateManager(GameObject obj)
    {
        this.obj = obj;
        Init();
    }

    public void Init()
    {
        runState = new RunState(this);
        idleState = new IdleState(this);
        type = StateType.idle;
        state = idleState;
    }

    public void Update()
    {
        switch (type)
        {
            case StateType.idle:
                ((IdleState)state).Update();
                break;
            case StateType.run:
                ((RunState)state).Update();
                break;
        }
    }
}