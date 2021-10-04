# password-dotnet-api

### Execução do Projeto
Projeto criado em .Net 5

https://dotnet.microsoft.com/download/dotnet/5.0

Para executar o projeto precisa acessar a pasta /Password.Api e executar o comando dotnet run

Abrir o navegador e infomar a URL https://localhost:5001/swagger/

Para executar os testes precisa acessar a pasta /Password.UnitTests e executar o comando dotnet test

### Políticas da Força da Senha
Totalmente configuráveis através do appsettings.json. Inclusive quais são os caracteres especiais aceitos e se permite ou não a repetição de caracteres.

Utilizando a configuração padrão informada. 

Facilidade na alteração do padrão da força da senha. 

Os testes validam cada política individualmente levando em consideração as configurações.

### classe static
A classe Password é static porque as políticas da força da senha estão definidas no appsettings.json. 

### VSCode
O projeto já tem as configurações necessárias para poder executar no VSCode.
