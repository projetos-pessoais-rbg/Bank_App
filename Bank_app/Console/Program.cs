
using Figgle;
using System;
using System.Text;
using System.Threading.Tasks;
using Bank_app.Domain;
using Bank_app.Infrastructure;

namespace Bank_app.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("OLÁ! BEM VINDO AO BANCO");
            System.Console.WriteLine();
            System.Console.WriteLine(@"
                 CCCCC  EEEEE  SSSSS  H   H   AAAAA  RRRR   PPPP   M   M  OOOOO  N   N  EEEEE   Y   Y
                 C      E      S      H   H   A   A  R   R  P   P  MM MM  O   O  NN  N  E        Y Y
                 C      EEEE   SSSSS  HHHHH   AAAAA  RRRR   PPPP   M M M  O   O  N N N  EEEE      Y
                 C      E          S  H   H   A   A  R R    P      M   M  O   O  N  NN  E         Y
                 CCCCC  EEEEE  SSSSS  H   H   A   A  R  R   P      M   M  OOOOO  N   N  EEEEE     Y
                ");



            await Task.Delay(3000);

            new Program().GetMenu();
        }

        

        public void GetMenu()
        {
            string? option;
            do
            {
                System.Console.Clear();


                System.Console.WriteLine("╔══════════════════════════════════╗");
                System.Console.WriteLine("║        SISTEMA BANCÁRIO          ║");
                System.Console.WriteLine("╚══════════════════════════════════╝");
                System.Console.WriteLine("║                                  ║");
                System.Console.WriteLine("║ Escolha uma opção:               ║");
                System.Console.WriteLine("║                                  ║");
                System.Console.WriteLine("║ 1. Adicionar Conta               ║");
                System.Console.WriteLine("║ 2. Realizar Depósito             ║");
                System.Console.WriteLine("║ 3. Realizar Saque                ║");
                System.Console.WriteLine("║ 4. Transferir Dinheiro           ║");
                System.Console.WriteLine("║ 5. Listar Contas                 ║");
                System.Console.WriteLine("║ 6. Sair                          ║");
                System.Console.WriteLine("║                                  ║");
                System.Console.WriteLine("╚══════════════════════════════════╝");

                System.Console.Write("\nDigite a sua opção: ");
                option = System.Console.ReadLine();
                var verificacoes = new VerificacoesEntrada();
                var contaRepositorio = new ContaRepositorio();
                var account = new Account();

                switch (option)
                {

                    case "1":
                        System.Console.WriteLine("\nAdicionar Conta selecionado.");
                        
                        var case1Nome = verificacoes.ObterNome();
                        var case1Id = verificacoes.VerificarNovoIdUnico();
                        decimal case1Saldo = 0;
                        ContaRepositorio.AdicionarConta(case1Id, case1Nome, case1Saldo);
                        System.Console.WriteLine("\nConta adicionada co sucesso.");
                        break;

                    case "2":
                        System.Console.WriteLine("Depositar selecionado.");

                        var case2Id = verificacoes.VerificarId();
                        var conta2Selecionada = verificacoes.VerificarContaExiste(case2Id); 
                        var case2Quantia = verificacoes.VerificarQuantia();
                        conta2Selecionada.Depositar(case2Quantia);
                        System.Console.WriteLine($"Depósito realizado. Novo saldo: R$ {conta2Selecionada.Saldo:F2}");
                        break;

                    case "3":
                        System.Console.WriteLine("\nRealizar Saque selecionado.");

                        var case3Id = verificacoes.VerificarId();
                        var conta3Selecionada = verificacoes.VerificarContaExiste(case3Id);
                        var case3Quantia = verificacoes.VerificarQuantia();
                        conta3Selecionada.Debitar(case3Quantia);
                        System.Console.WriteLine($"Saque realizado. Novo saldo: R$ {conta3Selecionada.Saldo:F2}");
                        break;

                    case "4":
                        System.Console.WriteLine("\nTransferir Dinheiro selecionado.");
                        System.Console.WriteLine("\nInformações da Conta de origem da Transferência:");
                        var case4IdOrigem = verificacoes.VerificarId();
                        var case4SelecionadaOrigem = verificacoes.VerificarContaExiste(case4IdOrigem);
                        decimal conta4Quantia = verificacoes.VerificarQuantia() ;
                        if(case4SelecionadaOrigem.VerificarSaldo(conta4Quantia) != true)
                        {
                            System.Console.WriteLine("Quantia não pode ser maior que saldo.");
                            break;
                        }
                        System.Console.WriteLine("\nInformações da Conta de destino da Transferência:");
                        var case4IdDestino = verificacoes.VerificarId();
                        var conta4SelecionadaDestino = verificacoes.VerificarContaExiste(case4IdDestino);

                        case4SelecionadaOrigem.Debitar(conta4Quantia);
                        conta4SelecionadaDestino.Depositar(conta4Quantia);
                        System.Console.WriteLine($"Transferencia feita com sucesso. Novo saldo da conta de Origem: R$ {case4SelecionadaOrigem.Saldo:F2} Novo saldo da conta de Destino: R$ {conta4SelecionadaDestino.Saldo:F2}");
                        break;

                    case "5":
                        System.Console.WriteLine("\nListar Contas selecionado.");
                        ContaRepositorio.ListarContas();
                        break;

                    case "6":
                        System.Console.WriteLine("\nSaindo do sistema...");
                        Environment.Exit(0);
                        break;

                    default:
                        System.Console.WriteLine("\nOpção inválida. Por favor, tente novamente.");
                        break;
                }

                // Pausa a execução para o usuário ver a mensagem antes de o menu ser limpo
                if (option != "6")
                {
                    System.Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    System.Console.ReadKey();
                }
            } while (option != "6");
            
        }


    }
}
