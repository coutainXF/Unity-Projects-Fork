using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���������������ڼ��س������ṩһ��ͼƬ���ı�����alphaֵ��ʵ�ֵ��뵭��
/// </summary>
public class SceneLoader : PersistentSingleton<SceneLoader>
{
    //�����string�ǳ��������ơ�
    const string GAMEPLAY = "GamePlay";
    const string MAIN_MENU = "MainMenu";
    const string SCORING = "Scoring";
    [SerializeField] Image transitionImage;
    [SerializeField] float fadeTime = 3.5f;

    Color _color;

    /// <summary>
    /// ֱ�Ӽ��أ����ܻῨ�٣�
    /// </summary>
    /// <param name="sceneName"></param>
    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// �첽����
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadingCoroutine(string sceneName)
    {
        //load new scene in background and
        var loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        //set this scene inactive
        loadingOperation.allowSceneActivation = false;

        transitionImage.gameObject.SetActive(true);
        //Fade out
        while (_color.a < 1f)
        {
            _color.a = Mathf.Clamp01(_color.a += Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = _color;
            yield return null;
        }
        //�жϼ��ؽ����Ƿ񳬹�90%�������������ִ�м��������������첽����ֱ���ﵽҪ�����
        yield return new WaitUntil(() => loadingOperation.progress >= .9f);
        //active the new scene
        loadingOperation.allowSceneActivation = true;

        //Fade in
        while (_color.a > 0f)
        {
            _color.a = Mathf.Clamp01(_color.a -= Time.unscaledDeltaTime / fadeTime);
            transitionImage.color = _color;
            yield return null;
        }
        transitionImage.gameObject.SetActive(false);
    }

    public void LoadGamePlayScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(GAMEPLAY));
    }

    //�������˵�����
    public void LoadMainMenuScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(MAIN_MENU));
    }

    public void LoadScoringScene()
    {
        StopAllCoroutines();
        StartCoroutine(LoadingCoroutine(SCORING));
    }
}