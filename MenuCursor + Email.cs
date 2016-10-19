using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ACC
{
    class Program
    {
        class Cursor // variáveis do cursor que serão reaproveitadas.
        {  // os parâmetros public static string se devem pelo fato de que tais variáveis podem ser acessadas em qualquer lugar no sistema.
           public static string val1 = "->";
           public static string val2 = "  ";
           public static string val3 = "  ";
           public static string val4 = "  ";
           public static string Opcursor1 = val1, Opcursor2 = val2;
           public static ConsoleKeyInfo KeyPressed = new ConsoleKeyInfo(); // variável de leitura de tecla pressionada

           public static void PadCursor()
            {
                val1 = "->";
                val2 = "  ";
                val3 = "  ";
            } // retorna o valor padrão dos cursores 

        }

        class MenuPrincipal
        {
            public static void MenuMain() // variável para ler a tecla pressionada pelo usuário
            {
                // Primeiro menu a ser exibido
                Console.WriteLine("{0}Enviar email.", Cursor.val1);
                Console.WriteLine("{0}Leitor de texto.", Cursor.val2);
                Console.WriteLine("{0}Sair.", Cursor.val3);
                Cursor.KeyPressed = Console.ReadKey(); // leitura da tecla apertada          

            }

            public static void OpcaoSair() // Opção da tela principal
            {
                        Console.Clear();
                        Cabeçalho();
                        Console.Write("Deseja finalizar o sistema? \n");
                        Console.Write("{0}Não          {1}Sim", Cursor.Opcursor1, Cursor.Opcursor2);
                        Cursor.KeyPressed = Console.ReadKey();

                    Console.Clear();
                   
                }

            public static void OpcaoSairCursor()
            {
                if (Cursor.Opcursor1 == "->" && Cursor.Opcursor2 == "  ")
                {
                    Cursor.Opcursor1 = "  ";
                    Cursor.Opcursor2 = "->";
                }
                else
                {
                    Cursor.Opcursor1 = "->";
                    Cursor.Opcursor2 = "  ";
                }
            }
        } // Contem o as opções do menu e o método para escolha de opção.

        class Arrows
        {
            public static void ArrowDown()
            {
                if (Cursor.val1 == "->")
                {
                    Cursor.val1 = "  ";
                    Cursor.val2 = "->";
                    Cursor.val3 = "  ";
                }
                else if (Cursor.val2 == "->")
                {
                    Cursor.val1 = "  ";
                    Cursor.val2 = "  ";
                    Cursor.val3 = "->";
                }
                else
                {
                    Cursor.val1 = "->";
                    Cursor.val2 = "  ";
                    Cursor.val3 = "  ";
                }
            }

            public static void ArrowUp()
            {
                if (Cursor.KeyPressed.Key == ConsoleKey.UpArrow) // Se for pressionada a tecla "seta para cima"
                {
                    if (Cursor.val1 == "->" && Cursor.val2 == "  " && Cursor.val3 == "  ")
                    {
                        Cursor.val1 = "  ";
                        Cursor.val2 = "  ";
                        Cursor.val3 = "->";
                    }
                    else if (Cursor.val1 == "  " && Cursor.val2 == "->" && Cursor.val3 == "  ")
                    {
                        Cursor.val1 = "->";
                        Cursor.val2 = "  ";
                        Cursor.val3 = "  ";
                    }
                    else
                    {
                        Cursor.val1 = "  ";
                        Cursor.val2 = "->";
                        Cursor.val3 = "  ";
                    }
                }
            }
        } // classe com os métodos de seta para cima e para baixo.

        static void Cabeçalho()
        {   //Titulo do console
            Console.Title = "Aplicativos Conjuntos para Console - ACC"; // título do console
            string Exe = "ACC.exe"; //Nome do arquivo exe
            string ExeLocation; // variável que receberá a localização do .exe
      
            ExeLocation = Path.GetFullPath(Exe); // mostra onde está a variável "Exe"( Arquivo do programa)
            Console.Write(ExeLocation + "\t");

            // Mostra data e hora atual
            DateTime TempoFull = DateTime.Now; // variável tempo recebe a data e a hora
            Console.Write("\t" + TempoFull.ToString("dd/MM/yyy") + "\n\n"); // exibe a variável data/hora, mas com parâmetro 
            // de somente data. 
            
           

        } // exibido no topo do programa

        static void leitor()
        {
            string arquivo; // variável para receber o nome do arquivo
            int linha = 1; // variável para exibir o número de cada linha
            do
            {
                Console.Clear();
                Console.Write("Qual o nome do arquivo ?: ");
                arquivo = Console.ReadLine(); // recebe nome do arquivo

                 if(File.Exists(arquivo) == false)
                {
                    do
                    {
                        Console.Clear();
                        Console.Write("Valor inválido, digite o nome de um existente: ");
                        arquivo = Console.ReadLine();

                    } while (File.Exists(arquivo) == false);
                }

            } while (File.Exists(arquivo) == false);

            using (StreamReader fileread = new StreamReader(arquivo +".txt")) //
            {
                string Line;

                while((Line = fileread.ReadLine()) != null)
                {

                    Console.WriteLine(linha + "\t" + Line);
                    linha++;
                }
                

                fileread.Close();
            }


           // using (StreamReader Texto = new StreamReader())
           
        } // leitor do arquivo caso seja selecionado a opção de ler um.

        static void NetTest()
            {
            string status = "";
                try
                {
                    using (var cliente = new WebClient())
                    {
                        using (var teste = cliente.OpenRead("http://www.google.com.br"))
                        {

                            status = "Conexão OK";
                        
                        }
                    }
                }
                catch
                {

                    status = "Sem conexão";
                }
            Console.Write(status);

        } // Tesde simples de conexão à internet.

        static void Email(ref string cliente, ref string dest, ref string senha, ref string titulo, ref string msg) //recebe as informações para o email
        {   
            int index = cliente.LastIndexOf('@'); // variavel para pegar contar todos os valores antes do @

            SmtpClient SMTP = new SmtpClient(); // envio SMTP 

            // a diferença de cada provedor de emial está no seu host e a porta de comunicação basicamente.
                    //email de envio gmail.com
            if (cliente.Substring(index) == "@gmail.com" || cliente.Substring(index) == "@gmail.com.br") //se for retirado dos os carcteres entes do @, e o que sobrar for igual à "@gmail.com"
            {

                //configuração do SMTP. porta, host, timeout, etc
                SMTP = new SmtpClient("smtp.gmail.com", 587); // cria protocolo SMTP, informando o host(no caso O Gmail e a porta de envio dele. tais informações são retiradas do provedor de email

                SMTP.EnableSsl = true; //Conexão encriptografada.
                SMTP.Timeout = 10000; //tempo de tentativa de envio.
                SMTP.DeliveryMethod = SmtpDeliveryMethod.Network; //qual será o método de entrega do email? Internet
                SMTP.UseDefaultCredentials = false; //
                SMTP.Credentials = new NetworkCredential(cliente, senha);
            }
                         //email de envio Hotmail.com
            else if (cliente.Substring(index) == "@hotmail.com" || cliente.Substring(index) == "@hotmail.com.br")
            {
                 SMTP = new SmtpClient("smtp-mail.outlook.com", 587); // cria protocolo SMTP, informando o host(no caso O Gmail e a porta de envio dele. tais informações são retiradas do provedor de email

                SMTP.EnableSsl = true; //Conexão encriptografada.
                SMTP.Timeout = 10000; //tempo de tentativa de envio.
                SMTP.DeliveryMethod = SmtpDeliveryMethod.Network; //qual será o método de entrega do email? Internet
                SMTP.UseDefaultCredentials = false; //
                SMTP.Credentials = new NetworkCredential(cliente, senha);

            }
            else if(cliente.Substring(index) == "@yahoo.com" || cliente.Substring(index) == "@yahoo.com.br")
            {
                SMTP = new SmtpClient("smtp.mail.yahoo.com", 465); // cria protocolo SMTP, informando o host(no caso O Gmail e a porta de envio dele. tais informações são retiradas do provedor de email

                SMTP.EnableSsl = true; //Conexão encriptografada.
                SMTP.Timeout = 10000; //tempo de tentativa de envio.
                SMTP.DeliveryMethod = SmtpDeliveryMethod.Network; //qual será o método de entrega do email? Internet
                SMTP.UseDefaultCredentials = false; //
                SMTP.Credentials = new NetworkCredential(cliente, senha);
            }
                MailMessage mail = new MailMessage(cliente, dest, titulo, msg);
                mail.BodyEncoding = UTF8Encoding.UTF8;

                Cabeçalho();
                Console.WriteLine("\nEnviando email....");

                try
                {
                    SMTP.Send(mail);

                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                    return;
                }
               
                DateTime TempoRecebe = DateTime.Now;
                Console.Write("Email enviado com sucesso. " + TempoRecebe);
            }
        
        public static string LerSenha()
        {
            string senha = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter) // enquanto não for pressionado Enter:
            {
                if (info.Key != ConsoleKey.Backspace) //se não pressionar o backspace
                {
                    Console.Write("*");
                    senha += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(senha))
                    {
                        // remove o carter pressionado
                        senha = senha.Substring(0, senha.Length - 1);
                        // volta o cursos uma casa.
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // 
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return senha;
        } //deixa a senha mascarada

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

                do
                {
                    Console.Clear();
                    Cabeçalho();
                    MenuPrincipal.MenuMain();

                    if (Cursor.KeyPressed.Key == ConsoleKey.DownArrow) Arrows.ArrowDown();
                    else if (Cursor.KeyPressed.Key == ConsoleKey.UpArrow) Arrows.ArrowUp();

                } while (Cursor.KeyPressed.Key != ConsoleKey.Enter);

                  if (Cursor.val1 == "->")
            {
                string cliente, dest, senha="",  titulo, msg;

                Console.Clear();
                Cabeçalho();
                Console.WriteLine("<----------------------------- Email SMTP Gmail ------------------------------------->\n");
                NetTest();

                Console.Write("\nDe: ");
                cliente = Console.ReadLine();

                do {
                    try
                    {
                        MailAddress teste = new MailAddress(cliente);
                    }
                    catch (FormatException)
                    {
                        cliente = "";
                        Console.Write("Email inválido!, digite novamente");
                        cliente = Console.ReadLine();                       
                    }

                } while (cliente == "");

                Console.Write("Senha: ");
                senha = LerSenha();

                Console.Write("Para: ");
                dest = Console.ReadLine();

                do
                {
                    try
                    {
                        MailAddress teste = new MailAddress(dest);

                    }
                    catch (FormatException)
                    {
                        dest = "";
                        Console.Write("Email inválido!, digite novamente");
                        dest = Console.ReadLine();
                    }
                } while (dest == "");



                Console.Write("\n\nTitulo: ");
                titulo = Console.ReadLine();

                using (StreamWriter Mensagem = new StreamWriter("mensagem.txt", true))
                {
                    string texto;
                    Console.Write("Aperte END para terminar de escrever a mensagem.\n");
                    Console.Write("Mensagem:\n");
                    do
                    {
                        Console.Write("");
                        texto = Console.ReadLine();
                        Mensagem.WriteLine(texto);
                        Cursor.KeyPressed = Console.ReadKey();

                    } while (Cursor.KeyPressed.Key != ConsoleKey.End);

                    Mensagem.Close();           
                }
             
                Console.Clear();

                msg = File.ReadAllText("mensagem.txt");

                Email(ref cliente, ref dest, ref senha, ref titulo, ref msg);



                var resp = "";
                Console.Write("\nDeseja voltar para a tela inicial?(s/n): ");
                resp = Console.ReadLine();
                if (resp == "s")
                {
                    Main(args);
                }
                else return;

            }
                else if (Cursor.val2 == "->") leitor();
                else
                {
                Cursor.PadCursor();
                do
                {
                    MenuPrincipal.OpcaoSair();
                    if (Cursor.KeyPressed.Key == ConsoleKey.LeftArrow || Cursor.KeyPressed.Key == ConsoleKey.RightArrow) MenuPrincipal.OpcaoSairCursor();

                } while (Cursor.KeyPressed.Key != ConsoleKey.Enter);

                if (Cursor.Opcursor1 == "  " && Cursor.Opcursor2 == "->")
                {                   
                    return;
                }
                else Main(args); // volta para o menu principal

            }                
            Console.ReadKey();
        }
    }

}

