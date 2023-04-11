using UnityEngine.Events;
using UnityEngine;
using System.Collections;

//��Դ����ģ��
public class ResourcesManager : Singleton<ResourcesManager>
{
    //ͬ��������Դ
    public T Load<T>(string name) where T : Object
    {

        T res = Resources.Load<T>(name);
        //resources.Load-������Դ

        //���������һ��GameObject���͵ģ��Ұ���ʵ�������ٷ��س�ȥֱ��ʹ�á�
        //is���һ�������Ƿ������ָ�������� 
        if (res is GameObject)
            return GameObject.Instantiate(res);//ʵ����
        else //else���ʾ����TextAsset��AudioClip
            return res;
    }

    //�첽������Դ 
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        //�����첽���ص�Э��
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync<T>(name, callback));
    }

    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        {
            //Resources.LoadAsync �첽����Resources�ļ����е���Դ�� 
            //ResourceRequest����Դ���첽��������
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;

            if (r.asset is GameObject)
            {
                //ʵ����һ���ٴ�������
                callback(GameObject.Instantiate(r.asset) as T);
            }
            else
            {
                //ֱ�Ӵ�������
                callback(r.asset as T);
            }
        }
    }
}