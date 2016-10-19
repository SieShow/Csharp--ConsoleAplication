using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ATP_Resolução
{
    class Program
    {
        static void NullPrepair(ref string linha) // se, no bloco de notas, você segurar espaço, mas deixar a linha em branco. o sistema reconhecerá tal linha como por exemplo "              ".
        {
            if (linha.Length >= 1) // se o tamanho da linha for maior que 1.
            {
                if (linha.Substring(0, 1) == " ") // e se o primeiro caracter dela for " "
                {
                    string[] vetor = new string[linha.Length]; // crio um vetor referente ao tamanho da variavel
                    string totalvalor = "";

                    for (int i = 0; i < linha.Length; i++) //o for será executado até chegar no fim da variável
                    {
                        int x = 1;

                        vetor[i] = linha.Substring(0, x); // o indice 0 recebe o valor equivalente ao primeiro caracter da variável linha

                        if (vetor[i] == " ") // se esse valor for igual a " "
                        {
                            vetor[i] = ""; // ele será transformado em "", ou seja, vazio

                            totalvalor += vetor[i]; //interessante brincadeira com string. o valor total da string será a soma dela com o vetor. ou seja "" + "" + "" = "" :)
                        }
                        x++;
                    }
                    linha = totalvalor; // a variável linha recebe o valor da variável totalvalor.
                }
            }
        }

        static void Main(string[] args)
        {
            string linha = ""; 
            Random repor = new Random();
            Console.Write("Digite o nome do arquivo: ");
            string arquivo = Console.ReadLine();

              using(StreamReader BD = new StreamReader(@arquivo + ".txt"))
            {
                while((linha = BD.ReadLine()) != null)
                {
                    NullPrepair(ref linha);

                    if (linha == "")
                    {                    
                        linha = " " + repor.Next(100, 999);
                    }
             
                    Console.WriteLine(linha);
                }
                BD.Close();
            }  

            Console.ReadKey();
            }
    }
}
