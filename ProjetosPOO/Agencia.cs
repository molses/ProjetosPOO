using System.Collections.Generic;

namespace ProjetosPOO
{
    // UNIDADE 6: RELACIONAMENTO ENTRE CLASSES: AGREGAÇÃO
    // Agencia ◇── Conta é uma AGREGAÇÃO (losango VAZIO na UML):
    // a Agencia "tem" contas (List<Conta>), mas cada Conta poderia,
    // em tese, existir de forma independente ou ser movida para outra
    // Agencia sem deixar de existir, diferente da composição
    // Conta ◆── Transacao, em que a "parte" não sobrevive sem o "todo".
   
    public class Agencia
    {
        // UNIDADE 3/4: atributos com encapsulamento (private set)
        public string Codigo { get; private set; }
        public string Nome { get; private set; }

        // A lista de contas é pública aqui de propósito (diferente da
        // lista de Transacoes dentro de Conta): 
        public List<Conta> Contas { get; private set; } = new List<Conta>();

        // UNIDADE 5: Construtor: toda Agencia nasce com Codigo e Nome
        // definidos e uma lista de contas vazia.
        public Agencia(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }

        // UNIDADE 7: MENSAGENS + UNIDADE 5 — CICLO DE VIDA
        // Abre a conta NESTA agência: quem "dá à luz" o objeto Conta
        // é a própria Agencia (ela chama "new Conta(...)"), e logo em
        // seguida se agrega essa conta à sua própria lista.
        public Conta AbrirConta(string numeroConta, Cliente cliente)
        {
            var conta = new Conta(numeroConta, Codigo, cliente);
            Contas.Add(conta);
            return conta;
        }

        // AGREGAÇÃO NA PRÁTICA: a conta é apenas MOVIDA de uma lista
        // para outra, não é recriada (não tem "new Conta" aqui) nem
        // destruída. É o mesmo objeto, agora referenciado por outra
        // agência.
        //
        //
        // Agencia ENVIA UMA MENSAGEM para a Conta avisando da mudança
        public void TransferirConta(Conta conta, Agencia agenciaDestino)
        {
            if (Contas.Remove(conta))
            {
                agenciaDestino.Contas.Add(conta);
                conta.AlterarAgencia(agenciaDestino.Codigo);
            }
        }
    }
}
