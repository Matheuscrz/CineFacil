# Use a imagem base do PostgreSQL
FROM postgres:latest

# Defina as variáveis de ambiente para o PostgreSQL
ENV POSTGRES_DB=cf
ENV POSTGRES_USER=cf
ENV POSTGRES_PASSWORD=cinefacil

# Copie o arquivo SQL de inicialização para o diretório de inicialização do PostgreSQL
COPY ./schema/*.sql /docker-entrypoint-initdb.d/

# Exponha a porta padrão do PostgreSQL
EXPOSE 5432
