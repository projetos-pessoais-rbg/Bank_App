using Bank_app.Domain;
using Bank_app.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bank_app.Console
{
    internal class VerificacoesEntrada
    {
        public string ObterNome()
        {
            const string padraoNome = @"^[\p{L}\s'-]+$";
            string nomeInput;
            bool nomeJaExiste;
            Account contaEncontrada;
            do
            {
                System.Console.WriteLine("Digite o nome completo para a nova conta: ");
                nomeInput = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nomeInput) || !Regex.IsMatch(nomeInput, padraoNome))
                {
                    if (string.IsNullOrWhiteSpace(nomeInput))
                    {
                        System.Console.WriteLine("O nome não pode ser vazio.");
                    }
                    else
                    {
                        System.Console.WriteLine("O nome possui caracteres inválidos (apenas letras, espaços, hífens ou apóstrofos são permitidos).");
                    }
                    nomeJaExiste = true;
                    continue; 
                }

                contaEncontrada = ContaRepositorio.BuscarContaPorNome(nomeInput);

                nomeJaExiste = (contaEncontrada != null);

                if (nomeJaExiste)
                {
                    System.Console.WriteLine($"O nome '{nomeInput}' já está em uso. Digite um nome diferente.");
                }
            } while (nomeJaExiste);
            return nomeInput;
        }

        public decimal VerificarQuantia()
        {
            System.Console.WriteLine("Digite a quantia da operação: ");
            decimal quantiaInput = System.Convert.ToDecimal(System.Console.ReadLine());
            while (quantiaInput <=0)
            {
                System.Console.Write("Digite um valor: ");
                quantiaInput = System.Convert.ToDecimal(System.Console.ReadLine());
            }
            return quantiaInput;
        }


        public int VerificarId()
        {
            System.Console.WriteLine("Digite o ID da conta: ");
            var idInput = System.Console.ReadLine();
            int valor;
            while (!int.TryParse(idInput, out valor) || valor <= 0)
            {
                System.Console.Write("ID inválido. Digite um ID numérico > 0: ");
                idInput = System.Console.ReadLine();
            }
            return valor;
        }

        public Account VerificarContaExiste(int id)
        {
            var conta = ContaRepositorio.BuscarContaPorId(id);

            while (conta == null)
            {
                System.Console.Write("Conta não encontrada. Informe outro ID: ");
                int contaNova = System.Convert.ToInt32(System.Console.ReadLine());
                conta = ContaRepositorio.BuscarContaPorId(contaNova);
                
            }

            return conta;
        }

        public int VerificarNovoIdUnico()
        {
            int id;
            bool idExiste;

            do
            {
                id = VerificarId();

                idExiste = (ContaRepositorio.BuscarContaPorId(id) != null);

                if (idExiste)
                {
                    System.Console.WriteLine($"O ID {id} já está em uso. Digite um ID que não exista.");
                }
            } while (idExiste);

            return id;
        }


    }
}
