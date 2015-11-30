using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IHFF_Websystem.Models
{
    public class DbMedewerkerRepository : IMedewerkerRepository
    {
       public IHFFContext ctx = new IHFFContext();

       public void AddMedewerker(Medewerker medewerker)
       {
           ctx.Medewerkers.Add(medewerker);
           ctx.SaveChanges();
       }
    }
}