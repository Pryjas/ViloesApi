# ViloesApi

API REST de vilões de filmes/séries e suas aparições. Atividade 3 de Sistemas Distribuídos (UTFPR Toledo).

## Como rodar

**Pré-requisito:** [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

```bash
cd src/Presentation/WebAPI
dotnet run
```

- API: `http://localhost:5029/api`
- Documentação interativa (Scalar): `http://localhost:5029/scalar`
- Banco: o arquivo `viloes.db` (SQLite) é criado sozinho na primeira execução. Para zerar, apague o arquivo.

## Rotas

| Verbo | Rota | Sucesso |
|---|---|---|
| GET | `/api/viloes` | 200 |
| GET | `/api/viloes/{id}` | 200 |
| POST | `/api/viloes` | 201 |
| PUT | `/api/viloes/{id}/dados-basicos` · `/maldade` · `/ator` · `/franquia` | 200 |
| DELETE | `/api/viloes/{id}` | 200 |
| GET | `/api/viloes/{id}/aparicoes` | 200 |
| GET | `/api/viloes/{id}/aparicoes/{aparicaoId}` | 200 |
| POST | `/api/viloes/{id}/aparicoes` | 201 |

Erros: **400** campo ausente ou inválido · **404** registro não existe · **422** regra de negócio violada.

Exemplo de corpo para `POST /api/viloes`:

```json
{
  "nome": "Darth Vader",
  "franquia": "Star Wars",
  "ator": "James Earl Jones",
  "ano": 1977,
  "maldade": 9.9
}
```

## Arquitetura

DDD + CQRS em 4 camadas. Cada uma só conhece a de baixo:

```
WebAPI  →  Infra  →  Application  →  Domain
```

| Camada | Pasta | Responsabilidade |
|---|---|---|
| **Domain** | `src/Domain` | Entidades `Vilao` (raiz do agregado) e `Aparicao`. Regras de negócio nos métodos `Pode*`. |
| **Application** | `src/Application` | Commands (escrita) e Queries (leitura) com MediatR. Validators do FluentValidation. |
| **Infra** | `src/Infra` | EF Core + SQLite, repositório, tratamento de erros e pipeline de validação. |
| **WebAPI** | `src/Presentation/WebAPI` | Controllers: recebem a requisição e enviam ao MediatR. |

**Fluxo de uma requisição:**

1. O **Controller** recebe o JSON e envia o Command/Query ao MediatR.
2. O **Validator** confere se os campos vieram e se o registro existe (400/404).
3. O **Handler** carrega a entidade e chama o método do domínio.
4. O **Domínio** valida as regras de negócio (422) e altera o estado.
5. O **UnitOfWork** grava no banco e a resposta volta no formato:

```json
{
  "data": null,
  "succeeded": true,
  "errors": null,
  "message": "Sua mensagem aqui"
}
```
