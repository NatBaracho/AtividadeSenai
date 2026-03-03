# 🏦 Banco C-Sharp — Sistema de Notificações Bancárias

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=dot-net&logoColor=white)
![Console App](https://img.shields.io/badge/Console-Application-blue?style=for-the-badge)
![POO](https://img.shields.io/badge/Paradigma-POO-orange?style=for-the-badge)

> Atividade prática de **Programação Orientada a Objetos em C#**, cobrindo encapsulamento, propriedades, construtores, métodos e **Delegates** para um sistema de alertas bancários.

---

## 📋 Sobre o Projeto

O **Banco C-Sharp** precisa de um sistema básico para gerenciar contas de clientes. Por exigência de segurança, qualquer saque acima de **R$ 1.000** ou que utilize o limite extra deve disparar um **alerta de notificação**.

Como o canal de envio pode variar (e-mail ou SMS), a solução utiliza **Delegates** para desacoplar o método de saque da função de notificação, permitindo flexibilidade total na implementação.

---

## 🎯 Objetivos de Aprendizagem

- ✅ Declarar e utilizar **Delegates** em C#
- ✅ Criar **classes** com atributos privados (fields)
- ✅ Aplicar **encapsulamento** com propriedades (`get` / `set`)
- ✅ Implementar **construtores** para inicializar objetos
- ✅ Escrever **métodos** de negócio (Depositar, Sacar)
- ✅ Compreender o fluxo de **eventos e notificações**

---

## 🗂️ Estrutura do Projeto

```
ConsoleApp1/
├── ContaBancaria.cs    # Classe com atributos, propriedades, construtor e métodos
├── Program.cs          # Ponto de entrada com os testes do sistema
└── README.md
```

---

## 🔨 Implementação

### `ContaBancaria.cs`

#### Passo 1 — O Delegate

Declarado dentro da própria classe `ContaBancaria`, sem retorno e recebendo uma `string`:

```csharp
public delegate void NotificacaoAlerta(string mensagem);
```

---

#### Passo 2 — Atributos Privados

```csharp
private string _titular;
private double _saldo;
private double _limiteExtra;
```

---

#### Passo 3 — Propriedades

| Propriedade   | Get     | Set     | Observação                                                  |
|---------------|---------|---------|-------------------------------------------------------------|
| `Titular`     | público | público | Leitura e escrita livres                                    |
| `Saldo`       | público | privado | Alterado apenas por `Depositar` e `Sacar`                   |
| `LimiteExtra` | público | público | Valida que o valor não seja negativo; se for, define como 0 |

```csharp
public string Titular
{
    get { return _titular; }
    set { _titular = value; }
}

public double Saldo
{
    get { return _saldo; }
    private set { _saldo = value; }
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
```

---

#### Passo 4 — Construtor

```csharp
public ContaBancaria(string titular, double saldoInicial, double limiteExtra)
{
    Titular     = titular;
    Saldo       = saldoInicial;
    LimiteExtra = limiteExtra;
}
```

---

#### Passo 5 — Métodos

**`Depositar`** — Aceita apenas valores positivos:

```csharp
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
```

**`Sacar`** — Recebe o delegate `NotificacaoAlerta` e dispara alerta quando o saque for acima de R$ 1.000 ou deixar o saldo negativo:

```csharp
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
```

---

### `Program.cs`

#### Passo 6 — Programa Principal

Funções de notificação compatíveis com o delegate:

```csharp
public static void EnviarEmail(string msg)
{
    Console.WriteLine($"Enviando E-mail: {msg}");
}

public static void EnviarSMS(string msg)
{
    Console.WriteLine($"Enviando SMS: {msg}");
}
```

Testes realizados no `Main`:

```csharp
ContaBancaria conta = new ContaBancaria("Natã", 2000, 500);

// Teste 1: Saque normal — sem alerta
conta.Sacar(300, EnviarEmail);

// Teste 2: Saque de alto valor — alerta via e-mail
conta.Sacar(1500, EnviarEmail);

// Teste 3: Saque que usa limite — alerta via SMS
conta.Sacar(1000, EnviarSMS);
```

---

## 🖥️ Saída Esperada no Console

```
Titular: Natã
Saldo inicial: 2000
Limite Extra: 500

Saldo após saque: 1700

Enviando E-mail: ALERTA: Saque de alto valor ou uso de limite detectado na conta de Natã.
Saldo após saque: 200

Enviando SMS: ALERTA: Saque de alto valor ou uso de limite detectado na conta de Natã.
Saldo após saque: -800
```

---

## 💡 Conceitos Demonstrados

| Conceito             | Onde é aplicado                                              |
|----------------------|--------------------------------------------------------------|
| **Delegate**         | `NotificacaoAlerta` passado como parâmetro para `Sacar`      |
| **Encapsulamento**   | Atributos `private`, expostos por propriedades               |
| **Validação no Set** | `LimiteExtra` impede valores negativos                       |
| **Set privado**      | `Saldo` só é alterado internamente pelos métodos da classe   |
| **Construtor**       | Inicializa `Titular`, `Saldo` e `LimiteExtra`                |
| **Métodos**          | `Depositar` e `Sacar` com regras de negócio                  |

---

## ▶️ Como Executar

**Pré-requisito:** [.NET SDK](https://dotnet.microsoft.com/download) instalado.

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/banco-csharp.git

# Acesse a pasta
cd banco-csharp

# Execute o projeto
dotnet run
```

---

## 📚 Referências

- [Documentação oficial C# — Delegates](https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/delegates/)
- [Propriedades em C#](https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/properties)
- [Conceitos de POO em C#](https://learn.microsoft.com/pt-br/dotnet/csharp/fundamentals/tutorials/oop)

---

<p align="center">Feito com 💙 para a disciplina de <strong>Programação Orientada a Objetos</strong></p>
