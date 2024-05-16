using TMPro;
using UnityEngine;

public class UI_LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    void Start()
    {
        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
    }

}
