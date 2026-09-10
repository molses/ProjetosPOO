using System.Collections.Generic;

namespace ProjetosPOO
{
    // UNIDADE 6: RELACIONAMENTO: AGREGAÇÃO
    // Banco ◇── Agencia é outra agregação, do mesmo tipo que
    // Agencia ◇── Conta.
    // o MESMO conceito (agregação) se repete em níveis diferentes do
    // sistema: Banco tem Agências, que têm Contas, que têm (por
    // composição) Transações. Uma cadeia de relacionamentos:
    //
    //   Banco ◇── Agencia ◇── Conta ◆── Transacao
    //                          Conta ── Cliente   (associação)
    //
    // Multiplicidade 
    //   1 Banco tem N Agências
    //   1 Agência tem N Contas
    //   1 Conta tem N Transações
    //   1 Conta está associada a 1 Cliente (nesta versão simplificada)
    public class Banco
    {
        public string Nome { get; private set; }
        public List<Agencia> Agencias { get; private set; } = new List<Agencia>();

        public Banco(string nome)
        {
            Nome = nome;
        }

        // UNIDADE 7: MENSAGENS
        // O Banco "não sabe" como uma Agencia guarda suas contas por
        // dentro; ele só envia a mensagem CriarAgencia. Quem decide o
        // que fazer com o pedido é o próprio Banco, que aqui atua como
        // uma "fábrica" de agências, do mesmo jeito que Agencia é
        // fábrica de Contas em AbrirConta.
        public Agencia CriarAgencia(string codigo, string nome)
        {
            var agencia = new Agencia(codigo, nome);
            Agencias.Add(agencia);
            return agencia;
        }

        // 
        // MENSAGENS EM CADEIA (encadeamento de
        // chamadas através de vários objetos relacionados):
        // Banco pergunta pra cada Agencia -> cada Agencia pergunta
        // pra cada Conta -> cada Conta responde seu próprio saldo
        // (sem que o Banco precise saber COMO o saldo é calculado)
        // Em nenhum momento o Banco acessa um campo privado
        // diretamente, ele só troca mensagens (chama métodos públicos).
        public decimal ConsultarSaldoTotal()
        {
            decimal total = 0;
            foreach (var agencia in Agencias)
            {
                foreach (var conta in agencia.Contas)
                {
                    total += conta.ConsultarSaldo();
                }
            }
            return total;
        }
    }
}
