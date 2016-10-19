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
        const int MAX = 2000;

        class Produtos
        {
           

           public static int[] cod = new int[MAX];
           public static string[] desc = new string[MAX];
           public static double[] preco = new double[MAX];

        }

        class Carrinho
        {
            public static int tamanho = 0;
            public static int[] codcar = new int[MAX];
            public static string[] desccar = new string[MAX];
            public static double[] precocar = new double[MAX];
            public static double[] quanticar = new double[MAX];

            public static void CarTam()
            {
                Console.WriteLine("   ___");
                Console.WriteLine(@" _/___\_ ==== " + tamanho);
                Console.WriteLine("|0     0| ");
            }

            public static void GerarNotaCarrinho(double total)
            {
                using ( StreamWriter txtcarro = new StreamWriter(@"Carrinho.txt", false))
                {
                    int lugar = 1;
                    txtcarro.WriteLine("Pos\tCódigo\tPreço\t\tQuantidade\tDescrição");
                    for (int i = 0; i < tamanho; i++)
                    {
                        txtcarro.WriteLine("{0}\t{1}\tR${2:n2}\t\t{3}\t{4}", lugar, codcar[i], precocar[i], quanticar[i], desccar[i]);
                        lugar++;
                    }

                    txtcarro.WriteLine("\t\tTotal: R${0:n2}", total);
                }

            }

        }

        static void Lerarquivo(string arquivo)
        {
            using (StreamReader BD = new StreamReader(@arquivo + ".txt"))
            {
                int i = 0;
                Console.WriteLine("Código \t Preço \t\t\t Descrição");
                while (!BD.EndOfStream)
                {
                    Produtos.cod[i] = int.Parse(BD.ReadLine());
                    Produtos.desc[i] = BD.ReadLine();
                    Produtos.preco[i] = double.Parse(BD.ReadLine());
                    Console.WriteLine(Produtos.cod[i] + "  \tR$" + Produtos.preco[i] + "\t" + Produtos.desc[i]);
                    i++;
                }
                BD.Close();
            }
        } 

        static void Verificar( ref int cod, ref int aux, ref double total, ref double subtotal, ref int quant)
        {
            string Go = "nao"; // essa variável define vai controlar se dps do for, será adicionado um produto no carrinho

            for (int i = 0; i < Carrinho.codcar.Length; i++) // verifica cada indice do vetor
            {
                if (Carrinho.codcar[i] == aux) // se o determinado indice, for igual ao codigo digitado pelo usuario
                {
                    Carrinho.quanticar[i] += quant; // esse indice tera a quantidade do produto somada.
                    total += subtotal;
                    Go = "sim"; 
                    break; 
                }
              
            }
            if (Go == "nao") //se o if do for for satisfeito, "Go" será convertido para sim. dessa forma, não será adicionado um novo produto no carrinho.
            {

                Carrinho.codcar[Carrinho.tamanho] = Produtos.cod[cod];
                Carrinho.desccar[Carrinho.tamanho] = Produtos.desc[cod];
                Carrinho.precocar[Carrinho.tamanho] = Produtos.preco[cod];
                Carrinho.quanticar[Carrinho.tamanho] = quant;
                total += subtotal;
                Carrinho.tamanho++;
            }
            
        } // método topper para ver se o cod digitado ja existe la no carrinho

        static void Main(string[] args)
        {
            int cod, quant, aux;
            double subtotal, total=0;
            var resp="";

            Console.WriteLine("\nBem vindo ao mercado do Tendeu!!");
            Console.Write("Digite o nome do arquivo dos dados: ");
            string arquivo = Console.ReadLine();

            Lerarquivo(arquivo);
           do
            {
                do
                {
                    Carrinho.CarTam();

                    Console.Write("\nDigite o código do produto: ");
                    cod = int.Parse(Console.ReadLine());
                    aux = cod;
                    cod = Array.IndexOf(Produtos.cod, cod);

                    Console.Write("Digite a quantidade: ");
                    quant = int.Parse(Console.ReadLine());

                    subtotal = quant * Produtos.preco[cod];

                    Console.WriteLine("{0}\t{1} \t R${2:n2} x {3} = R${4:n2}", Produtos.cod[cod], Produtos.desc[cod], Produtos.preco[cod], quant, subtotal);
                    Console.Write("Confirma produto(s/n)?");
                    resp = Console.ReadLine();

                    

                } while (resp == "n");


                Verificar(ref cod, ref aux, ref total, ref subtotal, ref quant);


                Console.Write("Deseja Adicionar mais produtos ao carrinho(s/n)?");
                resp = Console.ReadLine();

            } while (resp == "s");

            Console.Clear();

            int lugar = 1;
            Console.WriteLine("Pos\tCódigo\tPreço\t\tQuantidade\tDescrição");
            for (int i = 0; i < Carrinho.tamanho; i++)
            {       
                Console.WriteLine("{0}\t{1}\tR${2:n2}\t\t{3}\t{4}", lugar, Carrinho.codcar[i], Carrinho.precocar[i], Carrinho.quanticar[i], Carrinho.desccar[i]);
                lugar++;
            }

            Console.WriteLine("\n\t\tTotal: R${0:n2}", total);

            Carrinho.GerarNotaCarrinho(total);


            Console.ReadKey();
        } 

           
    }
}
