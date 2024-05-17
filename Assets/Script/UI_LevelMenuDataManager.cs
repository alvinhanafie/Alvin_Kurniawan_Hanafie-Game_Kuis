using TMPro;
using UnityEngine;

public class UI_LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private UI_LevelPackList _LevelPackList = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [Space, SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    void Start()
    {
        if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgres();
        }
        
        _LevelPackList.LoadLevelPack(_levelPacks, _playerProgress.progresData);

        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
        AudioManager.instance.PlayBGM(0);

    }
    public void UpdateKoinDisplay()
    {
        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
    }
}
