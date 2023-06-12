# ThirdAspNetCoreAPIProjectIdentityUser
# **User API / API de Usuários**

This is a RESTful API project that allows user management. It's built in .NET 5.0, using the Repository design pattern to manage user data access.

Este é um projeto de API RESTful que permite o gerenciamento de usuários. É construído em .NET 5.0, usando o padrão de design de Repositório para gerenciar o acesso aos dados do usuário.

**Features / Funcionalidades**

- User Registration: Allows creating new users with a username, birth date, and password.
- Login: Allows users to log in and receive an authentication token.
- Age Verification: The API checks the user's age before allowing access to certain endpoints.
- Cadastro de usuários: Permite criar novos usuários com um nome de usuário, data de nascimento e senha.
- Login: Permite que os usuários façam login e recebam um token de autenticação.
- Verificação de idade: A API verifica a idade dos usuários antes de permitir o acesso a determinados endpoints.

**How to Run**

1. Clone this repository to your local machine.
2. Open the project in Visual Studio.
3. Configure the connection string in appsettings.json to point to your database server.
4. Run the application.

**Como Executar**

1. Clone este repositório para a sua máquina local.
2. Abra o projeto no Visual Studio.
3. Configure a string de conexão no appsettings.json para apontar para o seu servidor de banco de dados.
4. Execute a aplicação.

**Technologies Used / Tecnologias Utilizadas**

- .NET 5.0
- Entity Framework Core
- MySQL
- AutoMapper
- JWT Bearer Authentication
- .NET 5.0
- Entity Framework Core
- MySQL
- AutoMapper
- Autenticação JWT Bearer

**Project Structure / Estrutura do Projeto**

The solution is made up of several projects:

- **UsuariosApi** : This is the API project which contains the controllers and business logic.
- **UsuariosApi.Services** : This project contains the services that the API uses such as the user service and the token service.
- **UsuariosApi.Data** : This project contains the DbContext, the DTOs and the authorization code which is used to verify the user's age.
- **UsuariosApi.Models** : This project contains the data models which represent the structure of the data in the database.
- **UsuariosApi.Authorization** : This project contains the "IdadeMinima" authorization policy that is used to verify the user's age.
- **UsuariosApi.Profiles** : This project contains the AutoMapper profiles that are used to map the DTOs to the data models.

A solução é composta por vários projetos:

- **UsuariosApi** : Este é o projeto da API que contém os controladores e a lógica de negócios.
- **UsuariosApi.Services** : Este projeto contém os serviços que a API utiliza, como o serviço de usuário e o serviço de token.
- **UsuariosApi.Data** : Este projeto contém o DbContext, os DTOs e o código de autorização que é utilizado para verificar a idade do usuário.
- **UsuariosApi.Models** : Este projeto contém os modelos de dados que representam a estrutura dos dados no banco de dados.
- **UsuariosApi.Authorization** : Este projeto contém a política de autorização "IdadeMinima" que é usada para verificar a idade dos usuários.
- **UsuariosApi.Profiles** : Este projeto contém os perfis de AutoMapper que são usados para
