namespace ProjetosPOO
{
    // UNIDADE 1: ABSTRAÇÃO
    // "Cliente" é uma abstração: pegamos uma pessoa do mundo real
    // (que tem MUITAS características altura, cor dos olhos, humor,
    // time de futebol...) e escolhemos guardar SÓ o que interessa
    // para o nosso sistema bancário: Nome, CPF, Endereço e RG.
    // Isso é abstrair: representar somente o que é relevante para o
    // problema que estamos resolvendo.
    //
    // UNIDADE 2: CLASSES E OBJETOS
    // "Cliente" é a CLASSE (o molde, a planta, o "projeto da casa").
    // Quando fazemos "new Cliente(...)" no Program, criamos um OBJETO
    // (uma instância concreta daquele molde, uma "casa construída").
    // Podemos criar quantos clientes quisermos a partir dessa mesma
    // classe: cada um com seus próprios valores de Nome, Cpf, etc.
    public class Cliente
    {
        // UNIDADE 3 — ATRIBUTOS / MÉTODOS / PROPRIEDADES
        // Nome, Cpf, Endereco e Rg são os ATRIBUTOS do Cliente
        // (o "estado" que ele guarda). Aqui eles estão expostos como
        // PROPRIEDADES (get/set), que é a forma "C#" de expor atributos
        // de forma controlada, em vez de usar campos públicos soltos
        // (public string nome;).
        //
        // UNIDADE 4 — ENCAPSULAMENTO E CONTROLE DE ACESSO
        // Repare: "private set". Isso significa que qualquer código
        // FORA da classe Cliente pode LER (get) essas informações,
        // mas NINGUÉM de fora pode ALTERAR (set) depois que o objeto
        // foi criado.
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Endereco { get; private set; }
        public string Rg { get; private set; }

        // UNIDADE 5: CONSTRUTORES E CICLO DE VIDA
        // O construtor é o método especial chamado no exato momento em
        // que o objeto "nasce" (new Cliente(...)). Como Nome, Cpf,
        // Endereco e Rg só têm "private set", a ÚNICA forma de definir
        // esses valores é passando-os aqui, no nascimento do objeto.
        // Depois disso, o objeto vive com esses dados até ser
        // descartado (não existe um "AlterarCpf" o cliente não
        // troca de CPF depois de criado, por decisão de design).
        public Cliente(string nome, string cpf, string endereco, string rg)
        {
            Nome = nome;
            Cpf = cpf;
            Endereco = endereco;
            Rg = rg;
        }

        // Observação didática (não é obrigatório para o N1, mas fica
        // como provocação): note que não há validação de formato de
        // Cpf/Rg aqui (ex.: 11 dígitos, dígito verificador). Isso foi
        // deixado de propósito simples, para o foco ficar nos conceitos
        // de classes/objetos/relacionamentos, e não em regras de
        // negócio. Fica de exercício pensar: "onde eu validaria isso?"
    }
}
