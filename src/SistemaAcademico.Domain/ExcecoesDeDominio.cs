namespace SistemaAcademico.Domain;

/// <summary>Base para erros que violam regras de negócio do sistema.</summary>
public class ExcecaoDeDominio : Exception
{
    public ExcecaoDeDominio(string mensagem) : base(mensagem)
    {
    }
}

/// <summary>Lançada quando matrícula ou registro já está cadastrado.</summary>
public sealed class CadastroDuplicadoException : ExcecaoDeDominio
{
    public CadastroDuplicadoException(string mensagem) : base(mensagem)
    {
    }
}

/// <summary>Lançada quando uma consulta estrita não encontra o registro solicitado.</summary>
public sealed class RegistroNaoEncontradoException : ExcecaoDeDominio
{
    public RegistroNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}
