

using Sistema_Academico;
Aluno aluno = new Aluno();
Console.WriteLine("============ Bem-vindo ao sistema academico ============");
menuPrincipal();

void menuPrincipal()
{
    int opc;
    Console.WriteLine("============*************============");
    Console.WriteLine("Selecione uma opção  para seguir");
    Console.WriteLine("1 - para cadastro de Alunos");
    Console.WriteLine("2 - para busca de alunos por matricula");
    Console.WriteLine("3 - para cadastro de professores");
    Console.WriteLine("4 - para busca de professores por registro");
    Console.WriteLine("============*************============");
    Console.WriteLine("        ");
    opc = int.Parse( Console.ReadLine());

    switch (opc)
    {
        case 1:
            cadastraAluno();
            menuPrincipal();
            break;
        case 2:
            buscaAluno();
            menuPrincipal();
            break;
        case 3:
            break;
        case 4:
            break;
        default:
            Console.WriteLine("Opção invalida");
            break;
    }
}
  void cadastraAluno()
{
    Console.WriteLine("Digite o nome do aluno");
    aluno.Nome = Console.ReadLine();
    Console.WriteLine("Digite a data de nascimento");
    aluno.DataN = DateTime.Parse(Console.ReadLine());
    Console.WriteLine("Digite o Cpf do aluno");
    aluno.Cpf = Console.ReadLine();
    Console.WriteLine("Digite o e-mail do aluno");
    aluno.Email = Console.ReadLine();
    Console.WriteLine("Digite a matricula do aluno");
    aluno.Matricula = Console.ReadLine();
    Console.WriteLine("Digite o curso do aluno");
    aluno.Curso = Console.ReadLine();
    Console.WriteLine("Digite a turma do aluno");
    aluno.Turma = Console.ReadLine();
}
 void buscaAluno()
{
    string _matricula;
    Console.WriteLine("Digite a matricula do aluno");
    _matricula = Console.ReadLine();
    Console.WriteLine($"{aluno.buscarporMatricula(_matricula)}");
}