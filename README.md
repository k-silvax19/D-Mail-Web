# D-Mail 📅

Um agendador de e-mails desenvolvido em **C# e .NET**, inspirado conceitualmente nos D-Mails de *Steins;Gate*.

A aplicação permite criar, agendar, acompanhar e cancelar mensagens de e-mail, utilizando processamento em segundo plano para realizar os envios automaticamente.

> **Nota:** a referência a *Steins;Gate* é exclusivamente temática. O projeto não utiliza personagens, logotipos, imagens ou trechos da obra.

## ✨ Funcionalidades

* 📧 Criação e agendamento de D-Mails
* 📅 Agendamento único ou diário
* ❌ Cancelamento de mensagens agendadas
* 🔄 Controle dos estados de envio
* ⏱️ Processamento automático em segundo plano
* 🔐 Proteção das credenciais SMTP utilizando **ASP.NET Data Protection**
* 💾 Persistência local com **Entity Framework Core + SQLite**
* 🧪 Testes unitários para as principais regras de negócio
* 🖥️ Interface web desenvolvida com **ASP.NET MVC**

## 🏗️ Arquitetura

O projeto utiliza uma arquitetura organizada em camadas, separando responsabilidades entre domínio, aplicação, infraestrutura e apresentação:

```text
D-Mail
├── src
│   ├── DMail.Domain
│   │   └── Regras e entidades do domínio
│   │
│   ├── DMail.Application
│   │   └── Casos de uso e regras de aplicação
│   │
│   ├── DMail.Infra
│   │   └── Persistência, EF Core e serviços externos
│   │
│   └── DMail.WebApp
│       └── Interface ASP.NET MVC
│
└── tests
    └── Testes automatizados
```

Essa separação facilita a manutenção do código e permite que as regras de negócio permaneçam independentes da interface e dos detalhes de infraestrutura.

## 🛠️ Tecnologias

| Tecnologia                | Utilização                      |
| ------------------------- | ------------------------------- |
| **C#**                    | Linguagem principal             |
| **.NET**                  | Plataforma da aplicação         |
| **ASP.NET Core MVC**      | Interface web                   |
| **Entity Framework Core** | ORM e persistência              |
| **SQLite**                | Banco de dados local            |
| **SMTP / TLS**            | Envio dos e-mails               |
| **Data Protection**       | Proteção das credenciais        |
| **Testes unitários**      | Validação das regras de negócio |

## ⚙️ Como executar

### Pré-requisitos

* [.NET SDK](https://dotnet.microsoft.com/download)
* Uma conta de e-mail com acesso SMTP, caso queira testar um envio real.

### 1. Clonar o repositório

```bash
git clone https://github.com/k-silvax19/D-Mail-Web.git
cd D-Mail-Web
```

### 2. Restaurar as dependências

```bash
dotnet restore
```

### 3. Compilar o projeto

```bash
dotnet build DMail.slnx
```

### 4. Executar os testes

```bash
dotnet test DMail.slnx
```

### 5. Aplicar a migration

```bash
dotnet ef database update \
  --project src/DMail.Infra \
  --startup-project src/DMail.WebApp
```

### 6. Executar a aplicação

```bash
dotnet run --project src/DMail.WebApp
```

Após iniciar, acesse a aplicação pelo endereço informado no terminal.

## 💾 Banco de dados

Por padrão, o projeto utiliza **SQLite**, armazenando os dados no arquivo:

```text
dmail.db
```

A configuração da conexão está localizada em:

```text
src/DMail.WebApp/appsettings.json
```

O SQLite foi utilizado para simplificar a execução local, sem exigir a instalação ou configuração de um servidor de banco de dados.

## 📤 Configurando um envio real

Para testar o envio de e-mails:

1. Execute a aplicação.
2. Acesse a área **Remetente**.
3. Informe o endereço de e-mail utilizado para envio.
4. Configure o servidor SMTP.
5. Informe uma **senha de aplicativo**.
6. Crie um D-Mail com uma data futura próxima.

O serviço em segundo plano verifica a fila de mensagens periodicamente e realiza o processamento dos D-Mails agendados.

### Gmail

Para contas Gmail, uma configuração típica é:

```text
Servidor: smtp.gmail.com
Porta: 587
Segurança: TLS
```

É necessário utilizar uma **senha de aplicativo** em vez da senha normal da conta.

> **Importante:** nunca coloque sua senha de e-mail ou senha de aplicativo diretamente no `appsettings.json` ou no código-fonte.

## 🔐 Segurança

As credenciais utilizadas para autenticação no servidor SMTP são protegidas utilizando o **ASP.NET Core Data Protection** antes de serem armazenadas.

Em ambiente de desenvolvimento local, isso permite manter as credenciais protegidas sem expô-las diretamente no banco de dados.

Para ambientes de produção, as chaves do Data Protection devem ser armazenadas em um local seguro e persistente, especialmente quando houver múltiplas instâncias da aplicação.

## ⏱️ Processamento em segundo plano

O envio dos D-Mails é realizado por um serviço executado em segundo plano.

O processo:

```text
D-Mail agendado
       ↓
Banco de dados
       ↓
Serviço em Background
       ↓
Verificação da fila
       ↓
Envio via SMTP/TLS
       ↓
Atualização do estado
```

A fila é verificada ao iniciar a aplicação e posteriormente em intervalos de **30 segundos**.

## 🧪 Testes

O projeto possui testes automatizados voltados principalmente para as regras fundamentais do domínio.

Para executar:

```bash
dotnet test DMail.slnx
```

Os testes ajudam a garantir que alterações na aplicação não quebrem comportamentos já implementados.

## 🚧 Próximos passos

Algumas melhorias planejadas para o projeto:

* [ ] Implementar autenticação de usuários
* [ ] Adicionar histórico detalhado das tentativas de envio
* [ ] Implementar política de retry para falhas temporárias
* [ ] Melhorar o gerenciamento da fila de mensagens
* [ ] Preparar configuração para ambientes de produção
* [ ] Persistir as chaves do ASP.NET Data Protection em armazenamento compartilhado
* [ ] Expandir a cobertura de testes automatizados

## 🧰 Ferramenta recomendada

Para quem utiliza Visual Studio, o **EF Core Power Tools** pode facilitar o trabalho com Entity Framework Core.

A extensão permite visualizar o modelo do banco de dados e auxilia em tarefas relacionadas ao EF Core.

* [EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
* [Documentação oficial do EF Core](https://learn.microsoft.com/pt-br/ef/core/)

## 👨‍💻 Autor

**Kauan da Silva**

Projeto desenvolvido como parte dos estudos e prática de desenvolvimento **C# / .NET**, com foco em arquitetura, persistência de dados, processamento em segundo plano, segurança e testes automatizados.

---

⭐ Se este projeto foi útil para você, considere deixar uma estrela no repositório!
