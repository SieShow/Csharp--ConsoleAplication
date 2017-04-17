using System;
using System.IO;
namespace Encryption
{
    class Program
    {
       
        const string CharKeys = "qwertyuiopasdfghjklçzxcvbnmQWERTYUIOPÇLKJHGFDSAZXCVBNM1234567890"; // variavel constante para a geração da variável de caracteres
        public static string[] Line = new string[191]; // vetor contendo todas as linhas da Key

        static void KeyGene() //geração do arquivo Key
        {
            
            Random Character = new Random(); 
            int KeyPL; //tamanho da variável KeyP
            string KeyPSub=""; //password da cada unicode /Cada substring da variável
            
            using (StreamReader KeyFile = new StreamReader("KeyShort.txt")) // leitura do arquivo Key
            {
                for(int i = 0; i < 191; i++) 
                {
                    
                    Line[i] = KeyFile.ReadLine(); // leitura da linha, armazenando em Line[i]
                    Line[i] = Line[i].Substring(2, Line[i].Length-2); // Line[i] recebera o valor de Line começando da segunda letra até a antepenultima
                    KeyPL = Character.Next(5, CharKeys.Length); //Definirá qual será o tamanho do comprimento da variável 

                    for (int x = 1; x <= KeyPL; x++) // for para preencher cada espaço de KeyPL
                    {
                        KeyPL = Character.Next(10, CharKeys.Length); // começara a preencher apartir do 10º caracter
                        KeyPSub = CharKeys.Substring(KeyPL, 1); //O valor de cada caracter será um aleatório da variável CharKey, pegando apenas 1 caracter dela
                        Line[i] += KeyPSub;  // soma cada caracter à variável Line[i] e vai acumulando até chegar no seu tamanho definido
                    }
                    Console.WriteLine(Line[i]);
                }
                KeyFile.Close(); // fecha arquvio
            }

            using (StreamWriter GenerationFileKey = new StreamWriter("KeyFileCodes")) // escreve as informações do vetor em um novo arquivo
            {
               for(int i = 0; i < 191; i++) // enquanto não pegar todas os indices do vetor
                {
                    GenerationFileKey.WriteLine(Line[i]); //Escreve no arquivo a informação contida em Line[i]
                }
                GenerationFileKey.Close(); // fecha arquivo
            }
        }

        static string Encryption(string msg, out string EncryLocal) // Encriptação "DEMOREI PRA CARAI"
        {
            string Enc = " "; 
            int i = 0, x=0, y=0;
            string character, msgENC="";

           

            while (x != msg.Length ) { // enquanto não pegar todos os caracteres da mensagem

                Enc = msg.Substring(y, 1); // variável Enc recebera o 1 caracter da mensagem
                y++;
                i = 0;
                do
                {
                    character = Line[i].Substring(0, 4); // no arquivo Key, character recebe o primeiro valor unicode para comparar ao caracter da mensagem
                    int code = int.Parse(character, System.Globalization.NumberStyles.HexNumber); // converte o valor unicode para Hex
                    character = char.ConvertFromUtf32(code); // converte o valor hex para UTF-32 e character recebe esse valor

                    if (Enc == character) // 
                    {
                        msgENC += Line[i].Substring(5, Line[i].Length - 5);
                    }
                    i++;
                    
                } while (Enc != character);
               
                x++;
               
            }
                StreamWriter Encrypted = new StreamWriter("Encriptado.txt");
                Encrypted.Write(msgENC);
                Encrypted.Close();
                EncryLocal = Path.GetFullPath("Encriptado.txt");
            return msgENC;
            
        }

        static void LerChave()
        {
            StreamReader File = new StreamReader("KeyFileCodes" + ".txt");
            
            for(int i = 0; i < 191; i++)
            {
                Line[i] = File.ReadLine();
            }
            File.Close();
        }
        
        static string Decription(string AqrEnc)
        {
           using(StreamReader Decrip = new StreamReader(AqrEnc + ".txt"))
            {
                string All = Decrip.ReadToEnd();
                int test = 5;
                string Converted="", resp = "";
                while(All != "")
                {
                    int i = 0;

                    string get = All.Substring(0, test);

                    while(get != Line[i].Substring(5, get.Length))
                    {
                        i++;
                    }
                        if (get == Line[i].Substring(5, get.Length))
                        {
                            Converted = Line[i].Substring(0, 4);
                            int code = int.Parse(Converted, System.Globalization.NumberStyles.HexNumber);
                            Converted = char.ConvertFromUtf32(code);
                            resp += Converted;

                            string size = Line[i].Substring(5, Line[i].Length - 5);
                            All = All.Substring(size.Length, All.Length - size.Length);

                        }                                      
                }
                Decrip.Close();
                return resp;
            }
        }

        static void Main(string[] args)
        {
            string msg="", ArqEnc, EncryLocal;
            int opcao=0;
            Console.Clear();
            LerChave();
            Console.WriteLine("1)Encriptografar arquivo");
            Console.WriteLine("2)Decriptografar arquivo");
            Console.WriteLine("3)Exibir chave");
            Console.WriteLine("4)Gerar Key");
            Console.WriteLine("5)Sair");
            Console.Write("\nEscolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    {
                        string op;
                        int opconf = 0;
                        do
                        {
                            Console.Clear();
                            Console.Write("Deseja imprimir a Chave?(S/n): ");
                            op = Console.ReadLine();
                            if (op == "s" || op == "S" || op == "sim" || op == "Sim")
                            {
                                opconf = 1;
                                for (int i = 0; i < Line.Length; i++)
                                {
                                    Console.WriteLine(Line[i]);
                                }
                            }
                            else if (op == "n" || op == "não" || op == "Não" || op == "NÃO" || op == "nao")
                            {
                                opconf = 2;
                                LerChave();
                               
                            }
                        } while (opconf == 0 );

                        Console.Write("Digite uma mensagem:");
                        msg = Console.ReadLine();
                        Console.WriteLine("\nMensagem Encriptografada: \n\n\n" + Encryption(msg, out EncryLocal));
                        Console.WriteLine("\nCriada em ->" + EncryLocal);
                    }
                    break;
                case 2:
                    {
                        Console.Write("Digite o nome do arquivo codificado:");
                        ArqEnc = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write(Decription(ArqEnc) + "\n\n");
                       
                    }
                    break;
                case 3:
                    {
                        for (int i = 0; i < Line.Length; i++)
                        {
                            Console.WriteLine(Line[i]);
                        }
                    }
                    break;
                case 4:
                    {
                        KeyGene();
                        Console.Write("Key criada com sucesso.");
                    }
                    break;
                case 5:
                    {
                        return;
                    }
                 
            }
            Console.Write("Aperte qualquer tecla para continuar...");
            Console.ReadKey(false);
            Console.Clear();
            Main(args);
            Console.ReadKey();
        }
    }
}
