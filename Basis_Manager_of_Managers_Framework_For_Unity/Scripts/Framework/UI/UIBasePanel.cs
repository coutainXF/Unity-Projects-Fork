using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI������
/// �ҵ�������µ�ȫ���ӿռ����
/// �ṩ��ʾ�����صȹ���
/// </summary>
public class UIBasePanel : MonoBehaviour
{
    //����ת����ԭ�� �洢���е��ӿؼ�  
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<TMP_Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<Scrollbar>();
        InitPanel();
    }

    protected virtual void Update()
    {
        RefreshPlane();
    }

    /// <summary>
    /// ��Ҫ������ʼ��
    /// </summary>
    protected virtual void InitPanel()
    {

    }


    /// <summary>
    /// ��ʾ���
    /// </summary>
    public virtual void RefreshPlane()
    {

    }


    /// <summary>
    /// �������
    /// </summary>
    protected virtual void DisplayPlane(bool d_Order)
    {
        gameObject.SetActive(d_Order);
    }

    /// <summary>
    ///   ��ö�Ӧ�����ӿؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; i++)
            {
                if (controlDic[controlName][i] is T)
                {
                    //������е��������
                    return controlDic[controlName][i] as T;
                }
            }
        }
        Debug.LogError("δ�鵽��ӦUI�����" + controlName);
        return null;
    }


    /// <summary>
    /// �ҵ��Ӷ���Ķ�Ӧ�ؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    protected void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        string objName;
        for (int i = 0; i < controls.Length; i++)
        {
            objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
            }
        }
    }

}