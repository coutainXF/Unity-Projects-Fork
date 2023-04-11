using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 场景加载器，用于加载场景，提供一个图片，改变它的alpha值来实现淡入淡出
/// </summary>
public class SceneLoader : PersistentSingleton<SceneLoader>
{
    //这里的string是场景的名称。
    const string GAMEPLAY = "GamePlay";
    const string MAIN_MENU = "MainMenu";
    const string SCORING = "Scoring";
    [SerializeField] Image transitionImage;
    [SerializeField] float fadeTime = 3.5f;

    Color _color;

    /// <summary>
    /// 直接加载（可能会卡顿）
    /// </summary>
    /// <param name="sceneName"></param>
    void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 异步加载
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
        //判断加载进度是否超过90%，如果是则往下执行激活场景，否则继续异步加载直到达到要求进度
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

    //加载主菜单场景
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