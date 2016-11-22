using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Encryption
{
    class Program
    {
       
        const string CharKeys = "qwertyuiopasdfghjklçzxcvbnmQWERTYUIOPÇLKJHGFDSAZXCVBNM1234567890"; // variavel constante para a geração da variável de caracteres
        public static string[] Line = new string[191]; // vetor contendo todas as linhas da Key

        /*  static void KeyFile()
          {
              using (StreamReader KeyFile = new StreamReader("KeyShort.txt"))
              {
                  string keyfiltrer;

                  for (int i = 0; i < 191; i++)
                  {

                      keyfiltrer = KeyFile.ReadLine();
                      keyfiltrer = @"\u" + keyfiltrer.Substring(2, 5);
                      Line[i] = keyfiltrer;
                      Console.WriteLine(Line[i]);
                  }

                  KeyFile.Close();
              }
          } */

        /*   static void KeyShort()
          {
              using (StreamWriter KeyShort = new StreamWriter("KeyShort.txt"))
              {
                  for (int i = 0; i < 191; i++)
                  {
                      KeyShort.WriteLine(Line[i]);
                  }

                  KeyShort.Close();
              }

          } */

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

            using (StreamWriter GenerationFileKey = new StreamWriter("KeyFileCodes.txt")) // escreve as informações do vetor em um novo arquivo
            {
               for(int i = 0; i < 191; i++) // enquanto não pegar todas os indices do vetor
                {
                    GenerationFileKey.WriteLine(Line[i]); //Escreve no arquivo a informação contida em Line[i]
                }
                GenerationFileKey.Close(); // fecha arquivo
            }
        }

        static string Encryption(string msg) // Encriptação "DEMOREI PRA CARAI"
        {
            string Enc = " "; 
            int i = 0, x=0;
            string character, msgENC="";

            while (x != msg.Length ) { // enquanto não pegar todos os caracteres da mensagem

                Enc = msg.Substring(i, 1); // variável Enc recebera o 1 caracter da mensagem

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
                i = 0;
                x++;
            }
            return msgENC;
        }

        static void Main(string[] args)
        {
            string msg="";
                                        
            KeyGene();
            Console.Write("Digite uma mensagem:");
            msg = Console.ReadLine();
            Console.WriteLine("Mensagem Encriptografada: \n\n" + Encryption(msg));
           
            Console.ReadKey();
        }
    }
}
