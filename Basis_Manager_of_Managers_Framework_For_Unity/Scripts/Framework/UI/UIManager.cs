using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// UI�㼶
/// </summary>
public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System,
}

/// <summary>
/// UI������
/// 1.����������ʾ�����
/// 2.�ṩ���ⲿ ��ʾ�����صȵȽӿ�
/// </summary>
public class UIManager : Singleton<UIManager>
{
    public Dictionary<string, UIBasePanel> panelDic = new Dictionary<string, UIBasePanel>();

    public GameObject canvasObj;

    Transform bot;
    Transform mid;
    Transform top;
    Transform system;

    Scene UIScene;



    public UIManager()
    {
        //����Canvas �����������ʱ�� �����Ƴ�
        canvasObj = ResourcesManager.Instance.Load<GameObject>("UI/MainCanvas");

        Transform canvas = canvasObj.transform;
        Object.DontDestroyOnLoad(canvasObj);

        //��������
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");


    }

    /// <summary>
    /// ��ʾ�����Canve�ṹ�㼶��
    /// </summary>
    /// <typeparam name="T">���Ų�����</typeparam>
    /// <param name="panelName">�����</param>
    /// <param name="layer">��ʾ�㼶</param>
    /// <param name="callBack">�������سɹ��������Ļص�����</param>
    public void ShowPanelOnLayer<T>(string panelName, E_UI_Layer layer = E_UI_Layer.Mid, UnityAction<T> callBack = null) where T : UIBasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            //ֻ�Ǵ�����崴����Ļص�
            callBack?.Invoke(panelDic[panelName] as T);
        }

        // �����Դ����
        // 1.�󶨵�����������
        // 2.����λ�ò㼶
        // 3.�������λ�úʹ�С
        ResourcesManager.Instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            Transform ui_layer = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    ui_layer = mid;
                    break;
                case E_UI_Layer.Top:
                    ui_layer = top;
                    break;
                case E_UI_Layer.System:
                    ui_layer = system;
                    break;
            }

            obj.transform.SetParent(ui_layer);

            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.one;

            //�õ�Ԥ�������ϵ����Ų�
            T panel = obj.GetComponent<T>();

            //������崴����ɺ���߼�
            callBack?.Invoke(panel);
            //����������
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="panelName"></param>
    public bool DestoryPanelInDic(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            Object.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
            return true;
        }

        return false;
    }

    /// <summary>
    /// ��ȡUI����㼶����
    /// </summary>
    /// <returns></returns>
    public Transform GetUIlayer(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bot:
                return bot;
            case E_UI_Layer.Mid:
                return mid;
            case E_UI_Layer.Top:
                return top;
            case E_UI_Layer.System:
                return system;
            default:
                return system;
        }

    }

    /// <summary>
    /// ���UI���
    /// </summary>
    /// <returns>UI�������</returns>
    public T GetPanel<T>(string name) where T : UIBasePanel
    {
        if (panelDic.ContainsKey(name))
            return panelDic[name] as T;
        return null;
    }

    /// <summary>
    /// ��UI�ؼ�����Զ�������¼�
    /// </summary>
    /// <param name="control">�ؼ�����</param>
    /// <param name="type">�¼�����</param>
    /// <param name="callBack">�¼���Ӧ����</param>
    public static void AddUIEventListener(UIBehaviour control, EventTriggerType type,
        UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = control.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }
}
