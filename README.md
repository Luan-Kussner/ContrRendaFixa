Contratação de Renda Fixa

contrRendaFixa é um sistema desenvolvido em .NET 8.0 para gerenciar contratações de produtos financeiros com base em regras de negócio específicas.
O projeto inclui uma API que interage com um banco de dados PostgreSQL e um worker que realiza o cálculo de pagamentos das contratações diariamente.

Funcionalidades

Gestão de Contratantes e Produtos: Cadastro e gerenciamento de contratantes e produtos financeiros.
Regras de Contratação: Validação de contratações com base no segmento do contratante e tipo de produto.
Pagamentos: Registro de pagamentos para contratações realizadas.
Worker para Pagamentos Diários: Processamento diário de contratações não pagas, excluindo as que não foram pagas integralmente até o final do dia.

Tecnologias Utilizadas

.NET 8.0
PostgreSQL
Docker (opcional para configuração do ambiente)
Entity Framework Core

Requisitos

Docker (ou instalação direta do PostgreSQL)
.NET SDK 8.0

Configuração do Ambiente

Clone o repositório:
git clone https://github.com/Luan-Kussner/ContrRendaFixa.git
cd contrRendaFixa

Configuração do Banco de Dados:

Crie uma base de dados PostgreSQL.
Execute o script database.sql localizado na raiz do projeto.

Configuração da API:

Configure a string de conexão no appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=seu_banco;Username=seu_usuario;Password=sua_senha"
}

Executando a Aplicação:

Com o .NET SDK instalado, execute:
dotnet build
dotnet run

Usando Docker (opcional):

Para rodar a aplicação e o banco de dados com Docker, use:
docker-compose up

Regras de Negócio

Horário de Contratação: Operações permitidas apenas entre 10:30 e 16:00, de segunda a sexta-feira.
Tipos de Produtos Permitidos: Regras específicas para segmentos Varejo, Atacado e Especial.
Bloqueio de Contratantes e Produtos: Contratantes e produtos podem ser bloqueados, impedindo novas contratações.
Agrupamento de Contratações: O mesmo produto contratado várias vezes no dia é agrupado, atualizando apenas a quantidade.
Pagamentos: Contratações devem ser pagas no mesmo dia; caso contrário, serão excluídas no dia seguinte.

Contribuição

Fork o projeto
Crie uma nova branch (git checkout -b feature/nova-feature)
Commit suas mudanças (git commit -m 'Adiciona nova feature')
Faça o push para a branch (git push origin feature/nova-feature)
Crie um Pull Request
