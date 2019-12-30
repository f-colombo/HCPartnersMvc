using System.Collections;
using System;

namespace HCPartnersMvc.Models
{
    // public class HomeViewModel
    // {
    //     public IEnumerable<Registro> Registros { get; set; }
    // }

    public class Registro
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Razon { get; set; }
        public bool Aplicale { get; set; }
    }

}