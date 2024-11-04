# SistemaGerenciamento

## Tela Inicial
Na tela inicial, o usuário pode escolher entre fazer um **pedido** ou uma **reserva**. Para realizar qualquer uma dessas ações, é necessário estar logado no sistema.

## Confirmação de E-mail e Redefinição de Senha
### Confirmação de E-mail
- Após o registro, o usuário recebe um e-mail com um link para confirmar sua conta.
- A confirmação é necessária para que o usuário possa fazer login no sistema.
- Existe a opção de reenviar o e-mail de confirmação caso o usuário não o tenha recebido.
- **Nota**: o usuário administrador (admin@teste.com) não precisa de confirmação de e-mail para facilitar o gerenciamento do sistema.

### Redefinição de Senha
- Se o usuário esquecer sua senha, ele pode solicitar uma redefinição.
- Um link de redefinição de senha é enviado para o e-mail cadastrado.
- Ao clicar no link, o usuário pode criar uma nova senha.

## Funcionalidades do Usuário Comum
### Pré-pedido
Após fazer login, o usuário pode criar um **Pré-pedido**, onde:
- É possível **criar** um pré-pedido com os itens que deseja consumir, ajudando o restaurante a ter ciência do pedido e o cliente a saber os valores.
- O usuário pode **atualizar**, **verificar detalhes** e **deletar** o pré-pedido.

### Reservas
A criação de reservas possui algumas validações específicas:
- Não é possível criar uma reserva com o número de pessoas maior que o número de cadeiras disponíveis na mesa selecionada.
- A quantidade de pessoas deve ser maior que zero.
- Não é permitido criar uma reserva na mesma mesa e horário de uma reserva já existente.

## Funcionalidades do Administrador
Ao logar como administrador, é possível acessar as **telas administrativas** de **Itens**, **Categorias** e **Mesas**. 

### Gerenciamento de Itens
- Um **item** possui uma categoria associada e pode ser gerenciado pelo administrador.
- É possível realizar o **CRUD completo** dos itens, incluindo o upload de imagens para ilustrar os itens no menu.
- No modo de edição, o administrador também pode **atualizar a imagem** de um item.

### Gerenciamento de Mesas
- O administrador pode **criar mesas** e definir a quantidade de cadeiras disponíveis para cada uma, assegurando que a configuração das mesas esteja alinhada com a capacidade de atendimento do restaurante.

## Validações Importantes
- **Confirmação de E-mail**: Necessária para login (exceto para o administrador).
- **Redefinição de Senha**: Link de redefinição enviado por e-mail.
- **Pré-pedido**: O usuário pode visualizar, atualizar, deletar ou consultar os detalhes de um pré-pedido.
- **Reserva**:
  - Número de pessoas não pode exceder o número de cadeiras na mesa.
  - Quantidade de pessoas deve ser maior que zero.
  - Não é possível reservar a mesma mesa no mesmo horário.

## Tecnologias Utilizadas
- ASP.NET Framework para o desenvolvimento da aplicação.
- Entity Framework para ORM e mapeamento de banco de dados.
- Gmail API para o envio de e-mails de confirmação e redefinição de senha.
