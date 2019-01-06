using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class BaseState
{
    protected StateManager mgr;

    public virtual void EnterState(StateType state)
    {
        switch (state)
        {
            case StateType.idle:
                mgr.type = StateType.idle;
                mgr.state = mgr.idleState;
                if (ItemManager.GetInst().equippedData[0] == null || ItemManager.GetInst().equippedData[0].weaponType == WeaponType.melee)
                    mgr.obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "jian_idle", true);
                else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.magic)
                    mgr.obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "fazhang_idle", true);
                else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.range)
                    mgr.obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "gongjian_idle", true);
                break;
            case StateType.run:
                mgr.type = StateType.run;
                mgr.state = mgr.runState;
                if (ItemManager.GetInst().equippedData[0] == null || ItemManager.GetInst().equippedData[0].weaponType == WeaponType.melee)
                    mgr.obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "jian_run", true);
                else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.magic)
                    mgr.obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "fazhang_run", true);
                else if (ItemManager.GetInst().equippedData[0].weaponType == WeaponType.range)
                    mgr.obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "gongjian_run", true);
                break;
        }
    }

    public virtual void Update()
    {

    }
}