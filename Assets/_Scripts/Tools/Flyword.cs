using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FlyWordType
{
    normal,
    critical,
    heal,
    magic,
}

public class Flyword : MonoBehaviour
{

    public float m_horizontalSpeed;
    public float m_verticalSpeed;
    public float m_verticalDecreaseSpeed;
    private int m_dir;
    public float m_lastTime;
    public float m_startTime;
    private Vector3 worldPos;
    public GameObject[] wordChildren;
    public string m_word;
    private GameObject m_panel;
    private List<Image> words;
    private GameObject group;

    public void Init(Vector3 position, string word, GameObject panel, FlyWordType type = FlyWordType.normal)
    {
        m_horizontalSpeed = Random.Range(0.75f, 1.5f);
        m_verticalSpeed = Random.Range(5f, 8f);

        //m_lastTime = 1f;
        m_panel = panel;
        m_dir = (Random.Range(0, 2) == 0 ? 1 : -1);
        worldPos = position;
        transform.position = Camera.main.WorldToScreenPoint(worldPos);
        m_startTime = Time.time;
        SetScale(1);

        group = m_panel;
        switch (type)
        {
            case FlyWordType.normal:
                SetWord(word);
                break;
            case FlyWordType.critical:
                SetWord(word, "Critical_0", null, 1.5f);
                break;
            case FlyWordType.heal:
                SetWord(word, "Heal_0");
                break;
            case FlyWordType.magic:
                SetWord(word, "MagicDamage_0");
                break;
            default:
                break;
        }
    }

    protected float easeInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
    }

    protected float easeOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
    }

    void SetAlpha(float value)
    {
        Color col = gameObject.GetComponent<Image>().color;
        col.a = value;
        gameObject.GetComponent<Image>().color = col;
    }

    void SetScale(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }

    // Update is called once per frame
    void Update()
    {
        worldPos += m_dir * Vector3.right * m_horizontalSpeed * Time.deltaTime;

        //float verticalSpeed = easeOutSine(m_verticalSpeed, 0, (TimerEx.CurTime - m_startTime) / (m_lastTime + m_delay));
        worldPos += Vector3.up * m_verticalSpeed * Time.deltaTime;

        transform.position = Camera.main.WorldToScreenPoint(worldPos);

        m_verticalSpeed -= m_verticalDecreaseSpeed * Time.deltaTime;

        float percent = Mathf.Clamp01((Time.time - m_startTime) / 0.5f);
        float alpha = easeInSine(0.5f, 1, percent);

        //SetAlpha(alpha);

        percent = Mathf.Clamp01((Time.time - m_startTime));
        float scale = easeInSine(0.5f, 2, percent);
        SetScale(scale);
        if (Time.time - m_startTime >= m_lastTime)
        {
            // TODO : Object Pool
            m_panel.SetActive(false);
        }
    }

    //设置图片文字
    void SetWord(string word, string added = "", Sprite sprite = null, float scale = 1)
    {
        words = new List<Image>();

        for (int i = 0; i < 6; i++)
        {
            words.Add(m_panel.transform.Find("Group/" + i.ToString()).GetComponent<Image>());
            words[i].gameObject.SetActive(false);
            group.transform.localScale = Vector3.one * scale;
        }

        GameObject type = transform.Find("Group/TypeParent/type").gameObject;
        if (sprite == null)
        {
            type.SetActive(false);
        }
        else
        {
            type.SetActive(true);
            type.GetComponent<Image>().sprite = sprite;
        }
        if (word == "")
        {
            return;
        }

        for (int i = 0; i < word.Length; i++)
        {
            words[i].gameObject.SetActive(true);
            string name = word[word.Length - i - 1].ToString();
            words[i].sprite = AtlasManager.GetInst().GetFlyWordAtlas(added + name);
        }
    }
}
