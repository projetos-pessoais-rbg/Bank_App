using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_app.Domain
{
    public class Account
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }

        public decimal Debitar(decimal quantia)
        {

            Saldo -= quantia;
            return Saldo;
        }

        public void Depositar (decimal quantia)
        {
            Saldo += quantia;
        }

        public bool VerificarSaldo(decimal quantia)
        {
            if (Saldo < quantia)
            {
                return false;
            }

            return true;
        }

       

        


    }
}
