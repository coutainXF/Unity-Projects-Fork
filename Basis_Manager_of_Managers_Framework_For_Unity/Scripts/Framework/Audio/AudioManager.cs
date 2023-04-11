using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;

    const float MIN_PITCH = .9f;
    const float MAX_PITCH = 1.1f;

    //used for ui SFX
    //�����ڲ���ui��Ч
    public void PlaySFX(AudioData audioData)
    {
        //sFXPlayer.clip = audioClip;
        //sFXPlayer.volume = volume;
        //sFXPlayer.Play();�ú����޷����Ÿ�������Ƶ������ظ�����һ���ֶ��޷����������о������ϡ�
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }

    //����ı����߶������� �໥�������Ƶ
    //used for repeat-play SFX
    //�����ڲ����ظ����ŵ���Ч
    public void PlayRandomSFX(AudioData audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        sFXPlayer.PlayOneShot(audioData.audioClip, audioData.volume);
    }

    //�������ĳЩ
    public void PlayRandomSFX(AudioData[] audioData)
    {
        sFXPlayer.pitch = Random.Range(MIN_PITCH, MAX_PITCH);
        PlayRandomSFX(audioData[Random.Range(0, audioData.Length)]);
    }
}
[System.Serializable]
public class AudioData
{
    public AudioClip audioClip;
    public float volume;
}