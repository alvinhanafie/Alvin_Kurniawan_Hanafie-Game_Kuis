using TMPro;
using UnityEngine;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _tempatPesan = null;

    public string pesan
    {
        get => _tempatPesan.text;
        set => _tempatPesan.text = value;
    }
    private void Awake()
    {
        // for turning of message console before start
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

}
