using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventSystem/EventChannel", fileName = "EventChannel_")]
public class EventChannel : ScriptableObject
{
    event System.Action Delegate;

    public void Broadcast()
    {
        Delegate?.Invoke();
    }

    public void AddListener(System.Action action)
    {
        Delegate += action;
    }

    public void RemoveListener(System.Action action)
    {
        Delegate -= action;
    }
}

#region ʹ��˵��
/// <summary>
/// 1�������ʲ��ļ�
/// 2������Ҫʹ���¼����Ƶĵط������������л�EventChannel
/// 3���ڶ����ߵ�OnEnable������ͨ��EventChannelע���¼���
///     ��OnDisable������ͨ��EventChannel�˶��¼���
/// 4�����������ʵ��ĵķ����е��ù㲥������
/// </summary>
#endregion