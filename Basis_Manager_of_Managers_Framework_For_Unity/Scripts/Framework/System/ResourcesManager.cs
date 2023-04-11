using UnityEngine.Events;
using UnityEngine;
using System.Collections;

//资源加载模块
public class ResourcesManager : Singleton<ResourcesManager>
{
    //同步加载资源
    public T Load<T>(string name) where T : Object
    {

        T res = Resources.Load<T>(name);
        //resources.Load-加载资源

        //如果对象是一个GameObject类型的，我把它实例化后，再返回出去直接使用。
        //is检查一个对象是否兼容于指定的类型 
        if (res is GameObject)
            return GameObject.Instantiate(res);//实例化
        else //else情况示例：TextAsset、AudioClip
            return res;
    }

    //异步加载资源 
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        //开启异步加载的协程
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync<T>(name, callback));
    }

    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        {
            //Resources.LoadAsync 异步加载Resources文件夹中的资源。 
            //ResourceRequest从资源包异步加载请求。
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;

            if (r.asset is GameObject)
            {
                //实例化一下再传给方法
                callback(GameObject.Instantiate(r.asset) as T);
            }
            else
            {
                //直接传给方法
                callback(r.asset as T);
            }
        }
    }
}