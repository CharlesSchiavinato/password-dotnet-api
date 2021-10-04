# password-dotnet-api

Projeto criado em .Net 5
https://dotnet.microsoft.com/download/dotnet/5.0

Para executar o projeto precisa acessar a pasta /Password.Api e executar o comando dotnet run

Abrir o navegador e infomar a URL https://localhost:5001/swagger/

Para executar os testes precisa acessar a pasta /Password.UnitTests e executar o comando dotnet test

As políticas da força da senha são configuráveis através do appsettings.json e estão com a configuração padrão. Dessa forma é possível alterar facilmente a configuração da força da senha. Os testes também levam em consideração as configurações da força da senha.

A classe Password é static porque as políticas da força da senha estão definidas no appsettings.json. 

O projeto já tem as configurações necessárias para poder executar no VSCode.
