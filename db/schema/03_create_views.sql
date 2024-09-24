CREATE VIEW cineFacil.vw_pedidos_cliente AS
SELECT 
    p.id AS pedido_id,
    p.dataHora AS pedido_dataHora,
    p.valor AS pedido_valor,
    c.id AS cliente_id,
    c.cpf AS cliente_cpf,
    c.telefone AS cliente_telefone,
    pr.id AS produto_id,
    pr.nome AS produto_nome,
    pr.categoria AS produto_categoria,
    pp.quantidade AS produto_quantidade,
    pr.preco AS produto_preco
FROM 
    cineFacil.pedido p
JOIN 
    cineFacil.cliente c ON p.clienteId = c.id
JOIN 
    cineFacil.item_pedido pp ON p.id = pp.pedidoId
JOIN 
    cineFacil.produto pr ON pp.produtoId = pr.id;
COMMENT ON VIEW cineFacil.vw_pedidos_cliente IS 'View de Pedidos com Cliente e Produtos';

CREATE VIEW cineFacil.vw_ingressos_sessao AS
SELECT 
    i.id AS ingresso_id,
    i.sessaoId,
    i.poltronaId,
    p.id AS produto_id,
    p.nome AS produto_nome,
    p.preco AS produto_preco,
    s.dataHora AS sessao_dataHora,
    s.filmeId,
    s.sala
FROM 
    cineFacil.ingresso i
JOIN 
    cineFacil.produto p ON i.produtoId = p.id
JOIN 
    cineFacil.sessao s ON i.sessaoId = s.id;
COMMENT ON VIEW cineFacil.vw_ingressos_sessao IS 'View de Ingressos com Sessão e Produtos';