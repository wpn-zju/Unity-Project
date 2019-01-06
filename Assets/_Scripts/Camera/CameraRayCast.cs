using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Spine;
using Spine.Unity;

public class CameraRayCast : MonoBehaviour
{
    public static CameraRayCast instance;

    private RaycastHit2D ObjHit;
    private GameObject last;
    private GameObject current;
    private bool toself = false;
    private bool choosing = false;

    private void Awake()
    {
        instance = this;
    }

    private void SetIdle(GameObject obj)
    {
        CharacterType type = Battle.instance.Obj2Char(obj).charType;

        switch(type)
        {
            case CharacterType.NormalMonster:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "idle", true);
                break;
            case CharacterType.EliteMonster:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "idle", true);
                break;
            case CharacterType.Boss:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "idle", true);
                break;
            case CharacterType.Player:
                CommonHandle.PlayerSetIdle(obj);
                break;
            case CharacterType.Teammate:
                CommonHandle.PlayerSetIdle(obj);
                break;
            case CharacterType.Summon:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "idle", true);
                break;
        }
    }

    private void SetEnable(GameObject obj)
    {
        CharacterType type = Battle.instance.Obj2Char(obj).charType;

        switch (type)
        {
            case CharacterType.NormalMonster:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "hurt", false);
                break;
            case CharacterType.EliteMonster:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "hurt", false);
                break;
            case CharacterType.Boss:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "hurt", false);
                break;
            case CharacterType.Player:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "ability01", false);
                break;
            case CharacterType.Teammate:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "ability01", false);
                break;
            case CharacterType.Summon:
                obj.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "attack", false);
                break;
        }
    }

    private void Update()
    {
        if(Battle.instance.inBattle)
        {
            ObjHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 30.0f, (1 << 8) | (1 << 9));

            if (ObjHit)
                current = ObjHit.collider.transform.parent.gameObject;
            else
                current = null;

            if (current != last)
            {
                if (choosing)
                {
                    if (last != null)
                        SetIdle(last);
                    if (current != null && ((current.transform.Find("HitBox").gameObject.layer == 8 && toself) || (current.transform.Find("HitBox").gameObject.layer == 9 && !toself)))
                        SetEnable(current);
                }

                last = current;
            }

            if (Input.GetMouseButtonDown(0) && last != null)
            {
                if (choosing)
                {
                    SetIdle(last);
                    SingletonObject<SelectMedia>.GetInst().aimObj = Battle.instance.Obj2Char(last);
                    SingletonObject<SelectMedia>.GetInst().SendChoice();
                    last = null;
                    choosing = false;
                }
                else
                    SingletonObject<BattleUIMedia>.GetInst().OpenCharInfo(Battle.instance.Obj2Char(last));
            }
        }
    }

    public void SetChoosing(bool choosing, bool toself)
    {
        this.toself = toself;
        this.choosing = choosing;
    }
}