CREATE TABLE IF NOT EXISTS cineFacil.usuario (
    id SERIAL PRIMARY KEY UNIQUE,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(200) NOT NULL
);
COMMENT ON TABLE cineFacil.usuario IS 'Tabela de Usuários';

CREATE TABLE IF NOT EXISTS cineFacil.cliente (
    id SERIAL PRIMARY KEY UNIQUE,
    cpf VARCHAR(11) NOT NULL UNIQUE,
    dataNascimento DATE NOT NULL,
    telefone VARCHAR(11) NOT NULL,
    usuarioId INTEGER NOT NULL,
    FOREIGN KEY (usuarioId) REFERENCES cineFacil.usuario(id)
);
COMMENT ON TABLE cineFacil.cliente IS 'Tabela de Clientes';

CREATE TYPE cineFacil.cargo AS ENUM('GERENTE', 'ATENDENTE');

CREATE TABLE IF NOT EXISTS cineFacil.administrador (
    id SERIAL PRIMARY KEY UNIQUE,
    usuarioId INTEGER NOT NULL,
    cargo cineFacil.cargo NOT NULL,
    FOREIGN KEY (usuarioId) REFERENCES cineFacil.usuario(id)
);
COMMENT ON TABLE cineFacil.administrador IS 'Tabela de Administradores';

CREATE TABLE IF NOT EXISTS cineFacil.endereco (
    id SERIAL PRIMARY KEY UNIQUE,
    cep VARCHAR(8) NOT NULL,
    rua VARCHAR(50) NOT NULL,
    numero INTEGER NOT NULL,
    complemento VARCHAR(50),
    bairro VARCHAR(50) NOT NULL,
    cidade VARCHAR(50) NOT NULL,
    estado CHAR(2) NOT NULL,
    clienteId INTEGER NOT NULL,
    FOREIGN KEY (clienteId) REFERENCES cineFacil.cliente(id)
);

CREATE TYPE cineFacil.genero AS ENUM('AÇÃO', 'AVENTURA', 'COMÉDIA', 'DRAMA', 'FICÇÃO CIENTÍFICA', 'ROMANCE', 'SUSPENSE', 'TERROR');
CREATE TYPE cineFacil.classificacao AS ENUM('LIVRE', '10 ANOS', '12 ANOS', '14 ANOS', '16 ANOS', '18 ANOS');
CREATE TYPE cineFacil.categoria AS ENUM('ALIMENTO', 'BEBIDA', 'BILHETE', 'OUTRO');
CREATE TYPE cineFacil.tamanho AS ENUM('PEQUENO', 'MÉDIO', 'GRANDE');

CREATE TABLE IF NOT EXISTS cineFacil.filme (
    id SERIAL PRIMARY KEY UNIQUE,
    titulo VARCHAR(100) NOT NULL,
    genero cineFacil.genero NOT NULL,
    classificacao cineFacil.classificacao NOT NULL,
    duracao INTEGER NOT NULL,
    sinopse TEXT,
    dataLancamento DATE NOT NULL,
    diretor VARCHAR(50) NOT NULL,
    poster VARCHAR(100) NOT NULL,
    trailer VARCHAR(100) NOT NULL
);
COMMENT ON TABLE cineFacil.filme IS 'Tabela de Filmes';

CREATE INDEX IF NOT EXISTS idxTitulo ON cineFacil.filme (titulo);

CREATE TABLE IF NOT EXISTS cineFacil.sessao (
    id SERIAL PRIMARY KEY UNIQUE,
    dataHora TIMESTAMP NOT NULL,
    sala INTEGER NOT NULL,
    filmeId INTEGER NOT NULL,
    FOREIGN KEY (filmeId) REFERENCES cineFacil.filme(id)
);
COMMENT ON TABLE cineFacil.sessao IS 'Tabela de Sessões';

CREATE TABLE IF NOT EXISTS cineFacil.poltrona (
    id SERIAL PRIMARY KEY UNIQUE,
    numero INTEGER NOT NULL,
    disponivel BOOLEAN NOT NULL,
    sessaoId INTEGER NOT NULL,
    FOREIGN KEY (sessaoId) REFERENCES cineFacil.sessao(id)
);
COMMENT ON TABLE cineFacil.poltrona IS 'Tabela de Poltronas';

CREATE INDEX IF NOT EXISTS idxSessao ON cineFacil.poltrona (sessaoId);

CREATE TABLE IF NOT EXISTS cineFacil.pedido (
    id SERIAL PRIMARY KEY UNIQUE,
    dataHora TIMESTAMP NOT NULL,
    valor DECIMAL(10, 2) NOT NULL,
    clienteId INTEGER NOT NULL,
    FOREIGN KEY (clienteId) REFERENCES cineFacil.cliente(id)
);
COMMENT ON TABLE cineFacil.pedido IS 'Tabela de Pedidos';

CREATE INDEX IF NOT EXISTS idxPedidoCliente ON cineFacil.pedido (clienteId);

CREATE TABLE IF NOT EXISTS cineFacil.produto (
    id SERIAL PRIMARY KEY UNIQUE,
    nome VARCHAR(50) NOT NULL,
    preco DECIMAL(10, 2) NOT NULL,
    categoria cineFacil.categoria NOT NULL
);
COMMENT ON TABLE cineFacil.produto IS 'Tabela de Produtos';

CREATE TABLE IF NOT EXISTS cineFacil.ingresso (
    id SERIAL PRIMARY KEY UNIQUE,
    produtoId INTEGER NOT NULL,
    sessaoId INTEGER NOT NULL,
    poltronaId INTEGER NOT NULL,
    FOREIGN KEY (produtoId) REFERENCES cineFacil.produto(id),
    FOREIGN KEY (sessaoId) REFERENCES cineFacil.sessao(id),
    FOREIGN KEY (poltronaId) REFERENCES cineFacil.poltrona(id)
);
COMMENT ON TABLE cineFacil.ingresso IS 'Tabela de Ingressos';

CREATE TABLE IF NOT EXISTS cineFacil.alimento (
    id SERIAL PRIMARY KEY UNIQUE,
    produtoId INTEGER NOT NULL,
    tamanho cineFacil.tamanho NOT NULL,
    validade DATE NOT NULL,
    FOREIGN KEY (produtoId) REFERENCES cineFacil.produto(id)
);
COMMENT ON TABLE cineFacil.alimento IS 'Tabela de Alimentos';

CREATE TABLE IF NOT EXISTS cineFacil.bebida (
    id SERIAL PRIMARY KEY UNIQUE,
    produtoId INTEGER NOT NULL,
    tamanho cineFacil.tamanho NOT NULL,
    volume INTEGER NOT NULL,
    FOREIGN KEY (produtoId) REFERENCES cineFacil.produto(id)
);
COMMENT ON TABLE cineFacil.bebida IS 'Tabela de Bebidas';

CREATE TABLE IF NOT EXISTS cineFacil.item_pedido (
    id SERIAL PRIMARY KEY UNIQUE,
    pedidoId INTEGER NOT NULL,
    produtoId INTEGER NOT NULL,
    quantidade INTEGER NOT NULL CHECK (quantidade > 0),
    FOREIGN KEY (pedidoId) REFERENCES cineFacil.pedido(id),
    FOREIGN KEY (produtoId) REFERENCES cineFacil.produto(id)
);
COMMENT ON TABLE cineFacil.item_pedido IS 'Tabela de Itens de Pedido';

CREATE TYPE cineFacil.forma_pagamento AS ENUM('DINHEIRO', 'CARTÃO DE CRÉDITO', 'CARTÃO DE DÉBITO');

CREATE TABLE IF NOT EXISTS cineFacil.pagamento (
    id SERIAL PRIMARY KEY UNIQUE,
    dataHora TIMESTAMP NOT NULL,
    valor DECIMAL(10, 2) NOT NULL,
    formaPagamento cineFacil.forma_pagamento NOT NULL,
    pedidoId INTEGER NOT NULL,
    FOREIGN KEY (pedidoId) REFERENCES cineFacil.pedido(id)
);
COMMENT ON TABLE cineFacil.pagamento IS 'Tabela de Pagamentos';

CREATE TABLE IF NOT EXISTS cinefacil.session_tokens (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    token VARCHAR(500) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES cineFacil.usuario(id)
);