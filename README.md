# 🏦 Banco C-Sharp — Sistema de Notificações Bancárias

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=dot-net&logoColor=white)
![Console App](https://img.shields.io/badge/Console-Application-blue?style=for-the-badge)
![POO](https://img.shields.io/badge/Paradigma-POO-orange?style=for-the-badge)

> Atividade prática de **Programação Orientada a Objetos em C#**, cobrindo encapsulamento, propriedades, construtores, métodos e **Delegates** para um sistema de alertas bancários.

---

## 📋 Sobre o Projeto

O **Banco C-Sharp** precisa de um sistema básico para gerenciar contas de clientes. Por exigência de segurança, qualquer saque acima de um valor específico ou que deixe a conta negativa deve disparar um **alerta de notificação**.

Como o canal de envio pode variar (e-mail, SMS, console), a solução utiliza **Delegates** para desacoplar o método de saque da função de notificação, permitindo flexibilidade total na implementação.

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
BancoCSharp/
├── Program.cs              # Ponto de entrada e testes do sistema
├── ContaBancaria.cs        # Classe principal com lógica de negócio
└── README.md
```

---

## 🔨 Passo a Passo da Implementação

### Passo 1 — O Delegate

Declare o delegate `NotificacaoAlerta` fora ou dentro da classe principal. Ele não retorna nada (`void`) e recebe uma `string` com a mensagem de alerta:

```csharp
public delegate void NotificacaoAlerta(string mensagem);
```

---

### Passo 2 — A Classe e Atributos

Crie a classe `ContaBancaria` com os seguintes **atributos privados**:

```csharp
public class ContaBancaria
{
    private string _titular;
    private double _saldo;
    private double _limiteExtra;
}
```

---

### Passo 3 — Propriedades (Gets e Sets)

Encapsule os atributos usando **Propriedades do C#**:

| Propriedade    | Get     | Set     | Observação                                      |
|----------------|---------|---------|--------------------------------------------------|
| `Titular`      | público | público | Livre leitura e escrita                         |
| `Saldo`        | público | privado | Alterado apenas por `Depositar` e `Sacar`       |
| `LimiteExtra`  | público | público | Pode ser ajustado externamente                  |

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
    set { _limiteExtra = value; }
}
```

---

### Passo 4 — Construtor

Inicialize a conta com titular, saldo inicial e limite extra:

```csharp
public ContaBancaria(string titular, double saldoInicial, double limiteExtra)
{
    _titular   = titular;
    _saldo     = saldoInicial;
    _limiteExtra = limiteExtra;
}
```

---

### Passo 5 — Métodos

#### `Depositar`
Adiciona valor ao saldo (rejeita valores negativos):

```csharp
public void Depositar(double valor)
{
    if (valor <= 0)
    {
        Console.WriteLine("Valor de depósito inválido.");
        return;
    }
    Saldo += valor;
    Console.WriteLine($"Depósito de R$ {valor:F2} realizado. Saldo atual: R$ {Saldo:F2}");
}
```

#### `Sacar`
Realiza o saque e **aciona o delegate** de notificação quando necessário:

```csharp
public void Sacar(double valor, NotificacaoAlerta notificacao)
{
    double limiteTotal = _saldo + _limiteExtra;

    if (valor <= 0)
    {
        Console.WriteLine("Valor de saque inválido.");
        return;
    }

    if (valor > limiteTotal)
    {
        notificacao?.Invoke($"[ALERTA] Saque de R$ {valor:F2} NEGADO. Saldo insuficiente.");
        return;
    }

    Saldo -= valor;

    if (Saldo < 0)
    {
        notificacao?.Invoke($"[ALERTA] Saque de R$ {valor:F2} deixou a conta negativa! Saldo: R$ {Saldo:F2}");
    }

    Console.WriteLine($"Saque de R$ {valor:F2} realizado. Saldo atual: R$ {Saldo:F2}");
}
```

---

### Passo 6 — Testando no `Program.cs`

```csharp
// Funções de notificação
static void NotificarConsole(string mensagem) => Console.WriteLine(mensagem);
static void NotificarSMS(string mensagem)     => Console.WriteLine($"[SMS] {mensagem}");
static void NotificarEmail(string mensagem)   => Console.WriteLine($"[EMAIL] {mensagem}");

// Uso
ContaBancaria conta = new ContaBancaria("João Silva", 1000.00, 500.00);

conta.Depositar(200);
conta.Sacar(300, NotificarConsole);
conta.Sacar(1500, NotificarSMS);    // deve disparar alerta
conta.Sacar(900, NotificarEmail);   // pode deixar negativo — dispara alerta
```

---

## 💡 Conceitos Demonstrados

| Conceito         | Onde é aplicado                                      |
|------------------|------------------------------------------------------|
| **Delegate**     | `NotificacaoAlerta` recebido pelo método `Sacar`     |
| **Encapsulamento** | Atributos `private`, expostos por propriedades     |
| **Propriedade**  | `Saldo` com `set` privado                            |
| **Construtor**   | Inicialização dos campos da `ContaBancaria`          |
| **Método**       | `Depositar` e `Sacar` com regras de negócio          |

---

## ▶️ Como Executar

**Pré-requisitos:** [.NET SDK](https://dotnet.microsoft.com/download) instalado.

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
