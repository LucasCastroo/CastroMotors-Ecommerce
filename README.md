
Seguindo as instruções do trabalho irei explicar de forma resumida o que eu fiz nesta aplicação e o que consegui implementar:

* A minha aplicação é no contexto de um E-commerce de uma loja de Carros, na qual o usuário consegue se registrar utilizando um email qualquer para ser armazenado de forma local ou com autenticação do Google.

* Só é possível adicionar um carro a garagem do usuário se ele estiver autenticado.
* As telas públicas são 
	- Tela Home que contém as opções dos carros possibilitando fazer um filtragem.
	- Tela Sobre que conta faz um breve resumo fictício do contexto da loja.
	- Além das telas públicas de recuperação de senha, login, register e etc.

* As minhas telas privadas são as de CRUDs
	- CRUD Carro
	- CRUD Marca
	- CRUD Categoria
- Essas telas são protegidas para apenas o usuário administrador ter acesso.

* Criei um usuário Administrador com email "administrador@gmail.com" e senha "Adm123@", que somente este acesso tem acesso as telas de CRUDs.

* Criei um tela de proteção da tela na qual o usuário não tenha acesso, caso ele tente acessar via URL.

* Tive que retirar a conexão com a autenticação do Google para poder efetuar o commit, por tanto, para que funcione, no arquivo Startup.Auth.cs coloque dentro das chaves do comentário do Google o seguintes comandos:

// Google Authentication Configuration
app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
{
    ClientId = "586301148925-55ffji08vnclb87sd12mn869oqfv71ff.apps.googleusercontent.com",
    ClientSecret = "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",
    CallbackPath = new PathString("/signin-google")
}); 


* Em todos os meus CRUDS existem validações para a criação de cada item, além da constante validação nas ações para ver se o usuário logado pode fazer tais ações.

* Minha conexão com banco de dados foi feita corretamente, armazenando todos os dados que são inseridos na aplicação utilizando o ORM Entity.

* Vou deixar disponível também aqui o meu diagrama de Classes de como ficou na minha aplicação.

* Com o usuário adicionando os carros desejados a sua garagem, ele consegue ter um resumo de preço de todos os carros que foram adicionado e é até esta etapa do processo que consegui concluir.



