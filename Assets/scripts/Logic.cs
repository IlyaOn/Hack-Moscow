using System;

public static class Game
{
    static System.Random rnd = new System.Random();
    public static double Tech_index = 1;
    public static Company com;
    public static Region[] reg = new Region[10];
    static public void SetGame(string name)
    {
        com = new Company(name);
        for (int i = 0; i < 10; i++)
        {
            reg[i] = new Region(rnd.Next(500000) + 100000);
        }

    }
}

public partial class Company
{
    // company name
    private string _name;

    public string Name
    {
        get
        {
            return _name;
        }
    }
    public double Tech_index = 1;
    public double Hate, D_Hate;
    public int Users, D_Users;
    public double Approval, D_Appoval;
    //budget of company
    private int _budget = 100000, D_Budget;
    public int DBudget { get { return D_Budget; } }
    public int Budjet
    {
        get
        {
            return _budget;
        }
        set
        {
            _budget = value;
        }
    }


}

public partial class Company
{
    public Company(string name)
    {
        //default parameters for each region
        _name = name;

    }

    public void Update()
    {
        int nu = 10;
        int money = 0;
        double hate = 0;
        double approval = 0;
        int users = 0;
        D_Budget = 0;
        D_Users = 0;
        D_Hate = 0;
        D_Appoval = 0;

        for (int i = 0; i < 10; i++)
        {
            Game.reg[i].Update();
            money += Game.reg[i].marja;
            hate += Game.reg[i].Hate * Game.reg[i].Users;
            users += Game.reg[i].Users;
            approval += Game.reg[i].Approval * Game.reg[i].Users;
            D_Budget += nu * Game.reg[i].delta_money;
            D_Users += nu * Game.reg[i].delta_users;
            D_Hate += Game.reg[i].delta_hate * Game.reg[i].Users;
            D_Appoval += Game.reg[i].delta_approval * Game.reg[i].Users;
        }
        _budget += money;
        Hate = hate / users;
        Approval = approval / users;
        Users = users;
        D_Appoval = nu * (D_Appoval / users);
        D_Hate = nu * (D_Hate / users);




    }
    int tech = 0;
    // explore new technology
    // works 
    public void App_technology(int tech)
    {
        switch (tech)
        {
            // encryption v1
            case 1:
                if (Budjet < 50000 && tech != 0)
                    break;
                _budget -= 50000;
                tech += 1;
                Game.Tech_index += 1;
                for (int i = 0; i < 10; i++)
                {
                    Game.reg[i].PlusApproval();
                    Game.reg[i].PlusHate();
                }
                break;
            // encryption v2
            case 2:
                if (Budjet < 100000 && tech != 1)
                    break;
                _budget -= 100000;
                tech += 1;
                Game.Tech_index += 2;
                for (int i = 0; i < 10; i++)
                {
                    Game.reg[i].PlusApproval();
                    Game.reg[i].PlusHate();
                }
                break;
            //private chat
            case 3:
                if (Budjet < 150000 && tech != 3)
                    break;
                _budget -= 150000;
                tech += 1;
                Game.Tech_index += 3;
                for (int i = 0; i < 10; i++)
                {
                    Game.reg[i].PlusApproval();
                    Game.reg[i].PlusHate();
                }
                break;
            // if smthing wrong
            default:
                break;
        }
    }
}

public partial class Region
{

    private double Sygm(double a)
    {
        return 1.9 / (1 + Math.Exp(-a));
    }
    //number of users
    private int _users;
    public int Users
    {
        get
        {
            return _users;
        }
    }

    //how users like your company
    private double _approval;

    public double Approval
    {
        get
        {

            return _approval;
        }
    }

    // governement hate
    private double _hate;
    public double Hate
    {
        get
        {
            return _hate;
        }
    }

    private int _servers;
    public int Servers
    {
        get
        {
            return _servers;
        }
    }

    private int _costs;
    public int Costs
    {
        get
        {
            _costs = (int)((Servers + 0.1) * (Hate + blocking) * 55);
            return _costs;
        }
    }
    private int _totalPeople;

    public int TotalPeople
    {
        get
        {
            return _totalPeople;
        }
    }


    private int _serverPrice = 10000;
    public int ServerPrice
    {
        get
        {
            return _serverPrice;
        }
    }

    // коэффициент блока серверов
    double blocking = 1;

    //нагрузка на сервера
    public double Adv_income_per_user = 0;

    public int marja;

    public int delta_users;
    public int delta_money;
    public double delta_approval;
    public double delta_hate;
}

public partial class Region
{
    public Region(int totalPeople)
    {
        Random rnd = new Random();
        _totalPeople = totalPeople;

        _users = 50 + rnd.Next(100);
        _hate = rnd.Next(70) / 100 + 0.3;
        _approval = rnd.Next(75) / 100 + 0.35;
        //if (rnd.Next(100) % 9 == 0)
        //{
        //    _servers = 1;
        //}
        //else
        //{
        //    _servers = 0;
        //}

    }
    // add new servers
    public void AddServer()
    {
        if (Game.com.Budjet < 10000)
            return;
        _servers += 1;
        _costs += ServerPrice;
    }
    // set advetisment
    public void SetAdv(double price)
    {
        Adv_income_per_user = price;
    }
    public int Hype_price
    {
        get { return (int)((Users * Hate) / 100); }
    }
    public void Hype()
    {
        if (Hype_price > Game.com.Budjet)
        {
            return;
        }
        Game.com.Budjet -= Hype_price;
        _hate *= 0.9;
        _approval *= 1.1;
    }

    public int Copacity = 1000;
    public void Update()
    {

        System.Random rnd = new System.Random();
        double ips = 1 / (((_servers * Copacity + 250) * Game.com.Tech_index) / (blocking * (Users + 1)));
        double income_approval = (Sygm(Game.Tech_index - 4) + 1 - (Sygm((2 * Adv_income_per_user - 4.5)))) / 10 * (ips >= 1 ? 0.2 : 1.2);
        double income_hate = (1 + Sygm(Game.Tech_index - 2)) / 20 * (ips >= 1 ? 0.9 : 1.1);
        delta_hate = income_hate;
        delta_approval = income_approval;
        delta_users = ips <= 1 ?
            (int)(((_approval) * (_approval) - Hate + 1) * (1 + Math.Exp(-Users / 250))) :
            (int)(-ips * Users / 1000);


        _users += delta_users;
        if (_users < 0)
        {
            _users = 0;
        }
        _hate += delta_hate;
        _approval += delta_approval;
        delta_money = (int)((_users) * Adv_income_per_user - marja);
        marja = (int)((_users) * Adv_income_per_user - Costs);

    }
    public void PlusApproval()
    {
        _approval *= 1.2;
    }

    public void PlusHate()
    {
        _hate *= 1.2;
    }
}
