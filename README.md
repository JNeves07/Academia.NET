# Sistema Acadêmico

Sistema de console para cadastro e consulta de **alunos** e **professores**, refatorado com Arquitetura Limpa. As regras de negócio ficam no domínio, os casos de uso na camada de aplicação e a interface de console apenas coordena a interação com o usuário.

## Tecnologias

- **.NET 10** (`net10.0`) e **C#**
- **xUnit** para testes automatizados
- **Docker** para executar a aplicação em contêiner
- **GitHub Actions** para integração contínua (CI)

## Arquitetura

```text
Sistema-Academico/
├── docs/original/                       # Código-fonte original preservado
├── src/
│   ├── SistemaAcademico.Domain/         # Entidades, validações, interfaces e exceções
│   │   ├── Pessoa.cs
│   │   ├── Aluno.cs
│   │   ├── Professor.cs
│   │   ├── Validacao.cs
│   │   ├── ExcecoesDeDominio.cs
│   │   └── Interfaces.cs
│   ├── SistemaAcademico.Application/    # Serviços e repositórios em memória
│   │   ├── ServicoAcademico.cs
│   │   └── RepositorioEmMemoria.cs
│   └── SistemaAcademico.Cli/            # Program.cs enxuto + menu de console
│       ├── Program.cs
│       └── MenuConsole.cs
├── tests/SistemaAcademico.Tests/        # Testes xUnit
├── .github/workflows/ci.yml
├── Dockerfile
└── SistemaAcademico.sln
```

### Regras de negócio

- Nome obrigatório, com 3 a 120 caracteres.
- CPF com 11 dígitos válidos; e-mail com formato válido e normalizado.
- Data de nascimento não pode estar no futuro nem ser anterior a 1900.
- Matrícula: 6 a 20 caracteres alfanuméricos, normalizada em maiúsculas e única no cadastro.
- Registro do professor: 4 a 20 caracteres alfanuméricos, normalizado em maiúsculas e único no cadastro.
- Carga horária do professor: de 1 a 80 horas semanais.
- Matrículas e registros duplicados geram `CadastroDuplicadoException`.

## Como executar

Pré-requisito: [.NET SDK 10.0](https://dotnet.microsoft.com/download).

```bash
dotnet run --project src/SistemaAcademico.Cli
```

O menu permite cadastrar alunos e professores, buscar por matrícula/registro, listar os cadastros e sair.

### Usando Docker

```bash
docker build -t sistema-academico .
docker run --rm -it sistema-academico
```

O parâmetro `-it` é necessário porque a aplicação lê entradas do console.

## Testes

```bash
dotnet test SistemaAcademico.sln
```

Os testes cobrem validações de nome, CPF, e-mail, data, matrícula e carga horária, além de cadastro, listagem ordenada, busca (inclusive ignorando caixa), remoção e regras de duplicidade.

## Integração contínua

O workflow `.github/workflows/ci.yml` instala o .NET 10, restaura dependências, compila a solução em `Release` e executa `dotnet test` em cada `push` e `pull request` para `main`/`master`.

## Histórico

Os arquivos primitivos (`Pessoa.cs`, `Aluno.cs`, `Professor.cs`, `Program.cs`, projeto e solução antigos) foram preservados em `docs/original/` antes da refatoração.
