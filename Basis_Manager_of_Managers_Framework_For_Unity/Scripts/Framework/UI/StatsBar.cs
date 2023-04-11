using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    [SerializeField] Image fillImageBack;
    [SerializeField] Image fillImageFront;
    [SerializeField] bool delayFill = true;//�Ƿ���״̬������ӳ�
    [SerializeField] float fillDelay = .5f;//״̬�������ӳ�
    [SerializeField] float fillSpeed = .1f;//״̬������ٶ�

    float t;
    WaitForSeconds waitForDelayFill;
    Coroutine bufferedFillingCoroutine;

    float currentFillAmount;//ͼƬ�ĵ�ǰ���ֵ
    float previousFillAmount;
    protected float targetFillAmount;//ͼƬ��Ŀ�����ֵ

    void Awake()
    {
        if (TryGetComponent<Canvas>(out Canvas _canvas))
        {
            _canvas.worldCamera = Camera.main;
        }
        waitForDelayFill = new WaitForSeconds(fillDelay);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// ʵ��״̬����ʼ���ĺ���
    /// </summary>
    /// <param name="currentValue">��ǰֵ</param>
    /// <param name="maxValue">���ֵ</param>
    public virtual void Initialize(float currentValue, float maxValue)
    {
        currentFillAmount = currentValue / maxValue;
        targetFillAmount = currentFillAmount;
        fillImageBack.fillAmount = currentFillAmount;
        fillImageFront.fillAmount = currentFillAmount;
    }

    public void UpdateStats(float currentValue, float maxValue)
    {
        targetFillAmount = currentValue / maxValue;

        if (bufferedFillingCoroutine != null)
        {
            StopCoroutine(bufferedFillingCoroutine);
        }

        //if stats reduce   ���״̬����
        if (currentFillAmount > targetFillAmount)
        {
            //fill image front' fill amount = target fill amount  ��������ǰ��ͼƬ�����ֵ
            fillImageFront.fillAmount = targetFillAmount;
            //slowly reduce fill image back's fill amount �������º���ͼƬ�����ֵ
            bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageBack));
            return;
        }

        //if stats increase
        if (currentFillAmount < targetFillAmount)
        {
            //fill image back' fill amount = target fill amount
            fillImageBack.fillAmount = targetFillAmount;

            //slowly increase fill image front's fill amount
            bufferedFillingCoroutine = StartCoroutine(BufferedFillingCoroutine(fillImageFront));
        }
    }

    protected virtual IEnumerator BufferedFillingCoroutine(Image image)
    {
        if (delayFill)
        {
            yield return waitForDelayFill;
        }

        previousFillAmount = currentFillAmount;
        t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fillSpeed;
            currentFillAmount = Mathf.Lerp(previousFillAmount, targetFillAmount, t);
            image.fillAmount = currentFillAmount;
            yield return null;
        }
    }
}