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

#region 使用说明
/// <summary>
/// 1、创建资产文件
/// 2、在需要使用事件机制的地方，声明并序列化EventChannel
/// 3、在订阅者的OnEnable函数中通过EventChannel注册事件，
///     在OnDisable函数中通过EventChannel退订事件。
/// 4、发布者在适当的的方法中调用广播函数。
/// </summary>
#endregion