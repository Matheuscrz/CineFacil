# CineFácil - A Plataforma Intuitiva para Compra de Ingressos de Cinema

## Descrição

CineFácil é uma plataforma inovadora que facilita a compra de ingressos de cinema de forma rápida e intuitiva. Nosso objetivo é proporcionar a melhor experiência para os amantes de cinema, permitindo que adquiram seus ingressos com facilidade e segurança.

## Funcionalidades

- **Compra de ingressos online**: Permite aos usuários comprar ingressos para filmes em cartaz.
- **Visualização de horários e sessões disponíveis**: Exibe os horários e sessões de todos os filmes disponíveis.
- **Seleção de assentos**: Oferece a opção de escolher os assentos preferidos no cinema.
- **Pagamento seguro**: Integração com sistemas de pagamento seguros para garantir a proteção dos dados dos usuários.
  [EM DISCUSSAO] - **Recebimento de ingressos digitais**: Envia os ingressos comprados diretamente para o e-mail.

## Tecnologias Utilizadas

- **Frontend**: Considerando o uso de VueJS
- **Backend**: Considerando o uso de C#
- **Banco de Dados**: Considerando o uso do PostgreSQL
- **Conteinerização**: Docker

## Como Executar

Para executar a aplicação, siga os passos abaixo:

1. Certifique-se de que você tem o Docker instalado em sua máquina. Você pode baixar e instalar o Docker a partir do [site oficial](https://www.docker.com/get-started).

2. Navegue até o diretório `docker` do projeto:

   ```sh
   cd docker
   ```

3. Execute o comando `docker-compose up --build` para iniciar os containers.

4. Aguarde até que todos os contêineres sejam iniciados e estejam prontos para uso.

5. Teste as rotas da API utilizando uma ferramenta de teste de sua preferência:

   - Registrar Cliente:

     - URL: `http://localhost:5000/api/Client/register`
     - Método: `POST`
     - Corpo da Requisição (JSON):
       ```sh
       {
           "nome": "string",
           "email": "string",
           "senha": "string",
           "cpf": "string",
           "dataNascimento": "2024-10-10T18:09:41.256Z",
           "telefone": "string"
       }
       ```

   - Login de Usuário:

     - URL: `http://localhost:5000/api/User/login`
     - Método: `POST`
     - Corpo da Requisição (JSON):
       ```sh
       {
           "email": "string",
           "senha": "string"
       }
       ```

   - Logout de Usuário:
     - URL: `http://localhost:5000/api/User/logout`
     - Método: `POST`
     - Corpo da Requisição (JSON):
       ```sh
       {
           "token"
       }
       ```

## Estrutura do Projeto

- **/docs**: Contém a documentação do projeto.
- **/docker**/: Contém os arquivos de composição do container.
- **/backend**/: Contém o código fonte do backend.

## Requisitos do Sistema

- **Sistema Operacional**: Windows, macOS ou Linux
- **Docker**: Necessário para a execução dos containers.

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [Licença](./LICENSE) para mais detalhes.
