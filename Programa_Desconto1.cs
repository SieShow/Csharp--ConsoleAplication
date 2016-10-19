using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPresolusao
{
    class Program
    {
        static void Main(string[] args)
        {
            double pagar=0;
            float pessoas;
            string dia;
            //tabelinha com os valores e descontos
            Console.Write("\t\tDescontos");
            Console.Write("\nDia         \tNumero de pessoas  Desconto");
            Console.Write("\nFim de seman\tMenos de 100   \t    0%:");
            Console.Write("\nFim de semana\tMais de 100   \t    10%:");
            Console.Write("\nDia de semana\tMenos de 100   \t    30%:");
            Console.Write("\nDia de semana\tMais de 100   \t    50%:");
            //digite o dia da semana
            Console.Write("\n\nDigite o dia da semna: ");
            dia = Console.ReadLine();
            // se for digitado algum dia do final de semana o programa converte a variavel "dia" para "final de semana"
            if( dia == "sexta" || dia == "sabado" || dia == "domingo")
            {
                dia = "final de semana";
            } // se for digitado algum dia da semana ele converte "dia" para "dia de semana"
            else if(dia == "segunda" || dia == "terça" || dia == "quarta" || dia == "quinta")
            {
                dia = "dia de semana";
            }
               else // se for digita do algum valor que não é dia da semana nem final de semana 
             {// exibe mensagem de erro 
                  ConsoleColor cor = Console.ForegroundColor;
                  Console.ForegroundColor = ConsoleColor.Red;
                  Console.Write("DIGITE UM DIA VÁLIDO!!");
                  Console.ForegroundColor = cor;
                return;
        }
        //digite o número de pessoas
        Console.Write("Digite o número de pessoas:");
            pessoas = float.Parse(Console.ReadLine());

            switch (dia) //pega a variável dia
            {
                case "final de semana" : // se ela for final de semana
                        if(pessoas < 100) //menor que 100 pessoas
                    {
                        pagar = pessoas * 70; // sem desconto
                    }
                    else //se não
                    {
                        pagar = pessoas * 70 - ((pessoas * 70 * 10) / 100); //desconto 10%
                    }
                    break;
                case "dia de semana": // caso seja dia da semana
                    if( pessoas < 100) // menos de 100 pessoas
                    {
                        pagar = pessoas * 70 - ((pessoas * 70 * 30) / 100); // 30% de desconto
                    }
                    else // A formula de pagar é feita com o valor total de pessoas menos o desconto
                    {
                        pagar = pessoas * 70 - ((pessoas * 70 * 50) / 100); // 50% de desconto
                    }
                    break;        
            }
 
            Console.Write("\n\nO valor a ser pago é de: R${0:n2}", pagar);
            Console.ReadKey();
            return; // fecha o programa
        }
    }
}
