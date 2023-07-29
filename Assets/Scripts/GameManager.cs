using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq; List i�in olu�turulan k�t�phane 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public Soru[] sorular; //Soru scripts dosyas�n� �a��rm�� olduk 
    private static List<Soru> cevaplanmamisSorular; //static: oyunun herhangi bir yerinden eri�ilebilir
    private Soru gecerliSoru;

    [SerializeField]
    private Text soruText;

    [SerializeField]
    private Text dogruCevapText, yanlisCevapText;

    [SerializeField]
    private GameObject dogruButon, yanlisButon;

    [SerializeField]
    private GameObject sonucPanel;

    int dogruAdet, yanlisAdet,toplamPuan;

    SonucManager sonucManager;
    
    void Start()
    {
        if (cevaplanmamisSorular == null || cevaplanmamisSorular.Count == 0)
        {
            cevaplanmamisSorular = new List<Soru>(sorular);
        }

        dogruAdet = 0;
        yanlisAdet = 0;
        toplamPuan = 0;

        RastgeleSoruSec();

       
    }
    void RastgeleSoruSec()
    {
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(323,.2f);//yanl�sButton'un konumu
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-323, .2f);
        int randomSay�Index = Random.Range(0,cevaplanmamisSorular.Count);
        gecerliSoru = cevaplanmamisSorular[randomSay�Index];

        soruText.text = gecerliSoru.soru;//soruText'in text b�l�m��ne gecerliSoru'nun sorusunu texte yazd�r.

        if(gecerliSoru.dogrumu)
        {
            dogruCevapText.text = "DO�RU CEVAPLADINIZ";
            yanlisCevapText.text = "YANLI� CEVAPLADINIZ";
        }
        else
        {
            dogruCevapText.text = "YANLI� CEVAPLADINIZ";
            yanlisCevapText.text = "DO�RU CEVAPLADINIZ";
        }
    }
    IEnumerator SorularAras�BekleRoutine()
    {
        cevaplanmamisSorular.Remove(gecerliSoru);
        yield return new WaitForSeconds(1f);

      /*  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); -->Ayn� sahneyi ekleyece�imiz i�in ayn� sahneyi yaz�yoruz.
        GetActiveScene().buildIndex --> Aktiv olan sahnenin index numaras�n� y�ke  */

        if(cevaplanmamisSorular.Count<=0)
        {
            sonucPanel.SetActive(true);
            sonucManager=Object.FindObjectOfType<SonucManager>();//sonucPanel aktif olduktan sonra bize SonucManager scriptine ulas�n 
            sonucManager.SonuclariYazdir(dogruAdet, yanlisAdet, toplamPuan);
        }
        else
        {
            RastgeleSoruSec();
        }
    }
    public void dogruButonaBasildi()
    {
        if(gecerliSoru.dogrumu)//do�ruysa
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
        }
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f,.2f);//dogruCevapText'e bas�ld���nda yanl�sButton �ekilecek ve alttaki siyah ekran g�r�necek.
       StartCoroutine(SorularAras�BekleRoutine());
    }
    public void yanl�sButonaBasildi()
    {
        if (!gecerliSoru.dogrumu)//gecerliSoru.dogrumu==false ile !gecerliSoru.dogrumu ayn� �ey 
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
        }
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-2000f, .2f);
        StartCoroutine(SorularAras�BekleRoutine());
    }

    public void YenidenOyna()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


