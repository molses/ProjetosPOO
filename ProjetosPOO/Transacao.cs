using System;

namespace ProjetosPOO
{
    // UNIDADE 6: RELACIONAMENTO ENTRE CLASSES: COMPOSIÇÃO
    // Uma Transacao NÃO EXISTE sozinha no nosso sistema. Ela só faz
    // sentido "dentro" de uma Conta (ela nasce quando a Conta deposita
    // ou saca, lá dentro da própria Conta) e, se a Conta "morrer", as
    // transações dela morrem junto, ninguém mais tem acesso a elas.
    //
    // Isso é COMPOSIÇÃO (Conta ◆── Transacao): o "todo" (Conta) é dono das "partes"
    // (Transacao) e controla totalmente o ciclo de vida delas.
    // Compare com a Unidade 6 na classe Agencia (agregação), lá o
    // relacionamento é mais "fraco": a Conta sobrevive numa transferência
    // entre agências.
    //
    // "se eu apagar o TODO, a PARTE ainda faz sentido sozinha?" Aqui a resposta é NÃO
    // uma transação sem conta não significa nada.
    public class Transacao
    {
        // UNIDADE 3: ATRIBUTOS/PROPRIEDADES
        // Tipo, Valor e Data são o estado da transação.
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime Data { get; private set; }

        // UNIDADE 5: CONSTRUTOR / CICLO DE VIDA
        // Todos os dados são obrigatórios no construtor e nenhum
        // "set" é público depois disso: uma Transacao é um REGISTRO
        // HISTÓRICO. Faz sentido que um extrato bancário não possa
        // ser editado depois, imagine se você pudesse "reescrever"
        // um saque já realizado!
        public Transacao(string tipo, decimal valor, DateTime data)
        {
            Tipo = tipo;
            Valor = valor;
            Data = data;
        }
    }
}
