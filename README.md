# Sistema de Gerenciamento de Biblioteca Universitária

## Integrantes
- Adelano Mascarenhas | RGM: 114.027
- Guilherme Campos | RGM: 114.045
- Antonella Scanoni | RGM: 114.042
- Mariana Marques | RGM: 114.071

## Descrição
Web API para gerenciamento de livros e revistas de uma biblioteca universitária. O projeto demonstra herança, abstração, encapsulamento, polimorfismo, interfaces, Repository Pattern, injeção de dependência e Singleton.

## Tecnologias
- C#
- ASP.NET Core Web API (.NET 10)
- Swagger / Swashbuckle
- Dictionary para armazenamento em memória

## Como executar
1. Instale o SDK do .NET 10.
2. Abra um terminal na pasta do arquivo `MinhaBiblioteca.csproj`.
3. Execute:
   dotnet restore
   dotnet run
4. Abra o endereço HTTPS informado no terminal e acrescente `/swagger`.

Os dados são mantidos somente em memória e voltam aos valores iniciais quando a aplicação é reiniciada.

## Endpoints principais

| Método | Rota | Descrição |
|---|---|---|
| GET | `/api/Materiais` | Lista todos os materiais |
| GET | `/api/Materiais/{id}` | Busca material por ID |
| POST | `/api/Materiais/livro` | Cadastra livro |
| POST | `/api/Materiais/revista` | Cadastra revista |
| PUT | `/api/Materiais/livro/{id}` | Atualiza livro |
| PUT | `/api/Materiais/revista/{id}` | Atualiza revista |
| DELETE | `/api/Materiais/{id}` | Exclui material |
| GET | `/api/Materiais/{id}/descricao` | Retorna descrição e prazo |
| POST | `/api/Materiais/{id}/emprestar` | Tenta emprestar o material |

## Exemplos de cadastro

Livro:
```json
{
  "titulo": "Absolute Programer",
  "anoPublicacao": 2026,
  "autor": "Andrey Rodrigues",
  "paginas": 352
}