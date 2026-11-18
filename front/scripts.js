const botaoConsultar = document.querySelector("#consultar-material");
const buscarID = document.querySelector("#detalhe-id");


botaoConsultar.addEventListener("click", function () {
    let idDigitado = Number(buscarID.value);

    const urlBuscarId = "https://localhost:7000/api/Materiais/" + idDigitado;

    fetch(urlBuscarId)
        .then(function (resposta) {
            if (!resposta.ok) throw new Error("Material não encontrado.");
            return resposta.json();
        })
        .then(function (dados) {
            saida.textContent = JSON.stringify(dados, null, 2);
            status.textContent = "GET realizado com sucesso!";
            status.className = "alert alert-success mt-3";
        })
        .catch(function (erro) {
            saida.textContent = erro.message
            status.textContent = "A API respondeu com erro ou não foi feito nenhum cadastro!";
            status.className = "alert alert-danger mt-3"
        })
})
/* Criar Livro */
const status = document.querySelector("#status");
const saida = document.querySelector("#saida");
const formLivro = document.querySelector("#form-livro");
const livroTitulo = document.querySelector("#livro-titulo");
const livroAno = document.querySelector("#livro-ano");
const livroAutor = document.querySelector("#livro-autor");
const livroPaginas = document.querySelector("#livro-paginas");

const API_URL_LIVRO = "https://localhost:7000/api/Materiais/livro";

formLivro.addEventListener("submit", function (evento) {
    evento.preventDefault();

    const NovoLivro = {
        Titulo: livroTitulo.value,
        AnoPublicacao: Number(livroAno.value),
        Autor: livroAutor.value,
        Paginas: Number(livroPaginas.value)
    }

    fetch(API_URL_LIVRO, {
        method: "POST",
        body: JSON.stringify(NovoLivro),
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        }
    })
        .then(function (resposta) {
            if (!resposta.ok) throw new Error("A API respondeu com ERRO.");
            return resposta.json();
        })
        .then(function (dados) {
            saida.textContent = JSON.stringify(dados, null, 2);
            status.textContent = "POST realizado com sucesso."
            status.className = "alert alert-success mt-3"
        })
        .catch(function (erro) {
            saida.textContent = erro.message;
            status.textContent = "Não foi possível realizar o cadastro";
            status.className = "alert alert-danger mt-3"
        })

})

/* Criar Revista */
const formRevista = document.querySelector("#form-revista");
const revistaTitulo = document.querySelector("#revista-titulo");
const revistaAno = document.querySelector("#revista-ano");
const revistaEdicao = document.querySelector("#revista-edicao");
const revistaMes = document.querySelector("#revista-mes");

const API_URL_REVISTA = "https://localhost:7000/api/Materiais/revista"

formRevista.addEventListener("submit", function (evento) {
    evento.preventDefault();

    const NovaRevista = {
        Titulo: revistaTitulo.value,
        AnoPublicacao: Number(revistaAno.value),
        Edicao: Number(revistaEdicao.value),
        MesPublicacao: Number(revistaMes.value)
    }

    fetch(API_URL_REVISTA, {
        method: "POST",
        body: JSON.stringify(NovaRevista),
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        }
    })
        .then(function (resposta) {
            if (!resposta.ok) throw new Error("A API respondeu com ERRO.");
            return resposta.json();
        })
        .then(function (dados) {
            saida.textContent = JSON.stringify(dados, null, 2);
            status.textContent = "POST realizado com sucesso."
            status.className = "alert alert-success mt-3"
        })
        .catch(function (erro) {
            saida.textContent = erro.message;
            status.textContent = "Não foi possível realizar o cadastro";
            status.className = "alert alert-danger mt-3"
        })

})

/* Acervo */
const API_URL_MATERIAIS = "https://localhost:7000/api/Materiais";
const listaMateriais = document.querySelector("#lista-materiais");
const totalMateriais = document.querySelector("#total-materiais");
const totalLivros = document.querySelector("#total-livros");
const totalRevistas = document.querySelector("#total-revistas");

// Livro tem "autor"; revista não. É assim que o JS descobre o tipo.
function descobrirTipo(material) {
    if ("autor" in material) return "livro";
    return "revista";
}

function criarCelula(texto) {
    const celula = document.createElement("td");
    celula.textContent = texto;
    return celula;
}

function criarBotao(texto, aoClicar) {
    const botao = document.createElement("button");
    botao.type = "button";
    botao.className = "btn btn-primary";
    botao.textContent = texto;
    botao.addEventListener("click", aoClicar);
    return botao;
}

function carregarAcervo() {
    fetch(API_URL_MATERIAIS)
        .then(function (resposta) {
            if (!resposta.ok) throw new Error("Não foi possível carregar o acervo.");
            return resposta.json();
        })
        .then(function (lista) {
            // A lista vem só com os campos comuns (MaterialBiblioteca).
            // Buscamos cada material pelo id, que vem completo (Livro ou Revista).
            const buscas = lista.map(function (item) {
                return fetch(API_URL_MATERIAIS + "/" + item.id).then(function (resposta) {
                    if (!resposta.ok) throw new Error("Não foi possível carregar o material " + item.id + ".");
                    return resposta.json();
                });
            });
            return Promise.all(buscas);
        })
        .then(function (materiais) {
            listaMateriais.textContent = "";

            let livros = 0;
            let revistas = 0;

            if (materiais.length === 0) {
                const linhaVazia = document.createElement("tr");
                const celula = criarCelula("Nenhum material cadastrado.");
                celula.colSpan = 5;
                linhaVazia.appendChild(celula);
                listaMateriais.appendChild(linhaVazia);
            }

            for (const material of materiais) {
                const tipo = descobrirTipo(material);
                let dadosDoMaterial;

                if (tipo === "livro") {
                    livros++;
                    dadosDoMaterial = "Autor: " + material.autor + " | Páginas: " + material.paginas;
                } else {
                    revistas++;
                    dadosDoMaterial = "Edição: " + material.edicao + " | Mês: " + material.mesPublicacao;
                }

                const linha = document.createElement("tr");
                linha.appendChild(criarCelula(material.titulo));
                linha.appendChild(criarCelula(material.anoPublicacao));
                linha.appendChild(criarCelula(tipo === "livro" ? "Livro" : "Revista"));
                linha.appendChild(criarCelula(dadosDoMaterial));

                const acoes = document.createElement("td");
                acoes.appendChild(criarBotao("Consultar", function () {
                    buscarID.value = material.id;
                    botaoConsultar.click();
                }));
                acoes.appendChild(criarBotao("Editar", function () {
                    carregarEdicao(material);
                    document.querySelector("#edicao").scrollIntoView({ behavior: "smooth" });
                }));
                linha.appendChild(acoes);

                listaMateriais.appendChild(linha);
            }

            totalMateriais.textContent = materiais.length;
            totalLivros.textContent = livros;
            totalRevistas.textContent = revistas;
        })
        .catch(function (erro) {
            saida.textContent = erro.message;
            status.textContent = "Não foi possível carregar o acervo.";
            status.className = "alert alert-danger mt-3"
        })
}

/* Editar Material */
const formEdicao = document.querySelector("#form-edicao");
const edicaoTitulo = document.querySelector("#edicao-titulo");
const edicaoAno = document.querySelector("#edicao-ano");
const edicaoAutor = document.querySelector("#edicao-autor");
const edicaoPaginas = document.querySelector("#edicao-paginas");
const edicaoNumero = document.querySelector("#edicao-numero");
const edicaoMes = document.querySelector("#edicao-mes");
const camposLivro = document.querySelector("#edicao-campos-livro");
const camposRevista = document.querySelector("#edicao-campos-revista");

// Guarda qual material está sendo editado
let idEmEdicao = null;
let tipoEmEdicao = null;

// Esconde os campos específicos até um material ser escolhido
camposLivro.style.display = "none";
camposRevista.style.display = "none";

function carregarEdicao(material) {
    idEmEdicao = material.id;
    tipoEmEdicao = descobrirTipo(material);

    edicaoTitulo.value = material.titulo;
    edicaoAno.value = material.anoPublicacao;

    if (tipoEmEdicao === "livro") {
        edicaoAutor.value = material.autor;
        edicaoPaginas.value = material.paginas;
        camposLivro.style.display = "block";
        camposRevista.style.display = "none";
    } else {
        edicaoNumero.value = material.edicao;
        edicaoMes.value = material.mesPublicacao;
        camposLivro.style.display = "none";
        camposRevista.style.display = "block";
    }
}

formEdicao.addEventListener("submit", function (evento) {
    evento.preventDefault();

    if (idEmEdicao === null) {
        saida.textContent = "Clique em 'Editar' na tabela antes de salvar.";
        status.textContent = "Nenhum material selecionado.";
        status.className = "alert alert-danger mt-3"
        return;
    }

    let MaterialAtualizado;

    if (tipoEmEdicao === "livro") {
        MaterialAtualizado = {
            Titulo: edicaoTitulo.value,
            AnoPublicacao: Number(edicaoAno.value),
            Autor: edicaoAutor.value,
            Paginas: Number(edicaoPaginas.value)
        }
    } else {
        MaterialAtualizado = {
            Titulo: edicaoTitulo.value,
            AnoPublicacao: Number(edicaoAno.value),
            Edicao: Number(edicaoNumero.value),
            MesPublicacao: Number(edicaoMes.value)
        }
    }
