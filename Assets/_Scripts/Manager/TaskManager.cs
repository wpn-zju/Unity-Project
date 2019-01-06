using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : SingletonObject<TaskManager>
{
    public void Init()
    {

    }

    public void SetAccept(int taskId)
    {
        if (TaskLoader.data[taskId].state == TaskState.NotAcceptAndCouldNot)
            TaskLoader.data[taskId].state = TaskState.NotAcceptAndCould;
    }

    public void AddTask(int taskId)
    {
        if(TaskLoader.data[taskId].state == TaskState.NotAcceptAndCould)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint("You accept " + TaskLoader.data[taskId].name + " .", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint("你接受了 " + TaskLoader.data[taskId].cnName, Color.green);
            TaskLoader.data[taskId].state = TaskState.AcceptedAndUnfinished;
            TaskLoader.data[taskId].RefreshProgress(-1);
        }
    }

    public void Complete(int taskId)
    {
        if(TaskLoader.data[taskId].state == TaskState.FinishedButNotAwarded)
        {
            if (GameSettings.language == Language.English)
                SingletonObject<HintMedia>.GetInst().SendHint(TaskLoader.data[taskId].name + " finished.", Color.green);
            else
                SingletonObject<HintMedia>.GetInst().SendHint(TaskLoader.data[taskId].cnName + " 已完成", Color.green);
            TaskLoader.data[taskId].state = TaskState.FinishedAndAwarded;
            TaskLoader.data[taskId].GetAward();
            SingletonObject<FightMedia>.GetInst().RefreshTaskText();
        }
    }
}