using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq; List için oluþturulan kütüphane 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    public Soru[] sorular; //Soru scripts dosyasýný çaðýrmýþ olduk 
    private static List<Soru> cevaplanmamisSorular; //static: oyunun herhangi bir yerinden eriþilebilir
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
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(323,.2f);//yanlýsButton'un konumu
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-323, .2f);
        int randomSayýIndex = Random.Range(0,cevaplanmamisSorular.Count);
        gecerliSoru = cevaplanmamisSorular[randomSayýIndex];

        soruText.text = gecerliSoru.soru;//soruText'in text bölümüüne gecerliSoru'nun sorusunu texte yazdýr.

        if(gecerliSoru.dogrumu)
        {
            dogruCevapText.text = "DOÐRU CEVAPLADINIZ";
            yanlisCevapText.text = "YANLIÞ CEVAPLADINIZ";
        }
        else
        {
            dogruCevapText.text = "YANLIÞ CEVAPLADINIZ";
            yanlisCevapText.text = "DOÐRU CEVAPLADINIZ";
        }
    }
    IEnumerator SorularArasýBekleRoutine()
    {
        cevaplanmamisSorular.Remove(gecerliSoru);
        yield return new WaitForSeconds(1f);

      /*  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); -->Ayný sahneyi ekleyeceðimiz için ayný sahneyi yazýyoruz.
        GetActiveScene().buildIndex --> Aktiv olan sahnenin index numarasýný yüke  */

        if(cevaplanmamisSorular.Count<=0)
        {
            sonucPanel.SetActive(true);
            sonucManager=Object.FindObjectOfType<SonucManager>();//sonucPanel aktif olduktan sonra bize SonucManager scriptine ulasýn 
            sonucManager.SonuclariYazdir(dogruAdet, yanlisAdet, toplamPuan);
        }
        else
        {
            RastgeleSoruSec();
        }
    }
    public void dogruButonaBasildi()
    {
        if(gecerliSoru.dogrumu)//doðruysa
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
        }
        yanlisButon.GetComponent<RectTransform>().DOLocalMoveX(1000f,.2f);//dogruCevapText'e basýldýðýnda yanlýsButton çekilecek ve alttaki siyah ekran görünecek.
       StartCoroutine(SorularArasýBekleRoutine());
    }
    public void yanlýsButonaBasildi()
    {
        if (!gecerliSoru.dogrumu)//gecerliSoru.dogrumu==false ile !gecerliSoru.dogrumu ayný þey 
        {
            dogruAdet++;
            toplamPuan += 100;
        }
        else
        {
            yanlisAdet++;
        }
        dogruButon.GetComponent<RectTransform>().DOLocalMoveX(-2000f, .2f);
        StartCoroutine(SorularArasýBekleRoutine());
    }

    public void YenidenOyna()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


