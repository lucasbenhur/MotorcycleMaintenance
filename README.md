
# MotorcycleMaintenance

## Introdução

Sistema de Manutenção de Motos 

Este projeto é uma aplicação baseada em microserviços desenvolvida em .NET Core 8.0, utilizando uma arquitetura limpa e boas práticas de design. A aplicação é composta por 3 microserviços, cada um responsável por uma funcionalidade específica, e um API Gateway Ocelot para roteamento de requisições.

Os microsserviços são:
- **Motos** - Cadastro de Motos, atualização da placa, consulta pela placa ou pelo Id, exclusão de Motos.
- **Entregadores** - Cadastro de Entregadores e upload da foto da CNH.
- **Locação** - Cadastro de locação e devolução de Motos para Entregadores e calculo do cuto total do aluguel na devolução.

## Domínio

### Banco De Dados

O projeto usa MongoDb com a estrutura de collections abaixo:

![image](https://github.com/user-attachments/assets/5aeb1c43-b160-48f7-b90d-c17c6bb3710f)

### Projetos

Arquitetura:

![image](https://github.com/user-attachments/assets/1cec62a2-0118-4c21-a507-35ad02c66723)

### Componentes Principais

1. **Cliente**: Representa o usuário final ou sistema externo que interage com a aplicação.
2. **Ocelot API Gateway**: Age como ponto de entrada único para o cliente, roteando requisições para os microserviços apropriados.
3. **Microserviços**:
   - **Motorcycle API**
   - **Rent API**
   - **DeliveryMan API**
4. **MongoDB**: Utilizado para armazenar os dados das entidades e notificações.
5. **RabbitMQ**: Utilizado para comunicação de eventos.
6. **Docker**: Os serviços são containerizados usando Docker.

### Princípios e Padrões

- **Microservices**: Cada serviço é responsável por uma funcionalidade específica e pode ser implantado de forma independente.
- **Clean Architecture**: Segue os princípios da arquitetura limpa, separando a lógica de negócios, a lógica de aplicação e a lógica de infraestrutura.
- **CQRS**: Command Query Responsibility Segregation para separar operações de leitura e escrita.
- **Event-Driven Architecture**: Utiliza RabbitMQ para comunicação de eventos.
- **Dependency Injection**: Para gerenciar dependências e melhorar a testabilidade.
- **Repository Pattern**: Para abstrair a lógica de acesso aos dados e facilitar a substituição de implementações 

## Rodando o projeto

- Clone esse repositório.
- Abra o terminal na pasta raiz do repositório e execute o comando `docker-compose up --build -d` para compilar os projetos, publicá-los no docker e iniciar os containers.

Após o comando executado com sucesso seu Docker terá a seguinte estrutura de containers:

![image](https://github.com/user-attachments/assets/c88f06d2-7563-476e-9dfe-f24f57b99a68)

## Documentação - Swagger

- Com os containers em execução no docker é possível acessar o swagger de cada microsserviço em:

- http://localhost/motos/swagger/index.html

![image](https://github.com/user-attachments/assets/19acaca9-b9ee-49b5-b92d-486f73a73eb8)

- http://localhost/entregadores/swagger/index.html

![image](https://github.com/user-attachments/assets/bb9db49f-d913-47c0-a19a-e9c5eded9bdf)

- http://localhost/locacao/swagger/index.html

![image](https://github.com/user-attachments/assets/81a64e69-423f-40c4-a029-4e2d68d05a9c)

## Logs

- Os logs são gerados apenas no console da aplicação e não são persistidos.

## Exceções

- Exceções foram tratadas para serem exibidas de forma amigável ao usuário.

## Notificações

- As notificações geradas pelo `Evento Moto Cadastrada` podem ser consultadas na collection `Notifications` no mongoDb

![image](https://github.com/user-attachments/assets/3ccbd90b-80b9-482b-976c-0eecb255077e)

## Imagens CNH

- As imagens de CNH dos entregadores são armazenadas no disco local do container deliveryman-api:

![image](https://github.com/user-attachments/assets/4078f55f-6f12-4440-8fa0-a9b10eef8be9)

### Requisitos

- Docker
- .NET 8
- As seguintes portas tem que estar livres: 80, 27017, 15672, 5672
