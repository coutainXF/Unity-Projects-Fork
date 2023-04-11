using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Events;

/// <summary>
/// ��װ����Ľӿ�
/// </summary>
public interface IEventInfo
{

}

/// <summary>
/// ����װ�����࣬Ϊ��Ϸ�¼�ί�з���Ⱥ
/// ���÷��ͱ����װ�����
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

/*
 * �¼��������� ��������
 * 1.Dicionary
 * 2.ί��
 * 3.�۲������ģʽ
 * 4.����
 * 5.����ת��ԭ��
 */
public class EventCenter : Singleton<EventCenter>
{
    //key -- �¼���
    //value -- ������Ӧ�¼��ķ���ί�к���(���ͷ��㴫����Ϸ�������)
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// ����¼�����
    /// </summary>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί��</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {

        if (eventDic.ContainsKey(name))
        {
            //�м���ಥ����
            (eventDic[name] as EventInfo<T>).actions += action;
            Debug.Log("ԭ�¼���" + name + " �����»��" + action.ToString());
        }
        else
        {
            //û�д����µĶ���
            eventDic.Add(name, new EventInfo<T>(action));
            Debug.Log("�������¼���" + name + " �׸����" + action.ToString());

        }
    }

    /// <summary>
    /// ����Ҫ���ݲ������¼���ӷ���
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(string name, UnityAction action)
    {

        if (eventDic.ContainsKey(name))
        {
            //�м���ಥ����
            (eventDic[name] as EventInfo).actions += action;
            Debug.Log("ԭ�¼���" + name + " �����»��" + action.Method.Name.ToString());
        }
        else
        {
            //û�д����µĶ���
            eventDic.Add(name, new EventInfo(action));
            Debug.Log("�������¼���" + name + " �׸����" + action.Method.Name.ToString());
        }
    }

    /// <summary>
    /// �¼����
    /// </summary>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί��</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
        Debug.LogWarning("δ��ѯ����Ӧ�¼�? ��" + name);
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
        Debug.LogWarning("δ��ѯ����Ӧ�¼�? ��" + name);
    }


    /// <summary>
    ///  �¼�����
    /// </summary>
    /// <param name="name">��Ӧ���Ƶ��¼�����</param>
    /// <param name="info">�¼����ݵ��������</param>
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo<T>).actions != null)
            {
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
            else
            {
                Debug.Log("��Ӧ������ڣ�");
            }
        }
        else
        {
            Debug.LogWarning("δ��ѯ����Ӧ�¼�? ��" + name);
        }
    }

    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if ((eventDic[name] as EventInfo).actions != null)
            {
                (eventDic[name] as EventInfo).actions.Invoke();
            }
            else
            {
                Debug.Log("��Ӧ������ڣ�");
            }
        }
        else
        {
            Debug.LogWarning("δ��ѯ����Ӧ�¼�? ��" + name);
        }
    }

    /// <summary>
    /// ��������¼�
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
