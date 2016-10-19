using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
           int tloja, tproduto, preco, qproduto, total=0, totalprod=0, totalp1=0, totalp2=0, totalp3=0, totalcompra=0, valdes=0;
            string resp="";

            Console.WriteLine(" Tipo de loja ");
            Console.WriteLine("1) Loja física");
            Console.WriteLine("2) Venda on-line");

            Console.Write("Digite o tipo da loja: ");
            tloja = int.Parse(Console.ReadLine());

            Console.Clear();
            Console.WriteLine(" Tipo de produto ");
            Console.WriteLine("1) Estar e jantar");
            Console.WriteLine("2) Escritório");
            Console.WriteLine("3) Quarto");

            do
            {
                Console.Write("Digite o tipo do produto: "); 
                tproduto = int.Parse(Console.ReadLine());

                Console.Write("Digite o preço deste: ");
                preco = int.Parse(Console.ReadLine());

                Console.Write("Digite a quantidade: "); 
                qproduto = int.Parse(Console.ReadLine());

                total = preco * qproduto; //total da venda do produto atual
                totalprod += qproduto; // total de produtos comprados no geral
                totalcompra += total; // valor total da compra
                switch (tproduto)
                {
                    case 1:
                        total -= total * 5 / 100; //calcula o desconto pelo produto
                        totalp1 += qproduto; // total de produto por categoria 1
                        break;
                    case 2:
                        total -= total * 10 / 100;
                        totalp2 += qproduto; // total de produto por categoria 2
                        break;
                    case 3:
                        total -= total * 7 / 100;
                        totalp3 += qproduto; // total de produto por categoria 3
                        break;
                }
                Console.Write("Deseja comprar mais produtos?(sim/nao): ");
                resp = Console.ReadLine();

                Console.Clear();

            } while (resp == "sim");

            Console.Clear();           

            //desconto sobre loja
            if (tloja == 1) total -= total * 10 / 100;
            else if (tloja == 2) total -= total * 30 / 100;

            valdes = totalcompra - total;

            Console.WriteLine("     Resultado    ");
            Console.WriteLine("Número total de produtos comprados: "+ totalprod);
            Console.WriteLine("Número total de produtos comprados por categora: " + tproduto);
            Console.WriteLine("Estar e jantar: " + totalp1);
            Console.WriteLine("Escritório: " + totalp2);
            Console.WriteLine("Quarto: " + totalp3);
            Console.WriteLine("Valor total sem desconto: R${0:n2}",totalcompra);
            Console.WriteLine("Valor total dos descontos dados: R${0:n2} ", valdes);
            Console.WriteLine("Valor total da compra com desconto: R${0:n2}", total);
            Console.ReadKey();



        }
    }
}
