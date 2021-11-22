# Ferramentaria

## _Gestão de Estoque e Empréstimo de Ferramentas_

Esta web API foi desenvolvida para o Projeto Integrador I do curso de Engenharia de Computação da UNIVESP, com o objetivo de solucionar problemas enfrentados pela Ferramentaria de uma Usina Hidrelétrica de FURNAS, buscando:

- 💻 Facilitar o acesso ao sistema pelos colaboradores da Usina;
- 📃 Simplificar o monitoramento dos empréstimos realizados;
- 📧 Enviar notificações por e-mail após empréstimo e em caso de atraso.

## Tech

As seguintes tecnologias foram utilizadas:

- .NET 5
- ASP .NET Web API
- Microsoft SQL Server
- Entity Framework Core
- OpenAPI / Swagger
- Automapper
- FluentValidation
- SendGrid
- BCrypt
- JWT
- Azure
- Git

## Overview

Esta API é estruturada na N-Layer Architecture, com projetos do tipo biblioteca de classe, além do projeto do tipo API. Apesar de não estar totalmente conforme a Clean Architecture e o Domain Driven Design, buscou-se seguir seus princípios na medida do possível. Além disso, a API conta com um repositório genérico.  
A abordagem do code-first por meio do Entity Framework Core foi utilizada para definir o esquema do banco de dados. Mapeamentos foram feitos com auxílio da biblioteca Automapper e as validações com uso da biblioteca FluentValidation.  
A API possui autenticação e autorização JWT, havendo dois tipos de perfis de acesso: Administrador e Colaborador.  
Ao serem cadastrados, os colaboradores receberão um e-mail para primeiro acesso, contendo um token para que definam sua senha. O login é feito com a matrícula e senha. Troca e recuperação de senha são possíveis.  
Para envio de e-mails foi utilizado o serviço SendGrid. E-mails são enviados para o colaborador em caso de empréstimos realizados (e também para o administrador em caso de empréstimo realizado pelo próprio colaborador) e empréstimos em atraso.  
Colaboradores podem visualizar apenas informações do próprio cadastro e seus próprios empréstimos, além da lista de ferramentas para verificação de disponibilidade.

## User Stories

Por meio de entrevistas e questionários, foram elaboradas as seguintes user stories para guiar o desenvolvimento:

🛠️ **Épico: CF - Cadastro de Ferramentas**

- CF1: Eu, como ferramenteiro, gostaria de poder cadastrar as ferramentas, para ter um controle do estoque das mesmas.  
  **Critérios de aceite:** Ao cadastrar uma nova ferramenta, suas informações de código alfanumérico, descrição, quantidade, valor de compra e local de armazenamento deverão persistir no banco de dados.
- CF2: Eu, como ferramenteiro, gostaria de poder editar os dados das ferramentas cadastradas, para poder corrigir eventuais erros de digitação.  
  **Critérios de aceite:** Ao editar as informações de uma ferramenta, seus dados atualizados devem persistir no banco de dados.
- CF3: Eu, como ferramenteiro, gostaria de poder inativar uma ferramenta cadastrada, para poder saber quais ferramentas não estarão mais disponíveis para empréstimos em caso de perda ou dano irreparável.  
  **Critérios de aceite:** Ao inativar uma ferramenta, uma mensagem de confirmação deverá aparecer, perguntando se o usuário tem certeza da ação que está realizando.
- CF4: Eu, como ferramenteiro/colaborador, gostaria de poder consultar as ferramentas cadastradas, para saber quais estão emprestadas ou não.  
  **Critérios de aceite:** Deve haver uma listagem geral de ferramentas e a possibilidade de busca de ferramenta específica, com informações mais detalhadas. As ferramentas poderão ser buscadas pelo seu código, descrição e podem ser filtradas por status de emprestadas ou disponíveis. Na listagem geral não devem ser exibidas ferramentas que não existem mais (as que se encontram inativas por razão de perna ou dano).

👨‍🔧 **Épico: CC - Cadastro de Colaboradores**

- CC1: Eu, como ferramenteiro, gostaria de poder cadastrar os colaboradores de Furnas e outras prestadoras de serviço, para que possam utilizar a ferramentaria.  
  **Critérios de aceite:** Ao cadastrar um novo colaborador, suas informações de CPF, nome, sobrenome, senha (criptografada), email, telefone, cargo e departamento deverão persistir no banco de dados.
- CC2: Eu, como ferramenteiro, gostaria de poder editar os dados dos colaboradores de Furnas e outras prestadoras de serviço, para poder corrigir eventuais erros de digitação ou atualizar suas informações pessoais.  
  **Critérios de aceite:** Ao editar as informações de um colaborador, seus dados atualizados devem persistir no banco de dados.
- CC3: Eu, como ferramenteiro, gostaria de poder inativar um colaborador cadastrado, para poder saber quais não poderão mais realizar empréstimos, por não trabalharem mais nas empresas autorizadas na ferramentaria ou outro motivo.  
  **Critérios de aceite:** Ao inativar um colaborador, uma mensagem de confirmação deverá aparecer, perguntando se o usuário tem certeza da ação que está realizando.
- CC4: Eu, como ferramenteiro, gostaria de poder consultar os colaboradores cadastrados, para saber quais estão em posse de ferramentas emprestadas ou não.  
  **Critérios de aceite:** Deve haver uma listagem geral de colaboradores e a possibilidade de busca de um colaborador específico, com informações mais detalhadas. Os colaboradores podem ser buscados por CPF, nome, ou filtrados por status de empréstimo em dia ou em atraso. Na listagem geral não devem ser exibidos colaboradores inativos (que não são mais elegíveis para empréstimo).
- CC5: Eu, como colaborador, gostaria de poder consultar minhas informações pessoais, para notificar o ferramenteiro em caso de necessidade de atualização dos meus dados ou não.  
  **Critérios de aceite:** Apenas os dados pessoais do colaborador logado poderão ser consultados.

🧑‍🔧👩‍ **Épico: EM - Empréstimos**

- EM1: Eu, como ferramenteiro, gostaria de poder emprestar as ferramentas para os colaboradores, para viabilizar o seu trabalho.  
  **Critérios de aceite:** Para que empréstimos sejam feitos, deve-se informar a ferramenta, o colaborador, a data de empréstimo e a data de devolução. Para que o empréstimo se concretize, o colaborador deverá digitar sua senha.
- EM2: Eu, como ferramenteiro, gostaria que os colaboradores fossem notificados de um empréstimo, para que possam lembrar-se da data de devolução.  
  **Critérios de aceite:** A notificação deverá ocorrer de forma automática, por meio eletrônico, devendo conter informação da ferramenta, data do empréstimo e data de devolução.
- EM3: Eu, como colaborador, gostaria de poder pegar ferramentas emprestadas em momentos de urgência em que o ferramenteiro não se encontra, para que eu possa realizar meu trabalho.  
  **Critérios de aceite:** Para que empréstimos sejam feitos, deve-se informar a ferramenta e a data de devolução. Para que o empréstimo se concretize, o colaborador deverá digitar sua senha. O ferramenteiro deverá ser notificado por email nesses casos.
- EM4: Eu, como ferramenteiro, gostaria de poder dar baixa nas ferramentas devolvidas, para que voltem a ficar disponíveis para outros colaboradores.  
  **Critérios de aceite:** Deve-se informar o id do empréstimo. Com a devolução, a disponibilidade da ferramenta deverá ser alterada e o estoque dela atualizado.
- EM5: Eu, como ferramenteiro, gostaria que o gerente e o supervisor do colaborador que não devolveu uma ferramenta fossem notificados da situação, para que possam contatá-lo pessoalmente.  
  **Critérios de aceite:** A notificação deverá ocorrer de forma automática, por meio eletrônico, devendo conter informação da ferramenta, do colaborador, data do empréstimo e data de devolução.
- EM6: Eu, como ferramenteiro, gostaria que o colaborador fosse notificado do término do prazo de devolução de uma ferramenta em seu nome, para que possa lembrar de devolvê-la.  
  **Critérios de aceite:** A notificação deverá ocorrer de forma automática, por meio eletrônico, devendo informá-lo do atraso, com informação sobre a ferramenta, data do empréstimo e data de devolução.
- EM7: Eu, como ferramenteiro, gostaria de poder consultar os empréstimos ativos, para saber quem está com as ferramentas, o prazo de devolução e a situação do empréstimo.  
  **Critérios de aceite:** Deverá haver uma listagem geral de todos os empréstimos, com informação da ferramenta e o CPF do colaborador, com data de empréstimo, término e situação (em dia ou em atraso), sendo possível visualizar detalhadamente o empréstimo. Para ferramentas identificadas com código exclusivo, apenas um empréstimo ativo, se houver. Para ferramentas sem código e muitas unidades, poderá aparecer mais de um empréstimo ativo.
- EM8: Eu, como colaborador, gostaria de poder consultar meus empréstimos, para saber se estou em dia com a devolução das ferramentas.  
  **Critérios de aceite:** Apenas os empréstimos do colaborador logado poderão ser consultados.
- EM9: Eu, como ferramenteiro, gostaria de poder consultar o histórico de todos os empréstimos, para fins de auditoria, se necessário.  
  **Critérios de aceite:** Todos os empréstimos, ativos ou concluídos, deverão ser listados, juntamente com a data de entrega, de devolução e situação (em dia, em atraso, concluído).
