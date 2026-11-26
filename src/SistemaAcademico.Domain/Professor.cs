namespace SistemaAcademico.Domain;

/// <summary>Professor com registro, titulação e carga horária semanal.</summary>
public sealed class Professor : Pessoa, IIdentificavel
{
    public Professor(
        string nome,
        DateTime dataNascimento,
        string cpf,
        string email,
        string registro,
        string departamento,
        string titulacao,
        int cargaHoraria)
        : base(nome, dataNascimento, cpf, email)
    {
        Registro = Validacao.ValidarRegistro(registro);
        Departamento = Validacao.TextoObrigatorio(departamento, nameof(Departamento));
        Titulacao = Validacao.TextoObrigatorio(titulacao, nameof(Titulacao));
        CargaHoraria = Validacao.ValidarCargaHoraria(cargaHoraria);
    }

    public string Identificador => Registro;

    public string Registro { get; }

    public string Departamento { get; private set; }

    public string Titulacao { get; private set; }

    /// <summary>Horas semanais de trabalho; deve estar entre 1 e 80.</summary>
    public int CargaHoraria { get; private set; }

    public void AlterarCargaHoraria(int cargaHoraria) =>
        CargaHoraria = Validacao.ValidarCargaHoraria(cargaHoraria);

    public void AlterarDepartamento(string departamento) =>
        Departamento = Validacao.TextoObrigatorio(departamento, nameof(departamento));

    public bool CorrespondeAoRegistro(string? registro) =>
        string.Equals(Registro, registro?.Trim(), StringComparison.OrdinalIgnoreCase);

    public string DescricaoRegistro() =>
        $"O professor {Nome} faz parte do departamento de {Departamento} e tem a titulação de {Titulacao} sobre o registro {Registro}";
}
