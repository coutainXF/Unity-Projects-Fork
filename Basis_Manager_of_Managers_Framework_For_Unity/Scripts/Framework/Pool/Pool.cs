using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public GameObject Prefab => prefab;
    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;
    Transform parent;//ָ��������
    Queue<GameObject> _queue;

    public int Size => size;//��ʼʱ�ߴ�
    public int RuntimeSize => _queue.Count;//����ʱ�ߴ�

    public void Initialize(Transform parent)
    {
        _queue = new Queue<GameObject>();
        this.parent = parent;
        for (var i = 0; i < size; i++)
        {
            _queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab, parent);
        copy.SetActive(false);
        return copy;
    }

    GameObject AvailableObject()
    {
        GameObject availableObject;
        //Peekȡ�����Ƕ��е�һ�����󣬼������activeSelfֵ����ֹ�������ڹ����Ķ���
        if (_queue.Count > 0 && !_queue.Peek().activeSelf)
        {
            availableObject = _queue.Dequeue();
        }
        else
        {
            availableObject = Copy();
        }
        //�������֮���������С�
        _queue.Enqueue(availableObject);
        return availableObject;
    }

    //�Ӷ�����л�ȡ��Ҫʹ�õĶ��󣬲���������SetActive(true)��
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }

    public GameObject PreparedObject(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        return preparedObject;
    }
}