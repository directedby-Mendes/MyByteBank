using System.Reflection.Metadata.Ecma335;
namespace MyByteBank {

    public class Program {


        static void ShowMenu() {
            Console.Clear();
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa\n\n");
            Console.Write("Digite a opção desejada: ");
        }

        static void Manipular() {
            Console.Clear();
            Console.WriteLine("1 - Depositar saldo");
            Console.WriteLine("2 - Sacar saldo");
            Console.WriteLine("3 - Transferir Saldo");
            Console.WriteLine("0 - Para sair do programa\n\n");
            Console.Write("Digite a opção desejada: ");
        }

        //ESCONDE A SENHA DO USUARIO
        public static string HidePass() {
            var pass = string.Empty;
            ConsoleKey key;
            do {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0) {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar)) {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        public static void Main(string[] args) {

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();


            //REGISTRARNOVOUSUARIO
            static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
                Console.Write("Digite o cpf: ");
                cpfs.Add(Console.ReadLine());
                Console.Write("Digite o nome: ");
                titulares.Add(Console.ReadLine());
                Console.Write("Digite a senha: ");
                senhas.Add(Console.ReadLine());
                saldos.Add(0);
            }

            //APRESENTAR USUARIO
            static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
                Console.Write("Digite o cpf: ");
                string cpfParaApresentar = Console.ReadLine();
                int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                if (indexParaApresentar == -1) {
                    Console.WriteLine("Não foi possível apresentar esta Conta");
                    Console.WriteLine("MOTIVO: Conta não encontrada.");
                }

                ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
                Console.ReadKey();
            }

            //APRESENTAR CONTA
            static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos) {
                Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
                Console.ReadKey();
            }


            //DELETAR USUARIOS
            static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
                Console.Write("Digite o cpf: ");
                string cpfParaDeletar = Console.ReadLine();
                int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

                if (indexParaDeletar == -1) {
                    Console.WriteLine("Não foi possível deletar esta Conta");
                    Console.WriteLine("MOTIVO: Conta não encontrada.");
                }
                cpfs.Remove(cpfParaDeletar);
                titulares.RemoveAt(indexParaDeletar);
                senhas.RemoveAt(indexParaDeletar);
                saldos.RemoveAt(indexParaDeletar);
                Console.WriteLine("Conta deletada com sucesso");

            }

            //LISTAR TODAS AS CONTAS
            static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos) {
                for (int i = 0; i < cpfs.Count; i++) {
                    Console.WriteLine("Listagem de Contas\n");
                    Console.WriteLine($"CPF: {cpfs[i]} | Titulares: {titulares[i]} | Saldo: R$ {saldos[i]:F2}");
                }

                Console.ReadKey();
            }

            //APRESENTAR VALOR ACUMULADO
            static void ApresentarValorAcumulado(List<double> saldos) {
                Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
                // saldos.Sum(); ou .Agregatte(0.0, (x, y) => x + y)
                Console.ReadKey();
            }
            //QUANTIA ARMAZENADA
            static void QuantiaArmazenada(List<string> cpfs, List<string> titulares, List<double> saldos) {
                Console.WriteLine();
                ApresentarValorAcumulado(saldos);
                Console.WriteLine("Pressione Enter para continuar.");
                Console.ReadKey();
            }

            //DEPOSITAR NA CONTA

            static void DepositarNaConta(List<string> cpfs, List<string> titulares, List<double> saldos) {
                Console.Write("\nDigite o cpf: ");
                string cpfParaApresentar = Console.ReadLine();
                Console.WriteLine("\n ");
                int indexParaConsultar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                if (indexParaConsultar == -1) {
                    Console.WriteLine("Não foi possível encontrar esta conta");
                    Console.WriteLine("Confira o CPF e tente novamente.");
                }
                else {
                    Console.Write("Digite a quantida do deposito: R$ ");
                    saldos[indexParaConsultar] += double.Parse(Console.ReadLine());
                    Console.WriteLine("Depósito realizado.");
                    Console.WriteLine("Valor hoje na conta é de: ");
                    int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);
                    ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);                          
                    Console.WriteLine("Pressione Enter para continuar.");
                    Console.ReadKey();
                }
            }

            //SACAR NA CONTA
            static void SacarNaConta(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
                
                Console.Write("\nDigite o cpf: ");
                string cpfParaApresentar = Console.ReadLine();
                int indexParaConsultar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                if (indexParaConsultar == -1) {
                    Console.WriteLine("Não foi possível encontrar esta conta");
                    Console.WriteLine("Confira o CPF e tente novamente.");
                }
                else {
                    Console.Write("\nDigite sua senha: ");
                    string senhaParaApresentar = HidePass();
                    int indexSenhaConsultar = senhas.FindIndex(senha => senha == senhaParaApresentar);

                    if (indexSenhaConsultar == -1) {
                        Console.WriteLine("Não foi possível encontrar esta senha");
                        Console.WriteLine("Confira a Senha e tente novamente.");
                    }
                    else {
                        Console.Write("\nDigite a quantia do Saque: R$ ");
                        double quantiaSaque = double.Parse(Console.ReadLine());
                        double quantiaSaldoAtualizado = saldos[indexSenhaConsultar] - quantiaSaque;

                        if (quantiaSaldoAtualizado >= 0) {
                            Console.WriteLine($"Valor do saldo atualizado: R$ {quantiaSaldoAtualizado}");
                            saldos[indexParaConsultar] = quantiaSaldoAtualizado;
                            Console.WriteLine("Saque realizado.");
                        }
                        else {
                            Console.WriteLine($"Valor do saldo atualizado: R$ {quantiaSaldoAtualizado}");
                            Console.WriteLine("Saldo insuficiente");
                            Console.WriteLine($"Seu saldo atual: R$ {saldos[indexParaConsultar]:f2}.");
                        }
                        Console.WriteLine("Pressione Enter para continuar.");
                        Console.ReadKey();
                    }
                }
            }

            int option;
            do {

                ShowMenu();
                option = int.Parse(Console.ReadLine());

                switch (option) {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Operação Finalizada");
                        break;
                    case 1:
                        Console.Clear();
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        Console.Clear();
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        Console.Clear();
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        Console.Clear();
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        Console.Clear();
                        ApresentarValorAcumulado(saldos);
                        break;
                    case 6:
                        Console.Clear();
                        Manipular();
                        int option2;
                       /* Console.Write("\nDigite sua senha para fazer Login neste terminal: ");
                        string senhaParaApresentar = HidePass();
                        int indexSenhaConsultar = senhas.FindIndex(senha => senha == senhaParaApresentar);
                        */
                        do {
                            option2 = int.Parse(Console.ReadLine());
                            Console.WriteLine("-----------------");

                            switch (option2) {
                                case 0:
                                    Console.WriteLine("Sair");
                                    break;
                                case 1:
                                    Console.WriteLine("Tela de Deposito");
                                    DepositarNaConta(cpfs, titulares, senhas, saldos);
                                    break;
                                case 2:
                                    Console.WriteLine("Tela de Saque");
                                    SacarNaConta(cpfs, titulares, senhas, saldos);

                                    break;
                                case 3:
                                    Console.WriteLine("Tela de Transferencia");
                                    //TransferirNaConta();
                                    break;
                            }

                        } while (option2 != 0);
                        break;
                }
            } while (option <6);
        }
    }         
}