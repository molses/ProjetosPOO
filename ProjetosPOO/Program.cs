using System;

namespace ProjetosPOO
{
    // MATERIAL DE ESTUDO: REVISÃO GERAL (N1)
    // Este Main funciona como um "passeio guiado" por todas as
    // unidades do 1º bimestre, usando o mesmo domínio bancário:
    //
    //   1. Abstração
    //   2. Classes e Objetos
    //   3. Atributos/Métodos/Propriedades
    //   4. Encapsulamento e Controle de Acesso
    //   5. Construtores e Ciclo de Vida
    //   6. Relacionamento entre Classes (associação/agregação/composição)
    //   7. Mensagens
    //
    // Leia os comentários numerados na ordem, eles contam uma
    // "história" de como o sistema é montado.
    public class Program
    {
        public static void Main()
        {
            // PASSO 1: Criando o Banco (topo da hierarquia de
            // agregação: Banco ◇ Agencia ◇ Conta ◆ Transacao)
            var banco = new Banco("Banco Estudo POO");

            // PASSO 2: O Banco cria (agrega) duas Agências.
            // Repare na MENSAGEM: "banco.CriarAgencia(...)" — o Main
            // não faz "new Agencia(...)" diretamente; ele pede para o
            // Banco fazer isso, respeitando quem é o responsável por
            // aquele relacionamento.
            var agenciaCentral = banco.CriarAgencia("0001", "Agência Central");
            var agenciaNorte = banco.CriarAgencia("0002", "Agência Norte");

            // PASSO 3: Criando um Cliente (OBJETO a partir da CLASSE
            // Cliente). É a ABSTRAÇÃO de uma pessoa real, guardando só
            // Nome/Cpf/Endereco/Rg.
            var joao = new Cliente("João da Silva", "123.456.789-00", "Rua Exemplo, 123", "MG-12.345.678");

            // PASSO 4: A Agência abre a conta para o cliente.
            // Isso demonstra a ASSOCIAÇÃO Conta→Cliente (a conta
            // guarda uma referência ao João) e a AGREGAÇÃO
            // Agencia→Conta (a conta entra na lista da agência).
            var contaJoao = agenciaCentral.AbrirConta("12345-6", joao);

            // PASSO 5: MENSAGENS: cada chamada abaixo é uma mensagem
            // enviada ao objeto contaJoao. O Main não sabe (nem
            // precisa saber) COMO o saldo é guardado por dentro, ele
            // só sabe que a Conta "entende" Depositar e Sacar.
            // Cada uma dessas chamadas também gera, por COMPOSIÇÃO,
            // um novo objeto Transacao dentro da conta.
            contaJoao.Depositar(500m);
            contaJoao.Sacar(150m);
            contaJoao.Depositar(500m);
            contaJoao.Sacar(150m);
            contaJoao.Depositar(500m);
            contaJoao.Sacar(150m);
            contaJoao.Depositar(500m);
            contaJoao.Sacar(150m);

            Console.WriteLine($"Saldo de {joao.Nome}: {contaJoao.ConsultarSaldo():C}");

            // PASSO 6: Consultando o extrato de forma ENCAPSULADA..
            Console.WriteLine("\nExtrato:");
            foreach (var transacao in contaJoao.ObterExtrato())
            {
                Console.WriteLine($"{transacao.Data:dd/MM/yyyy HH:mm:ss} | {transacao.Tipo,-10} | {transacao.Valor:C}");
            }

            // PASSO 7: RELACIONAMENTO: AGREGAÇÃO em ação.
            // Transferindo a conta do João da Agência Central para a
            // Agência Norte. Note que é a MESMA conta (mesmo objeto,
            // mesmo histórico de transações), ela não é recriada.
            // Isso é exatamente o que caracteriza agregação: a "parte"
            // sobrevive e muda de "todo".
            Console.WriteLine($"\nAntes da transferência, conta pertence à agência: {contaJoao.Agencia}");
            agenciaCentral.TransferirConta(contaJoao, agenciaNorte);
            Console.WriteLine($"Depois da transferência, conta pertence à agência: {contaJoao.Agencia}");

            // PASSO 8: Um segundo cliente e uma segunda conta, para
            // reforçar que a CLASSE Conta pode gerar quantos OBJETOS
            // conta quisermos, cada um com seu próprio saldo e seu
            // próprio histórico de transações (estados independentes).
            var maria = new Cliente("Maria Souza", "987.654.321-00", "Av. Central, 456", "SP-98.765.432");
            var contaMaria = agenciaNorte.AbrirConta("54321-0", maria);
            contaMaria.Depositar(1000m);

            // PASSO 9: MENSAGENS EM CADEIA: o Banco consulta o saldo
            // total de todas as agências, que por sua vez consultam o
            // saldo de todas as contas, sem que o Banco precise saber
            // COMO cada Conta calcula seu próprio saldo.
            Console.WriteLine($"\nSaldo total do {banco.Nome}: {banco.ConsultarSaldoTotal():C}");
        }
    }
}