using UnityEngine;
using UnityEngine.UI;

public class PlayerHpFiller : Singleton<PlayerHpFiller>
{
    Image _image;
    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
    }

    void Start()
    {
        _image.fillAmount = 1;
    }

    //更新玩家血量
    public void UpdatePlayerHp(int currentHp,int maxHp)
    {
        _image.fillAmount = (float)currentHp / (float)maxHp;
    }
}
