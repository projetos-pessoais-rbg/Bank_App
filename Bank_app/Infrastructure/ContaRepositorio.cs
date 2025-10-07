using Bank_app.Console;
using Bank_app.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank_app.Infrastructure
{
    public class ContaRepositorio
    {
        private static Dictionary<int, Account> _contas;

        static ContaRepositorio()
        {
            _contas = new Dictionary<int, Account>
            {
                { 1, new Account { Id = 1, Nome = "João Silva", Saldo = 500m } },
                { 2, new Account { Id = 2, Nome = "Maria Oliveira", Saldo = 100m } },
                { 3, new Account { Id = 3, Nome = "Pedro Souza", Saldo = 300m } }

            };
        }

        
        public static Account BuscarContaPorId(int id)
        {
            _contas.TryGetValue(id, out Account conta);
            return conta;
        }

        public static Account BuscarContaPorNome(string nome)
        {
           
            if (string.IsNullOrWhiteSpace(nome))
            {
                return null;
            }
  
            Account contaEncontrada = _contas.Values.FirstOrDefault(conta => string.Equals(conta.Nome, nome, StringComparison.OrdinalIgnoreCase));
            return contaEncontrada;
        }
            

        public static void ListarContas()
        {
            foreach (var conta in _contas.Values)
            {
                System.Console.WriteLine($"ID: {conta.Id}, Proprietário: {conta.Nome}, Saldo: R$ {conta.Saldo:F2}");
            }
        }

        public static void AdicionarConta(int id, string nome, decimal saldo)
        {
            
            var conta = new Account { Id = id, Nome = nome, Saldo = saldo };
            _contas.Add(conta.Id, conta);

        }






    }
}
