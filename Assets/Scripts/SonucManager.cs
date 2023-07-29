using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonucManager : MonoBehaviour
{
    [SerializeField]
    private Text dogruTxt, yanlisTxt, puanTxt;

    [SerializeField]
    private GameObject birinciYildiz, ikinciYildiz, ucuncuYildiz;
    public void SonuclariYazdir(int dogruAdet,int yanlisAdet,int puan)
    {
        dogruTxt.text=dogruAdet.ToString();//gelen deðer int bu deðeri string dönüþtürülmeli
        yanlisTxt.text=yanlisAdet.ToString();
        puanTxt.text=puan.ToString();

        birinciYildiz.SetActive(false);
        ikinciYildiz.SetActive(false);
        ucuncuYildiz.SetActive(false);

        //Soru Sayýlarýnýn artýþýna göre farklý bir puanlama sistemine göre yýldýz sayýlarýný aktif edilebilir 
        if (dogruAdet <= 3)
        {
            birinciYildiz.SetActive(true);
        }
        else if (dogruAdet >= 4 && dogruAdet <= 7)
        {
            birinciYildiz.SetActive(true);
            ikinciYildiz.SetActive(true);
        }
        else if (dogruAdet >= 8 && dogruAdet <= 10)
        {
            birinciYildiz.SetActive(true);
            ikinciYildiz.SetActive(true);
            ucuncuYildiz.SetActive(true);
        }
        else //(dogruAdet == 0)
        {
            birinciYildiz.SetActive(false);
            ikinciYildiz.SetActive(false);
            ucuncuYildiz.SetActive(false);
        }
    }
}
