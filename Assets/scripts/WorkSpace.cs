using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System;

public class WorkSpace : MonoBehaviour
{

    public Sprite sprite;
    public Text txt_trig;
    public Text t_bud, t_app, t_hate, t_us, t_buy, t_prof, t_0, t_1, t_ok, t_add, t_serv;
    public bool isGameStopped = false;
    private int timer = 0;
    public Image dop_menu, img_ok, img_0, img_1, img_add, img_serv;
    int money_user;
    bool add_bought, server_bought;
    int ind;

    private void Start()
    {
        Checker(false);
        Game.SetGame("Labudabda");
        //t_bud.enabled = false;
        //t_app.enabled = false;
        //t_hate.enabled = false;
        //t_us.enabled = false;

        InfoReg();

        StartCoroutine(WaitAndPrint());
    }

    public void OnClickReturn()
    {
        SceneManager.LoadScene(0);
    }

    public void Trig(Sprite gm)
    {
        img_0.color = Color.white;
        img_1.color = Color.white;
        img_add.color = Color.white;
        img_serv.color = Color.white;
        txt_trig.enabled = true;
        ind = int.Parse(gm.name.Substring(0, 1));
        txt_trig.text = (ind + 1) + " регион";
        gm = sprite;
        Checker(true);
    }


    IEnumerator WaitAndPrint()
    {
        while (true)
        {
            if (!isGameStopped)
            {
                Game.com.Update();
                yield return new WaitForSeconds(3);
                InfoReg();
            }

            yield return null;
        }
    }

    public void InfoReg()
    {
        t_bud.text = "Budget: " + Game.com.Budjet.ToString() + "$" + " (" + Game.com.DBudget + "$)"; ;
        t_app.text = "Approval: " + Math.Round(Game.com.Approval, 3).ToString() + " (" + Math.Round(Game.com.D_Appoval, 3) + ")";
        t_hate.text = "Hateness: " + Math.Round(Game.com.Hate, 3).ToString() + " (" + Math.Round(Game.com.D_Hate, 3) + ")";
        t_us.text = "Users: " + Game.com.Users.ToString() + " (" + Game.com.D_Users + ")"; ;
    }

    public void Checker(bool bl)
    {
        dop_menu.enabled = bl;
        img_0.enabled = bl;
        img_1.enabled = bl;
        img_add.enabled = bl;
        img_ok.enabled = bl;
        img_serv.enabled = bl;
        t_buy.enabled = bl;
        t_prof.enabled = bl;
        t_0.enabled = bl;
        t_1.enabled = bl;
        t_ok.enabled = bl;
        t_add.enabled = bl;
        t_serv.enabled = bl;
    }

    public void ButtonUser(int mon)
    {
        if (mon == 0)
        {
            img_0.color = Color.red;
            img_1.color = Color.white;
        }
        else
        {
            img_1.color = Color.red;
            img_0.color = Color.white;
        }
        money_user = mon;
    }

    public void GoodsUser(string str)
    {
        if (str == "add")
        {
            add_bought = true;
            img_add.color = Color.red;
        }
        else
        {
            server_bought = true;
            img_serv.color = Color.red;
        }
    }

    public void Exit_Dop_Menu()
    {
        Checker(false);
        Game.reg[ind].SetAdv(money_user);
        if (add_bought)
            Game.reg[ind].Hype();
        else if(server_bought)                     
            Game.reg[ind].AddServer();

    }
}