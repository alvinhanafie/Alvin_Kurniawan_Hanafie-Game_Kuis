using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private Animator _animator = null;
    [SerializeField]
    private InitialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;

    [SerializeField]
    private RectTransform _content = null;

    private void Start()
    {
        //LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(null, _inisialData.levelPack, false);
        }

        //subscribe events
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy()
    {
        //unsubscribe events
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(UI_OpsiLevelPack tombolLevelPack, 
    LevelPackKuis levelPack, bool terkunci)
    {
        if (terkunci)
        {
            return;
        }

        //buka menu level
        //_levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        //tutup menu level
        //gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;
        _animator.SetTrigger("KeLevels");
    }
    public void LoadLevelPack(LevelPackKuis[] levelPacks, PlayerProgress.MainData 
        playerData)
    {
        foreach (var lp in levelPacks)
        {
            //membuat salinan objek dari prefab tombol level pack
            var t = Instantiate(_tombolLevelPack);

            t.SetLevelPack(lp);

            //masukkan objek tombol sebagai anak dari objek content
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;

            //cek apakah level pack terdaftar di dictionary progres pemain
            if (!playerData.progresLevel.ContainsKey(lp.name))
            {
                //jika tidak terdaftar level pack terkunci
                t.KunciLevelPack();
            }
        }
    }
}
