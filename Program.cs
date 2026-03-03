namespace ConsoleApp1
{
    internal class Program
    {
        // Passo 6: Criando o programa Main

        #region Envio de mensagens
        public static void EnviarEmail(string msg)
        {
            Console.WriteLine($"Enviando E-mail: {msg}");
        }

        public static void EnviarSMS(string msg)
        {
            Console.WriteLine($"Enviando SMS: {msg}");
        }
        #endregion

        #region Criando uma conta bancária
        static void Main(string[] args)
        {
            // Instanciando a conta bancária
            ContaBancaria conta = new ContaBancaria("Natã", 2000, 500);

            Console.WriteLine($"Titular: {conta.Titular}");
            Console.WriteLine($"Saldo inicial: {conta.Saldo}");
            Console.WriteLine($"Limite Extra: {conta.LimiteExtra}");
            Console.WriteLine();

            #region Teste 1: Saque normal (Sem Alerta)
            conta.Sacar(300, EnviarEmail);
            Console.WriteLine($"Saldo após saque: {conta.Saldo}");
            Console.WriteLine();
            #endregion

            #region Teste 2: Saque alto valor (gera alerta via e-mail)
            conta.Sacar(1500, EnviarEmail);
            Console.WriteLine($"Saldo após saque: {conta.Saldo}");
            Console.WriteLine();
            #endregion

            #region Teste 3: Saque que usa limite (gera alerta via SMS)
            conta.Sacar(1000, EnviarSMS);
            Console.WriteLine($"Saldo após saque: {conta.Saldo}");
            #endregion

            Console.ReadKey();
        }
        #endregion
    }
}