using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHFF_Websystem.Models
{
    interface IMedewerkerRepository
    {
        void AddMedewerker(Medewerker medewerker);
    }
}