# Use a imagem base do PostgreSQL
FROM postgres:latest

# Copie o arquivo SQL de inicialização para o diretório de inicialização do PostgreSQL
COPY ./schema/*.sql /docker-entrypoint-initdb.d/

# Exponha a porta padrão do PostgreSQL
EXPOSE 5432
