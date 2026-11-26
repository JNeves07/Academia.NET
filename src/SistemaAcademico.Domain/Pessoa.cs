namespace SistemaAcademico.Domain;

/// <summary>Dados comuns a alunos e professores.</summary>
public abstract class Pessoa
{
    protected Pessoa(string nome, DateTime dataNascimento, string cpf, string email)
    {
        Nome = Validacao.ValidarNome(nome);
        DataNascimento = Validacao.ValidarDataNascimento(dataNascimento);
        Cpf = Validacao.ValidarCpf(cpf);
        Email = Validacao.ValidarEmail(email);
    }

    public string Nome { get; private set; }

    public DateTime DataNascimento { get; private set; }

    public string Cpf { get; private set; }

    public string Email { get; private set; }

    public void AtualizarNome(string nome) => Nome = Validacao.ValidarNome(nome);

    public void AtualizarEmail(string email) => Email = Validacao.ValidarEmail(email);

    public string SalvarPessoa() => "Pessoa salva com sucesso";

    public override string ToString() => $"{Nome} (CPF {Cpf})";
}
