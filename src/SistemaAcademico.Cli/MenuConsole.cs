using System.Globalization;
using SistemaAcademico.Application;
using SistemaAcademico.Domain;

namespace SistemaAcademico.Cli;

/// <summary>Interface de console: lê opções e delega todas as regras ao serviço.</summary>
public static class MenuConsole
{
    private static readonly string[] FormatosDeData = ["dd/MM/yyyy", "yyyy-MM-dd"];

    public static int Executar(ServicoAcademico servico, TextReader? entrada = null, TextWriter? saida = null)
    {
        var reader = entrada ?? Console.In;
        var writer = saida ?? Console.Out;

        writer.WriteLine("============ Bem-vindo ao sistema acadêmico ============");

        while (true)
        {
            EscreverMenu(writer);
            var opcao = reader.ReadLine()?.Trim();

            if (opcao is null)
            {
                writer.WriteLine("Entrada encerrada. Até logo!");
                return 0;
            }

            switch (opcao)
            {
                case "1":
                    CadastrarAluno(servico, reader, writer);
                    break;
                case "2":
                    BuscarAluno(servico, reader, writer);
                    break;
                case "3":
                    ListarAlunos(servico, writer);
                    break;
                case "4":
                    CadastrarProfessor(servico, reader, writer);
                    break;
                case "5":
                    BuscarProfessor(servico, reader, writer);
                    break;
                case "6":
                    ListarProfessores(servico, writer);
                    break;
                case "0":
                    writer.WriteLine("Até logo!");
                    return 0;
                default:
                    writer.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    private static void EscreverMenu(TextWriter writer)
    {
        writer.WriteLine();
        writer.WriteLine("============*************============");
        writer.WriteLine("Selecione uma opção para seguir:");
        writer.WriteLine("1 - Cadastrar aluno");
        writer.WriteLine("2 - Buscar aluno por matrícula");
        writer.WriteLine("3 - Listar alunos");
        writer.WriteLine("4 - Cadastrar professor");
        writer.WriteLine("5 - Buscar professor por registro");
        writer.WriteLine("6 - Listar professores");
        writer.WriteLine("0 - Sair");
        writer.WriteLine("============*************============");
    }

    private static void CadastrarAluno(ServicoAcademico servico, TextReader reader, TextWriter writer)
    {
        try
        {
            var nome = PedirTexto(reader, writer, "Digite o nome do aluno: ");
            var data = PedirData(reader, writer, "Digite a data de nascimento (dd/MM/yyyy): ");
            var cpf = PedirTexto(reader, writer, "Digite o CPF do aluno: ");
            var email = PedirTexto(reader, writer, "Digite o e-mail do aluno: ");
            var matricula = PedirTexto(reader, writer, "Digite a matrícula do aluno: ");
            var curso = PedirTexto(reader, writer, "Digite o curso do aluno: ");
            var turma = PedirTexto(reader, writer, "Digite a turma do aluno: ");

            ExigirPreenchimento(nome, cpf, email, matricula, curso, turma);

            var aluno = new Aluno(
                nome!, data!.Value, cpf!, email!, matricula!, curso!, turma!);

            servico.CadastrarAluno(aluno);
            writer.WriteLine($"Aluno cadastrado com sucesso. {aluno.DescricaoMatricula()}");
        }
        catch (Exception erro) when (erro is ArgumentException or ExcecaoDeDominio)
        {
            writer.WriteLine($"Erro: {erro.Message}");
        }
    }

    private static void BuscarAluno(ServicoAcademico servico, TextReader reader, TextWriter writer)
    {
        var matricula = PedirTexto(reader, writer, "Digite a matrícula do aluno: ");
        if (matricula is not null)
            writer.WriteLine(servico.DescreverAluno(matricula));
    }

    private static void ListarAlunos(ServicoAcademico servico, TextWriter writer)
    {
        var alunos = servico.ListarAlunos();
        writer.WriteLine(alunos.Count == 0
            ? "Nenhum aluno cadastrado."
            : string.Join(Environment.NewLine, alunos.Select(aluno => aluno.DescricaoMatricula())));
    }

    private static void CadastrarProfessor(ServicoAcademico servico, TextReader reader, TextWriter writer)
    {
        try
        {
            var nome = PedirTexto(reader, writer, "Digite o nome do professor: ");
            var data = PedirData(reader, writer, "Digite a data de nascimento (dd/MM/yyyy): ");
            var cpf = PedirTexto(reader, writer, "Digite o CPF do professor: ");
            var email = PedirTexto(reader, writer, "Digite o e-mail do professor: ");
            var registro = PedirTexto(reader, writer, "Digite o registro do professor: ");
            var departamento = PedirTexto(reader, writer, "Digite o departamento: ");
            var titulacao = PedirTexto(reader, writer, "Digite a titulação: ");
            var carga = PedirInteiro(reader, writer, "Digite a carga horária semanal (1 a 80): ");

            ExigirPreenchimento(nome, cpf, email, registro, departamento, titulacao);

            var professor = new Professor(
                nome!, data!.Value, cpf!, email!, registro!, departamento!, titulacao!, carga!.Value);

            servico.CadastrarProfessor(professor);
            writer.WriteLine($"Professor cadastrado com sucesso. {professor.DescricaoRegistro()}");
        }
        catch (Exception erro) when (erro is ArgumentException or ExcecaoDeDominio)
        {
            writer.WriteLine($"Erro: {erro.Message}");
        }
    }

    private static void BuscarProfessor(ServicoAcademico servico, TextReader reader, TextWriter writer)
    {
        var registro = PedirTexto(reader, writer, "Digite o registro do professor: ");
        if (registro is not null)
            writer.WriteLine(servico.DescreverProfessor(registro));
    }

    private static void ListarProfessores(ServicoAcademico servico, TextWriter writer)
    {
        var professores = servico.ListarProfessores();
        writer.WriteLine(professores.Count == 0
            ? "Nenhum professor cadastrado."
            : string.Join(Environment.NewLine, professores.Select(professor => professor.DescricaoRegistro())));
    }

    private static string? PedirTexto(TextReader reader, TextWriter writer, string mensagem)
    {
        writer.Write(mensagem);
        return reader.ReadLine()?.Trim();
    }

    private static DateTime? PedirData(TextReader reader, TextWriter writer, string mensagem)
    {
        var texto = PedirTexto(reader, writer, mensagem);
        return DateTime.TryParseExact(
            texto,
            FormatosDeData,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var data)
            ? data
            : null;
    }

    private static int? PedirInteiro(TextReader reader, TextWriter writer, string mensagem)
    {
        var texto = PedirTexto(reader, writer, mensagem);
        return int.TryParse(texto, out var numero) ? numero : null;
    }

    private static void ExigirPreenchimento(params string?[] campos)
    {
        if (campos.Any(string.IsNullOrWhiteSpace) || campos.Any(campo => campo is null))
            throw new ExcecaoDeDominio("Todos os campos são obrigatórios.");
    }
}
