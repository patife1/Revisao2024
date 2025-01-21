using System;
using System.IO;
using System.Text;

class GeradorDeSenhas
{
    static void Main()
    {
        Console.WriteLine("Gerador de Senhas 2.0");

        // vai executar ne
        ExecutarPrograma();
    }

    static void ExecutarPrograma()
    {
        while (true)
        {
            Console.Clear();  
            int tamanhoSenha = 0;
            while (tamanhoSenha <= 0)
            {
                Console.WriteLine("Informe o tamanho da senha:");
                if (!int.TryParse(Console.ReadLine(), out tamanhoSenha) || tamanhoSenha <= 0)
                {
                    Console.WriteLine("Por favor, insira um número válido maior que 0.");
                    tamanhoSenha = 0; // reinicia o trem
                }
            }

            
            bool incluirSimbolos = PerguntarSimNao("Deseja incluir caracteres especiais (ex: @, !, #, -)? (s/n)");

            
            bool incluirLetras = PerguntarSimNao("Deseja incluir números e letras? (s para sim, n para números apenas)");

           
            string senhaGerada = GerarSenha(tamanhoSenha, incluirSimbolos, incluirLetras);

            Console.WriteLine($"Senha Gerada: {senhaGerada}");

            // salva a senha no arquivo do trem
            SalvarSenhaNoBackup(senhaGerada);

            if (PerguntarSimNao("Deseja recuperar as senhas geradas anteriormente? (s/n)"))
            {
                RecuperarSenhasDoBackup();
            }

            
            if (!PerguntarSimNao("Deseja gerar outra senha? (s/n)"))
            {
                
                if (PerguntarSimNao("Deseja recomeçar o programa? (s/n)"))
                {
                    ExecutarPrograma(); // reinicia o programa chamando a funçao dnv
                    break; // break aq so sai do loop atual, mas o programa reinicia
                }
                else
                {
                    Console.WriteLine("Encerrando o programa. Pressione qualquer tecla para sair...");
                    Console.ReadKey();
                    break; // encerra o trem
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
                Console.WriteLine("Resposta inválida! Por favor, digite 's' para sim ou 'n' para não.");
            }
        }
    }

    // Função para gerar a senha com base nas escolhas do usuário
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

    // Função para salvar a senha no arquivo de backup
    static void SalvarSenhaNoBackup(string senha)
    {
        string caminhoArquivo = "bkp.TXT";
        
        // Salva a senha gerada no arquivo de backup
        File.AppendAllText(caminhoArquivo, senha + Environment.NewLine);
        Console.WriteLine("Senha salva no arquivo bkp.TXT");
    }

    // Função para recuperar senhas do arquivo de backup
    static void RecuperarSenhasDoBackup()
    {
        string caminhoArquivo = "bkp.TXT";

        if (File.Exists(caminhoArquivo))
        {
            Console.WriteLine("Senhas geradas previamente:");
            string[] senhas = File.ReadAllLines(caminhoArquivo);

            foreach (var senha in senhas)
            {
                Console.WriteLine(senha);
            }
        }
        else
        {
            Console.WriteLine("Nenhuma senha gerada previamente encontrada.");
        }
    }
}
