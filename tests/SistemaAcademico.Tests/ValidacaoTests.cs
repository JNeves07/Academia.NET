using SistemaAcademico.Domain;

namespace SistemaAcademico.Tests;

public class ValidacaoTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ValidarNome_DeveRejeitarNomesVazios(string? nome) =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarNome(nome));

    [Fact]
    public void ValidarNome_DeveRejeitarNomeMuitoCurto() =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarNome("Jo"));

    [Fact]
    public void ValidarNome_DeveApararEspacos()
    {
        Assert.Equal("João Silva", Validacao.ValidarNome("  João Silva  "));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("111.111.111-11")]
    [InlineData("abc.def.ghi-jk")]
    public void ValidarCpf_DeveRejeitarCpfInvalido(string cpf) =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarCpf(cpf));

    [Fact]
    public void ValidarCpf_DeveAceitarCpfFormatado()
    {
        Assert.Equal("12345678909", Validacao.ValidarCpf("123.456.789-09"));
    }

    [Theory]
    [InlineData("SemArroba")]
    [InlineData("a@b")]
    [InlineData("a @b.com")]
    public void ValidarEmail_DeveRejeitarEmailInvalido(string email) =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarEmail(email));

    [Fact]
    public void ValidarEmail_DeveNormalizarParaMinusculas()
    {
        Assert.Equal("ana@escola.br", Validacao.ValidarEmail("ANA@ESCOLA.BR"));
    }

    [Fact]
    public void ValidarDataNascimento_DeveRejeitarDataFutura() =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarDataNascimento(DateTime.Today.AddDays(1)));

    [Fact]
    public void ValidarDataNascimento_DeveRejeitarDataMuitoAntiga() =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarDataNascimento(new DateTime(1899, 12, 31)));

    [Theory]
    [InlineData("ABC")]
    [InlineData("123")]
    [InlineData("ABC-123")]
    public void ValidarMatricula_DeveRejeitarMatriculaInvalida(string matricula) =>
        Assert.Throws<ArgumentException>(() => Validacao.ValidarMatricula(matricula));

    [Fact]
    public void ValidarMatricula_DeveNormalizarParaMaiusculas()
    {
        Assert.Equal("2026A001", Validacao.ValidarMatricula("2026a001"));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(81)]
    public void ValidarCargaHoraria_DeveRejeitarForaDoLimite(int carga) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Validacao.ValidarCargaHoraria(carga));

    [Theory]
    [InlineData(1)]
    [InlineData(40)]
    [InlineData(80)]
    public void ValidarCargaHoraria_DeveAceitarLimites(int carga)
    {
        Assert.Equal(carga, Validacao.ValidarCargaHoraria(carga));
    }
}
