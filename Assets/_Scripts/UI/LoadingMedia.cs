using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMedia : BaseMedia
{
    private Text hint;
    private Slider loadingSlider;

    public override void Init()
    {
        base.Init();

        hint = panel.Find("Hint").GetComponent<Text>();
        loadingSlider = panel.Find("LoadingSlider").GetComponent<Slider>();
    }

    public override void Open()
    {
        base.Open();
        loadingSlider.value = 0.0f;

        int randomNum = UnityEngine.Random.Range(0, 10000) % hintDic.Count;
        hint.text = GameSettings.language == Language.English ? hintDic[randomNum] : hintDicCN[randomNum];
    }

    public void SetProgress(float progress)
    {
        loadingSlider.value = progress;
    }

    public static Dictionary<int, string> hintDic = new Dictionary<int, string>()
    {
        {0, "When you meet monsters that are difficult to deal with, you can try to learn different skills and talents." },
        {1, "After entering new battle, the monster will get a combat Buff, which is directly related to the difficulty of the battle." },
        {2, "Equipment with special attributes play a big role when facing some particular monsters." },
        {3, "If you are not satisfied with the apperance of your equipment, you can go to the blacksmith for replacement/customization." },
        {4, "Your choices in some conversations are related to the difficulty and development of the game." },
    };

    public static Dictionary<int, string> hintDicCN = new Dictionary<int, string>()
    {
        {0,"遇到难以应付的怪物时，可以尝试学习不同的技能与天赋。" },
        {1,"进入战斗后怪物会获得一个战斗增益Buff，这直接关系到战斗的难易程度。" },
        {2,"特定属性的装备在面对特定的怪物时往往会有很大的作用。" },
        {3,"如果对一件装备的外形不满意，可以去铁匠处花费一定金币进行外形替换/定制。" },
        {4,"部分对话中的选择关系到游戏的难度与发展。" },
    };
}
