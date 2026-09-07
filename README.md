# D-Mail

Agendador de e-mails inspirado na ideia de D-Mails de *Steins;Gate*. Esta aplicação não usa personagens, logotipos, imagens ou trechos da obra; a referência é somente temática.

## Estado inicial

- Arquitetura em camadas: `Dominio`, `Aplicacao`, `Infra` e `WebApp`;
- Módulo de D-Mail com validações, agendamento único ou diário, cancelamento e estados de envio;
- Tela MVC para criar e acompanhar transmissões;
- Envio SMTP/TLS em segundo plano, verificado a cada 30 segundos;
- Persistência com EF Core + SQLite local e senha de aplicativo protegida por Data Protection;
- Testes unitários das regras fundamentais.

## Próximos marcos

1. Aplicar a migration inicial incluída no projeto com `dotnet ef database update --project src/DMail.Infra --startup-project src/DMail.WebApp`.
2. Adicionar autenticação, histórico de tentativas e política de novas tentativas.
3. Para produção, persistir as chaves do ASP.NET Data Protection em um local seguro compartilhado entre instâncias.

## Executar

```powershell
dotnet restore
dotnet build DMail.slnx
dotnet test DMail.slnx
dotnet run --project src/DMail.WebApp
```

O padrão de conexão está em `src/DMail.WebApp/appsettings.json` e cria o arquivo local `dmail.db`, dispensando uma instalação de SQL Server. Ajuste-a caso use outro banco.

## Configurar e testar um envio real

1. Execute a migration e inicie o site.
2. Abra **Remetente** e informe o e-mail, servidor SMTP e uma **senha de aplicativo**. Para Gmail: `smtp.gmail.com`, porta `587`, com a verificação em duas etapas ativada.
3. Crie um D-Mail com data futura próxima. O serviço em segundo plano processa a fila ao iniciar e depois a cada 30 segundos.

Não use a senha normal da conta e não a adicione ao `appsettings.json`. A aplicação a criptografa antes de salvar; em ambiente de produção, configure o armazenamento das chaves de Data Protection conforme a infraestrutura escolhida.

## Extensão recomendada

Para Visual Studio, use **EF Core Power Tools**. Ela disponibiliza interface gráfica para tarefas de design do EF Core, incluindo gerar entidades/`DbContext` a partir de banco existente e visualizar diagramas do modelo. É especialmente útil quando o módulo de e-mail crescer e você quiser conferir as tabelas e relacionamentos sem sair do editor.

- [Instalar EF Core Power Tools](https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools)
- [Documentação da Microsoft sobre ferramentas e extensões EF Core](https://learn.microsoft.com/pt-br/ef/core/extensions/)
