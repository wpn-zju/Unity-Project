using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BaseSceneManager
{
    public string sceneName;
    public string campName;     // obsolete
    public int sceneBGMID;
    public string monsterName;
    public int monsterId;
    public Vector2 returnPos;
    public bool registered;
    public bool dirty;
    public Dictionary<string, NpcState> monsterInScene = new Dictionary<string, NpcState>();
    public Dictionary<string, NpcState> npcInScene = new Dictionary<string, NpcState>();

    public virtual void Init()
    {
        if (!Main.instance.sceneList.Contains(this))
            Main.instance.sceneList.Add(this);

        registered = false;
    }

    public virtual IEnumerator EnterScene(int entrance = 0)
    {
        SingletonObject<FightMedia>.GetInst().Close();
        MyPlayer.instance.gameObject.SetActive(false);
        SingletonObject<LoadingMedia>.GetInst().Open();
        yield return Main.instance.operation = SceneManager.LoadSceneAsync(sceneName);
        SingletonObject<LoadingMedia>.GetInst().Close();
        SingletonObject<FightMedia>.GetInst().Open();
        if (entrance != -1)
            MyPlayer.instance.gameObject.transform.position = GameObject.Find("SpawnPoint" + entrance.ToString()).transform.position;
        else
            MyPlayer.instance.gameObject.transform.position = ES2.Load<Vector3>(DataIO.savePath + "?tag=position");
        Vector3 temp = MyPlayer.instance.transform.position;
        Camera.main.transform.position = new Vector3(temp.x, temp.y, -10);
        MyPlayer.instance.gameObject.SetActive(true);
        SingletonObject<FightMedia>.GetInst().cantMove = false;
        if (!registered) Register();
        Refresh();
    }
    
    public virtual IEnumerator ExitScene()
    {
        yield return null;
    }

    public virtual IEnumerator ReturnScene() // return from battle
    {
        SingletonObject<FightMedia>.GetInst().Close();
        MyPlayer.instance.gameObject.SetActive(false);
        SingletonObject<LoadingMedia>.GetInst().Open();
        yield return Main.instance.operation = SceneManager.LoadSceneAsync(sceneName);
        SingletonObject<LoadingMedia>.GetInst().Close();
        SingletonObject<FightMedia>.GetInst().Open();
        MyPlayer.instance.gameObject.transform.position = returnPos;
        Camera.main.transform.position = new Vector3(returnPos.x, returnPos.y, -10);
        MyPlayer.instance.gameObject.SetActive(true);
        SingletonObject<FightMedia>.GetInst().cantMove = false;
        if (monsterInScene.ContainsKey(monsterName))
            monsterInScene[monsterName].dead = true;
        Refresh();
    }

    public virtual void Register()
    {
        if (GameObject.Find("Monster") != null)
            for (int i = 0; i < GameObject.Find("Monster").transform.childCount; ++i)
            {
                Transform monster = GameObject.Find("Monster").transform.GetChild(i);
                if (monster.GetComponent<Patrol>() != null)
                    monsterInScene[monster.name] = monster.GetComponent<Patrol>().state;
            }

        if (GameObject.Find("Npc") != null)
            for (int i = 0; i < GameObject.Find("Npc").transform.childCount; ++i)
            {
                Transform npc = GameObject.Find("Npc").transform.GetChild(i);
                if (npc.GetComponent<Patrol>() != null)
                    npcInScene[npc.name] = npc.GetComponent<Patrol>().state;
            }

        if (dirty)
        {
            Dictionary<string, bool> sceneStateDicLoad = ES2.LoadDictionary<string, bool>(DataIO.savePath + "?tag=scenestatedic");

            foreach (KeyValuePair<string, bool> kvp in sceneStateDicLoad)
                monsterInScene[kvp.Key].dead = kvp.Value;
        }

        registered = true;
    }

    public virtual void Refresh() // 1. Triggers 2. Map Lock 3. Characters
    {
        if (GameObject.Find("Monster") != null)
            foreach (KeyValuePair<string, NpcState> kvp in monsterInScene)
            {
                if (kvp.Value.visible && !kvp.Value.dead)
                    GameObject.Find("Monster").transform.Find(kvp.Key).gameObject.SetActive(true);
                else
                    GameObject.Find("Monster").transform.Find(kvp.Key).gameObject.SetActive(false);
            }

        if (GameObject.Find("Npc") != null)
            foreach (KeyValuePair<string, NpcState> kvp in npcInScene)
            {
                if (kvp.Value.visible && !kvp.Value.dead)
                    GameObject.Find("Npc").transform.Find(kvp.Key).gameObject.SetActive(true);
                else
                    GameObject.Find("Npc").transform.Find(kvp.Key).gameObject.SetActive(false);
            }

        RefreshTaskHint();

        // Refresh BGM
        AudioManager.GetInst().PlayBGM(sceneBGMID);
    }

    public virtual void RefreshTaskHint()
    {
        if (GameObject.Find("Npc") != null)
            for (int i = 0; i < GameObject.Find("Npc").transform.childCount; ++i)
            {
                int npcId = GameObject.Find("Npc").transform.GetChild(i).GetComponent<Patrol>().id;

                NpcLoader.data[npcId].SetNpcObj(GameObject.Find("Npc").transform.GetChild(i));
            }
    }
}

[System.Serializable]
public class NpcState
{
    public bool visible = true;
    public bool dead = false;

    public NpcState(NpcState that)
    {
        visible = that.visible;
        dead = that.dead;
    }
}