using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Ekz
{
    class Otobrazhenie
    { 
    public List<Service> service;
    public List<Clienti> clienti;
    public Otobrazhenie()
    {
        service = newserviceslist();
        clienti = DataBase.BaseModel.Clienti.ToList();
    }
    public List<Service> newserviceslist()
    {
        List<Service> services = new List<Service>();
        Service bufferservices;
        List<Service> bdservices = DataBase.BaseModel.Service.ToList();
        foreach (Service bds in bdservices)
        {
            bufferservices = new Service();
            bufferservices.id_service = bds.id_service;
            bufferservices.naimenovanie = bds.naimenovanie;
            bufferservices.izobrazhenie = bds.izobrazhenie;
            bufferservices.dlitelnost = bds.dlitelnost / 60;
            bufferservices.skidka = bds.skidka;
            bufferservices.stoimost = Math.Round(bds.stoimost, 2);
            if (bds.skidka > 0)
            {
                bufferservices.MyNewCost = Math.Round(bds.stoimost - Convert.ToDecimal(Convert.ToDouble(bds.stoimost) * bds.skidka), 2);
                bufferservices.MyVisiable = "Visible";
                bufferservices.MyLine = "Strikethrough";
                bufferservices.MyGreen = "#e7fabf";
            }
            else
            {
                bufferservices.MyVisiable = "Collapsed";
                bufferservices.MyLine = "none";
            }
            services.Add(bufferservices);
        }
        return services;
    }

    public List<ServiceClient> createSC()
    {
        List<ServiceClient> sc = DataBase.BaseModel.ServiceClient.ToList();
        foreach (Service s in service)
        {
            foreach (ServiceClient sc2 in sc)
            {
                if (sc2.service == s.id_service)
                {
                    sc2.FullNameService = s.naimenovanie;
                }
            }
        }

        foreach (Clienti cl in clienti)
        {
            foreach (ServiceClient sc2 in sc)
            {
                    if (sc2.client == cl.id_client)
                {
                    if (sc2.nachalo_okazania > DateTime.Now)
                    {
                        TimeSpan ts = sc2.nachalo_okazania - DateTime.Now;
                        sc2.TimeStart = ts.Days + ":" + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds;
                        if (ts.Days == 0 && ts.Hours <= 1)
                        {
                            sc2.colortime = "Red";
                        }

                    }
                    sc2.FullNameClient = cl.fullname;
                    sc2.PhoneEmail = cl.telefone + " " + cl.email;
                }
            }
        }
        List<ServiceClient> newSc = sc.Where(x => x.TimeStart != null).ToList();
        return newSc;
    }
}
}
