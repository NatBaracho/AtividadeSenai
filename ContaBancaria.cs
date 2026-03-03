using System;

namespace ConsoleApp1
{
    internal class ContaBancaria
    {
        // Passo 1: Criar um delegate (void, recebe string)
        public delegate void NotificacaoAlerta(string mensagem);

        // Passo 2: Atributos privados
        private string _titular;
        private double _saldo;
        private double _limiteExtra;

        // Passo 3: Propriedades (Gets e Sets)
        public string Titular
        {
            get { return _titular; }
            set { _titular = value; }
        }

        public double Saldo
        {
            get { return _saldo; }
            private set { _saldo = value; } // set privado
        }

        public double LimiteExtra
        {
            get { return _limiteExtra; }
            set
            {
                if (value < 0)
                {
                    _limiteExtra = 0;
                    Console.WriteLine("Erro: O limite extra não pode ser negativo.");
                }
                else
                {
                    _limiteExtra = value;
                }
            }
        }

        // Passo 4: Construtor
        public ContaBancaria(string titular, double saldoInicial, double limiteExtra)
        {
            Titular = titular;
            Saldo = saldoInicial;
            LimiteExtra = limiteExtra;
        }

        // Passo 5: Criando métodos
        public void Depositar(double valor)
        {
            if (valor > 0)
            {
                Saldo += valor;
            }
            else
            {
                Console.WriteLine("Erro: o valor do depósito deve ser positivo.");
            }
        }

        public void Sacar(double valor, NotificacaoAlerta alerta)
        {
            if (valor <= 0)
            {
                Console.WriteLine("Erro: o valor do saque deve ser positivo.");
                return;
            }

            if (Saldo + LimiteExtra >= valor)
            {
                Saldo -= valor;

                // Regra do delegate
                if (valor > 1000 || Saldo < 0)
                {
                    alerta?.Invoke($"ALERTA: Saque de alto valor ou uso de limite detectado na conta de {Titular}.");
                }
            }
            else
            {
                Console.WriteLine("Erro: Saldo insuficiente para realizar o saque.");
            }
        }
    }
}