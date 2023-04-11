using UnityEngine;

//����ģʽ���ͻ���
public class Singleton<T> : MonoBehaviour where T : Component
{
    /*
     *  T,ָ���ͣ�
     *  where T:Component ָ���˷��͵�Լ���������ͱ����Ǽ̳�Component��
     *  ��Component�Ǹ��ӵ�������Ϸ����Ļ�����(Base class for everything attached to GameObjects.)����������õ���װ���κι��ص���Ϸ�����ϵ�����
     */
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this as T;
    }//���ظ����͵�ʵ��

}