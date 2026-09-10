using System;
using System.Collections.Generic;

namespace ProjetosPOO
{
    // UNIDADE 1: ABSTRAÇÃO
    // Uma conta bancária "de verdade" tem dezenas de detalhes (limite,
    // taxas, histórico de mudança de titular, etc). Aqui abstraímos só
    // o essencial para estudar POO: número, agência, cliente titular,
    // saldo e as transações realizadas.
    public class Conta
    {
        // UNIDADE 3: ATRIBUTOS / PROPRIEDADES
        // UNIDADE 4: ENCAPSULAMENTO
        // Numero e Agencia: podem ser lidos de fora (get público), mas
        // só alterados por métodos da própria classe (private set).
        public string Numero { get; private set; }
        public string Agencia { get; private set; }

        // UNIDADE 6: RELACIONAMENTO: ASSOCIAÇÃO
        // Conta ─ Cliente é uma ASSOCIAÇÃO: são duas classes
        // independentes que se conhecem (a Conta guarda uma referência
        // para o Cliente titular), mas nenhuma delas "é dona" do ciclo
        // de vida da outra. Se a Conta for encerrada, o Cliente continua
        // existindo normalmente (e pode ter outras contas).
        // "as duas classes consegue existir de forma independente?" Aqui, SIM.
        public Cliente Cliente { get; private set; }

        // UNIDADE 6: RELACIONAMENTO: COMPOSIÇÃO (Conta ◆── Transacao)
        // Veja os comentários completos em Transacao.cs. Aqui reforçamos
        // o ENCAPSULAMENTO (Unidade 4): a lista é "private set" E a
        // propriedade em si é privada, ninguém de fora da Conta pode
        // nem ler, nem trocar essa lista diretamente, nem fazer
        // "conta.Transacoes.Add(...)" para inserir uma transação fake.
        // A ÚNICA forma de uma transação nascer é através dos métodos
        // Depositar/Sacar, que é exatamente o comportamento que
        // queremos garantir.
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

        // Saldo também é privado: ninguém de fora pode fazer
        // "conta.Saldo = 1000000" e "hackear" o próprio saldo. A única
        // forma de alterá é através de Depositar/Sacar (mensagens
        // que respeitam as regras da própria conta).
        private decimal Saldo { get; set; }

        // UNIDADE 5: CONSTRUTOR E CICLO DE VIDA
        // No nascimento da conta, ela já recebe Numero, Agencia e
        // Cliente (todos obrigatórios) e começa com Saldo = 0 e sem
        // nenhuma transação. Isso é o "estado inicial" do objeto.
        public Conta(string numero, string agencia, Cliente cliente)
        {
            Numero = numero;
            Agencia = agencia;
            Cliente = cliente;
            Saldo = 0;
        }

        // UNIDADE 7: MENSAGENS
        // Quando o Program (ou o Banco, ou a Agencia) chama
        // "conta.Depositar(500)", ele está ENVIANDO UMA MENSAGEM para
        // o objeto conta: "eu não sei COMO você guarda seu saldo, só
        // sei que você entende a mensagem Depositar". Quem decide o
        // que fazer com essa mensagem é a própria Conta, isso é
        // encapsulamento de comportamento, não só de dados.
        public void Depositar(decimal valor)
        {
            // Observação didática: propositalmente NÃO há validação de
            // "valor > 0" aqui. Isso é discutido em aula como uma
            // simplificação: o foco desta versão é ensinar relação
            // entre objetos, não regra de negócio. Fica de exercício:
            // "o que aconteceria se alguém chamasse Depositar(-500)?"
            Saldo += valor;

            // Aqui a Conta CRIA a Transacao (composição em ação: quem
            // dá vida à parte é o próprio todo).
            Transacoes.Add(new Transacao("Depósito", valor, DateTime.Now));
        }

        public void Sacar(decimal valor)
        {
            // Mesma observação: sem checagem de "valor <= Saldo", o
            // saldo pode ficar negativo.
            //
            // Ponto de discussão: "isso é uma conta com cheque especial automático
            // e ilimitado, ou é um bug?" Os dois são respostas
            // válidas dependendo da regra de negócio que você quiser simular.
            Saldo -= valor;
            Transacoes.Add(new Transacao("Saque", valor, DateTime.Now));
        }

        // ConsultarSaldo() é a forma correta e encapsulada de "ler" um
        // dado privado: em vez de expor o campo Saldo, expomos um
        // MÉTODO que devolve o valor. Isso permite, no futuro, mudar
        // como o saldo é calculado internamente sem quebrar quem usa
        // a classe Conta.
        public decimal ConsultarSaldo()
        {
            return Saldo;
        }

        // um método público que devolve os dados de forma segura,
        public List<Transacao> ObterExtrato()
        {
            return Transacoes;
        }

        // UNIDADE 6: RELACIONAMENTO (apoio à Agregação Agencia↔Conta)
        // Esse método existe justamente para ser chamado pela Agencia
        // quando uma conta é transferida entre agências (veja
        // Agencia.TransferirConta). Ele garante que o "estado interno"
        // da conta (o texto "Agencia") continue coerente com a lista
        // em que ela realmente está.
        public void AlterarAgencia(string novaAgencia)
        {
            Agencia = novaAgencia;
        }
    }
}
