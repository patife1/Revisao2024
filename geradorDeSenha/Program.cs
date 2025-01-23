using System;
using System.IO;
using System.Text;

class GeradorDeSenhas
{
    static void Main()
    {
        
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("=======================================");
        Console.WriteLine("       Gerador de Senhas 2.0");
        Console.WriteLine("=======================================");
        Console.ResetColor();

        // Vai executar
        ExecutarPrograma();
    }

    static void ExecutarPrograma()
    {
        while (true)
        {
            Console.WriteLine();  // p dar espaço
            Console.WriteLine();

            
            AnimacaoCarregando();

            int tamanhoSenha = 0;
            while (tamanhoSenha <= 0)
            {
                Console.WriteLine("Informe o tamanho da senha:");
                if (!int.TryParse(Console.ReadLine(), out tamanhoSenha) || tamanhoSenha <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Por favor, insira um número válido maior que 0.");
                    Console.ResetColor();
                    tamanhoSenha = 0; // reinicia o processo
                }
            }

            bool incluirSimbolos = PerguntarSimNao("Deseja incluir caracteres especiais (ex: @, !, #, -)? (s/n)");
            bool incluirLetras = PerguntarSimNao("Deseja incluir números e letras? (s para sim, n para números apenas)");

            string senhaGerada = GerarSenha(tamanhoSenha, incluirSimbolos, incluirLetras);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Senha Gerada: {senhaGerada}");
            Console.ResetColor();


            SalvarSenhaNoBackup(senhaGerada);

            if (PerguntarSimNao("Deseja recuperar as senhas geradas anteriormente? (s/n)"))
            {
                RecuperarSenhasDoBackup();
            }

            if (!PerguntarSimNao("Deseja gerar outra senha? (s/n)"))
            {
                if (PerguntarSimNao("Deseja recomeçar o programa? (s/n)"))
                {
                    ExecutarPrograma(); // reinicia o troço
                    break; // sai do loop de agr
                }
                else
                {
                    Console.WriteLine("Encerrando o programa. Pressione qualquer tecla para sair...");
                    Console.ReadKey();
                    break; // encerra o programa ne
                }
            }
        }
    }

    static bool PerguntarSimNao(string pergunta)
    {
        while (true)
        {
            Console.WriteLine(pergunta);
            string resposta = Console.ReadLine().ToLower();
            if (resposta == "s")
            {
                return true;
            }
            else if (resposta == "n")
            {
                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Resposta inválida! Por favor, digite 's' para sim ou 'n' para não.");
                Console.ResetColor();
            }
        }
    }

    // funçao para gerar a senha com base nas escolhas do caba
    static string GerarSenha(int tamanho, bool incluirSimbolos, bool incluirLetras)
    {
        const string caracteresNumeros = "0123456789";
        const string caracteresLetras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        const string caracteresSimbolos = "@!#$%&*()-_+=[]{}|;:,.<>?/";

        StringBuilder caracteresDisponiveis = new StringBuilder(caracteresNumeros);

        if (incluirLetras)
        {
            caracteresDisponiveis.Append(caracteresLetras);
        }

        if (incluirSimbolos)
        {
            caracteresDisponiveis.Append(caracteresSimbolos);
        }

        Random rand = new Random();
        char[] senha = new char[tamanho];

        for (int i = 0; i < tamanho; i++)
        {
            senha[i] = caracteresDisponiveis[rand.Next(caracteresDisponiveis.Length)];
        }

        return new string(senha);
    }

  
    static void SalvarSenhaNoBackup(string senha)
    {
        string caminhoArquivo = "bkp.TXT";
        
       
        File.AppendAllText(caminhoArquivo, senha + Environment.NewLine);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Senha salva no arquivo bkp.TXT");
        Console.ResetColor();
    }

    static void RecuperarSenhasDoBackup()
    {
        string caminhoArquivo = "bkp.TXT";

        if (File.Exists(caminhoArquivo))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Senhas geradas previamente:");
            string[] senhas = File.ReadAllLines(caminhoArquivo);

            foreach (var senha in senhas)
            {
                Console.WriteLine(senha);
            }
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Nenhuma senha gerada previamente encontrada.");
            Console.ResetColor();
        }
    }

    // Função para simular a animação de carregamento
    static void AnimacaoCarregando()
    {
        Console.Write("Carregando");
        for (int i = 0; i < 3; i++)  
        {
            System.Threading.Thread.Sleep(500); 
            Console.Write(".");
        }
        Console.WriteLine();
    }
}
