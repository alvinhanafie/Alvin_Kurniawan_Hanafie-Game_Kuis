using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PoinJawaban : MonoBehaviour
{
    [SerializeField]
    private UI_PesanLevel _tempatPesan = null;

    [SerializeField]
    private TextMeshProUGUI _teksJawaban = null;
    [SerializeField]
    private bool _adalahBenar = false;

    public void PilihJawaban()
    {
        //Debug.Log($"{_teksJawaban.text} adalah {_adalahBenar}");
        _tempatPesan.pesan = $"{_teksJawaban.text} adalah {_adalahBenar}";
    }

    public void SetJawaban(string teksJawaban, bool adalahBenar)
    {
        _teksJawaban.text = teksJawaban;
        _adalahBenar = adalahBenar;
    }
}
