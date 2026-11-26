using System.Text.RegularExpressions;

namespace SistemaAcademico.Domain;

/// <summary>
/// Regras de validação compartilhadas pelas entidades do domínio acadêmico.
/// </summary>
public static partial class Validacao
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.CultureInvariant)]
    private static partial Regex EmailRegex();

    public static string ValidarNome(string? nome)
    {
        var texto = TextoObrigatorio(nome, "Nome");

        if (texto.Length < 3)
            throw new ArgumentException("O nome deve ter pelo menos 3 caracteres.", nameof(nome));

        if (texto.Length > 120)
            throw new ArgumentException("O nome não pode ultrapassar 120 caracteres.", nameof(nome));

        return texto;
    }

    public static string ValidarCpf(string? cpf)
    {
        var digitos = new string((cpf ?? string.Empty).Where(char.IsDigit).ToArray());

        if (digitos.Length != 11 || digitos.Distinct().Count() == 1)
            throw new ArgumentException("O CPF deve conter 11 dígitos válidos.", nameof(cpf));

        return digitos;
    }

    public static string ValidarEmail(string? email)
    {
        var texto = TextoObrigatorio(email, "E-mail");

        if (!EmailRegex().IsMatch(texto))
            throw new ArgumentException("E-mail inválido.", nameof(email));

        return texto.ToLowerInvariant();
    }

    public static DateTime ValidarDataNascimento(DateTime dataNascimento)
    {
        if (dataNascimento.Date > DateTime.Today)
            throw new ArgumentException("A data de nascimento não pode estar no futuro.", nameof(dataNascimento));

        if (dataNascimento.Year < 1900)
            throw new ArgumentException("A data de nascimento deve ser posterior a 1900.", nameof(dataNascimento));

        return dataNascimento.Date;
    }

    public static string ValidarMatricula(string? matricula)
    {
        var texto = TextoObrigatorio(matricula, "Matrícula").ToUpperInvariant();

        if (texto.Length is < 6 or > 20 || !texto.All(char.IsLetterOrDigit))
            throw new ArgumentException(
                "A matrícula deve ter entre 6 e 20 caracteres alfanuméricos.",
                nameof(matricula));

        return texto;
    }

    public static string ValidarRegistro(string? registro)
    {
        var texto = TextoObrigatorio(registro, "Registro").ToUpperInvariant();

        if (texto.Length is < 4 or > 20 || !texto.All(char.IsLetterOrDigit))
            throw new ArgumentException(
                "O registro deve ter entre 4 e 20 caracteres alfanuméricos.",
                nameof(registro));

        return texto;
    }

    public static int ValidarCargaHoraria(int cargaHoraria)
    {
        if (cargaHoraria is < 1 or > 80)
            throw new ArgumentOutOfRangeException(
                nameof(cargaHoraria),
                cargaHoraria,
                "A carga horária deve estar entre 1 e 80 horas semanais.");

        return cargaHoraria;
    }

    public static string TextoObrigatorio(string? valor, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"{nomeCampo} é obrigatório.", nomeCampo);

        return valor.Trim();
    }
}
