using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : SingletonObject<TriggerManager>
{
    public void Init()
    {

    }

    public void EnableTriggers(List<int> triggers)
    {
        for (int i = 0; i < triggers.Count; ++i)
            EnableTrigger(triggers[i]);
    }

    private void EnableTrigger(int id)
    {
        Trigger trigger = TriggerLoader.data[id];

        switch (trigger.type)
        {
            case TriggerType.changeNpcDialog:
                NpcLoader.data[trigger.parameters[0]].dialogId = trigger.parameters[1];
                break;
            case TriggerType.startDialogDirectly:
                SingletonObject<DialogMedia>.GetInst().StartDialogDirectly(trigger.parameters[0]);
                break;
            case TriggerType.openShop:
                switch (trigger.parameters[0])
                {
                    case 1:
                        SingletonObject<ShopMedia>.GetInst().SetMode(ShopMode.buy);
                        SingletonObject<ShopMedia>.GetInst().Open();
                        break;
                    case 2:
                        SingletonObject<ShopMedia>.GetInst().SetMode(ShopMode.sell);
                        SingletonObject<ShopMedia>.GetInst().Open();
                        break;
                    case 3:
                        SingletonObject<ShopMedia>.GetInst().SetMode(ShopMode.build);
                        SingletonObject<ShopMedia>.GetInst().Open();
                        break;
                    case 4:
                        SingletonObject<ShopMedia>.GetInst().SetMode(ShopMode.customize);
                        SingletonObject<ShopMedia>.GetInst().Open();
                        break;
                    default:
                        SingletonObject<ShopMedia>.GetInst().SetMode(ShopMode.buy);
                        SingletonObject<ShopMedia>.GetInst().Open();
                        break;
                }
                break;
            case TriggerType.openTask:
                TaskManager.GetInst().SetAccept(trigger.parameters[0]);
                break;
            case TriggerType.acceptTask:
                TaskManager.GetInst().AddTask(trigger.parameters[0]);
                break;
            case TriggerType.completeTask:
                TaskManager.GetInst().Complete(trigger.parameters[0]);
                break;
            case TriggerType.taskConditionEnabled:
                PlayerData.GetInst().conditions[trigger.parameters[0]] = true;
                PlayerData.GetInst().RefreshProgress(-1);
                break;
            case TriggerType.gainItem:
                ItemManager.GetInst().GainNewItem(trigger.parameters[0], trigger.parameters[1], (ItemQuality)trigger.parameters[2], (WeaponType)trigger.parameters[3]);
                break;
            case TriggerType.sameSceneGoTo:
                Main.instance.StartCoroutine(Main.instance.EnterScene(Main.instance.sceneManager.sceneName, trigger.parameters[0]));
                break;
            case TriggerType.revive:
                PlayerData.GetInst().Revive();
                break;
            case TriggerType.theend:
                if (PlayerData.GetInst().conditions[1035])
                    SingletonObject<GuideMedia>.GetInst().SetMode(GuideMode.end2);
                else if (PlayerData.GetInst().conditions[1034])
                    SingletonObject<GuideMedia>.GetInst().SetMode(GuideMode.end1);
                else
                    SingletonObject<GuideMedia>.GetInst().SetMode(GuideMode.end3);
                SingletonObject<GuideMedia>.GetInst().Open();
                break;
            case TriggerType.bgmMusic:
                AudioManager.GetInst().PlayBGM(trigger.parameters[0]);
                break;
            case TriggerType.effectsMusic:
                AudioManager.GetInst().PlayEffect(trigger.parameters[0]);
                break;
        }

        if (Main.instance.sceneManager != null)
            Main.instance.sceneManager.Refresh();
    }
}