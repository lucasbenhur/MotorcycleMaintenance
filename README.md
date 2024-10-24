
# MotorcycleMaintenance

## Introdução

Sistema de Manutenção de Motos 

Este projeto é uma aplicação baseada em microserviços desenvolvida em .NET Core 8.0, utilizando uma arquitetura limpa e boas práticas de design. A aplicação é composta por 3 microserviços, cada um responsável por uma funcionalidade específica, e um API Gateway Ocelot para roteamento de requisições.

Os microsserviços são:
- **Motos** - Cadastro de Motos, atualização da placa, consulta pela placa ou pelo Id ou todos e exclusão.
- **Entregadores** - Cadastro de Entregadores e upload da foto da CNH.
- **Locação** - Cadastro de locação de Motos para Entregadores e devolução, calculo do custo total do aluguel na devolução, consulta de locação pelo Id.

## Domínio

### Banco De Dados

O projeto utiliza MongoDB com a estrutura de collections abaixo:

![image](https://github.com/user-attachments/assets/f6d43f48-83eb-4f54-9c37-2cdb0831115e)

### Arquitetura

![image](https://github.com/user-attachments/assets/e2e009ea-6656-4d19-8be4-67402c9b0d5b)

### Componentes Principais

1. **Cliente**: Representa o usuário final ou sistema externo que interage com a aplicação.
2. **Ocelot API Gateway**: Age como ponto de entrada único para o cliente, roteando requisições para os microserviços apropriados.
3. **Microserviços**:
   - **Motorcycle API**
   - **Rent API**
   - **DeliveryMan API**
4. **MongoDB**: Utilizado para armazenar os dados das entidades, notificações e logs.
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

Após o comando executado com sucesso o Docker deverá ter a seguinte estrutura de containers:

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

- Os logs gerados pela aplicação podem ser consultados na collection `Logs` no banco de dados

![image](https://github.com/user-attachments/assets/c4c1d97f-973d-45cc-add5-dc955f2abede)

## Exceções

- Exceções foram tratadas para serem exibidas de forma amigável ao usuário.

![image](https://github.com/user-attachments/assets/4d0cedbc-3026-495e-939e-ce8150b581cd)

## Notificações

- As notificações geradas pelo `Evento Moto Cadastrada` podem ser consultadas na collection `Notifications` no banco de dados

![image](https://github.com/user-attachments/assets/448f145a-f3a5-47b4-b1a1-2b00043f4a86)

## Imagens CNH

- As imagens de CNH dos entregadores são armazenadas no disco local do container deliveryman-api:

![image](https://github.com/user-attachments/assets/02ff3381-f2a0-4797-b0b8-0592d9daddc2)

### Requisitos

- Docker
- .NET 8
- As seguintes portas tem que estar livres: 80, 27017, 15672, 5672
