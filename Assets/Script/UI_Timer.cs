using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    public static event System.Action EventWaktuHabis;

    //[SerializeField]
    //private UI_PesanLevel _tempatPesan = null;
    [SerializeField]
    private Slider _timeBar = null;

    [SerializeField]
    private float _waktuJawab = 30f;
    private bool _waktuBerjalan = true;
    private float _sisawaktu = 0f; //Data Sementara

    public bool waktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }

    private void Start()
    {
        UlangWaktu();
    }    

    private void Update()
    {
        if (!_waktuBerjalan)
            return;

        _sisawaktu -= Time.deltaTime;
        _timeBar.value = _sisawaktu / _waktuJawab;

        if(_sisawaktu <= 0f)
        {
            //_tempatPesan.pesan = "Waktu Sudah Habis!!!";
            //_tempatPesan.gameObject.SetActive(true);
            //Debug.Log("Waktu Habis");

            EventWaktuHabis?.Invoke();
            _waktuBerjalan = false;
            return;
        }

        //Debug.Log(_sisawaktu);
    }
    public void UlangWaktu()
    {
        _sisawaktu = _waktuJawab;
    }
}
