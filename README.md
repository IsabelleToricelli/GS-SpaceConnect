# SpaceConnect - Monitoramento Agrícola Espacial

## Resumo da Solução e Relação com a Indústria Espacial
O **SpaceConnect** é um ecossistema de software desenvolvido em C# com .NET 8 que visa aproximar a tecnologia de monitoramento orbital da agricultura de precisão. O módulo desktop (WPF) simula o recebimento, persistência e análise de telemetrias capturadas por sensores terrestres integrados a sistemas de microssatélites. 

Na arquitetura da **Indústria Espacial**, o software atua na camada de processamento de dados (Ground Segment). Ele recebe informações críticas sobre variáveis geoambientais coletadas remotamente e, por meio de um motor de regras inteligente baseado em JSON, gera diagnósticos preditivos e recomendações acionáveis para otimização de safras, mitigação de riscos climáticos e gerenciamento eficiente de propriedades rurais.

## ODS Relacionada (Objetivos de Desenvolvimento Sustentável)
Este projeto está diretamente alinhado com a **ODS 2: Fome Zero e Agricultura Sustentável**. Ao fornecer análises em tempo real de temperatura, umidade e luminosidade, o SpaceConnect permite o uso racional de recursos hídricos, aplicação controlada de insumos e detecção precoce de anomalias climáticas. Isso promove uma produção alimentar mais resiliente, sustentável e eficiente, mitigando os impactos das mudanças climáticas no campo.

---

##  Decisões Técnicas de Arquitetura

1. **Camada de Visão (WPF / XAML):** Implementação de uma interface rica, responsiva e focada na experiência do usuário (UX), adotando estilos personalizados, feedback visual instantâneo para operações do CRUD e tratamento dinâmico de estados.
2. **Camada de Dados (Entity Framework Core - Code First):** Utilização do EF Core para mapeamento objeto-relacional (ORM) com o banco de dados MySQL. A estrutura garante consistência de chaves estrangeiras e isolamento total da camada de persistência em um `DbContext` dedicado.
3. **Desacoplamento de Regras de Negócio (Service Pattern):** Criação do `AnalisadorAgricoloService` para isolar a lógica de diagnóstico e leitura de arquivos de configuração, evitando acoplamento excessivo no Code-Behind das janelas (arquivos `.xaml.cs`).
4. **Armazenamento de Configurações Dinâmicas (JSON):** Utilização de arquivo estruturado `regras_agricolas.json` para parametrizar as faixas aceitáveis de leitura climática por região, garantindo manutenibilidade sem a necessidade de recompilar o código.

---

## Pacotes NuGet Utilizados
* `Microsoft.EntityFrameworkCore` (v8.x ou superior) - Gerenciamento do ORM.
* `Microsoft.EntityFrameworkCore.Design` (v8.x ou superior) - Suporte às ferramentas de design em tempo de desenvolvimento.
* `Microsoft.EntityFrameworkCore.Tools` (v8.x ou superior) - Execução dos comandos de console do EF Core.
* `Pomelo.EntityFrameworkCore.MySql` (v8.x ou superior) - Provedor oficial para integração do EF Core com o banco de dados MySQL.
* `System.Text.Json` - Serialização e desserialização eficiente das regras de diagnóstico.

---

## Integrantes do Grupo
**Gilson Dias Ramos Junior** - RM552345
**Isabelle Toricelli da Silva** - RM552806
**Jeferson Gabriel de Mendonça** - RM553149

---

##  Como Rodar o Projeto

### 1. Pré-requisitos
* .NET 8 SDK instalado.
* IDE Visual Studio 2022 ou VS Code.
* Servidor MySQL ativo (Localhost, XAMPP ou WampServer).

### 2. Configuração do Banco de Dados
Abra o arquivo `AppDbContext.cs` localizado na pasta `Data` e certifique-se de ajustar a sua string de conexão com as credenciais do seu ambiente local:

```csharp
string conexao = "Server=localhost;Database=GsSpaceConnectDb;Uid=root;Pwd=sua_senha;";
