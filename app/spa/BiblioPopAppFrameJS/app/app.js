var Constantes = {
    URL_API : 'http://localhost:49720/api'
};

//--------------------------------------------------------------------

var EditoraDTO = function() {
    this.EditoraId = 0;
    this.Nome = '';
    this.Site = '';
};

var AutorDTO = function() {
    this.AutorId = 0;
    this.Nome = '';
    this.Sobrenome = '';
    this.Email = '';
};

var LivroAcervoDTO = function() {
  this.LivroId = 0;
  this.Titulo = '';
  this.Estante = '';
  this.AnoPublicacao = 0;
  this.Editora = new EditoraDTO();
  this.Autoria = '';  
  this.Autores = []; //Array<AutorDTO>
};

var LivroDTO = function() {
  this.LivroId = 0;
  this.Titulo = '';
  this.Estante = '';
  this.AnoPublicacao = 0;
  this.EditoraId = 0;
  this.Editora = new EditoraDTO();
  this.Autores = []; //Array<AutorDTO>
};

//--------------------------------------------------------------------

js_frame.newContext('homeContext', function() {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    

    //funções do contexto    

    //eventos do framework
    this.onCreate = function() {
        //console.log(this.getUrlParams());
    };
});		

//--------------------------------------------------------------------

js_frame.newService('registrarEditoraService', function() {
    this.listaEditoras = function(onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/registrarEditora/listaEditoras`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };    
    this.localizaEditora = function(id, onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/registrarEditora/localizaEditora/${id}`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };    
    this.novaEditora = function(editora, onSucess, onError) {
        js_frame.httpPost(
            `${Constantes.URL_API}/registrarEditora/novaEditora`,
            JSON.stringify(editora),
            'application/json; charset=utf-8', 
            'json', 
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };  
    this.ajusteEditora = function(editora, onSucess, onError) {
        js_frame.httpPut(
            `${Constantes.URL_API}/registrarEditora/ajusteEditora/${editora.EditoraId}`,
            JSON.stringify(editora),
            'application/json; charset=utf-8', 
            'json', 
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    }; 
});

js_frame.newContext('registrarEditoraContext', function(registrarEditoraService) {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    
    this.editoras = [];
    this.problemas = [];
    this.mensagem = '';

    //funções do contexto    
    this.listaEditoras = function() {
        this.editoras.length = 0; //mais eficiente
        registrarEditoraService.listaEditoras(function(retorno) {
            vthis.mensagem = retorno.Mensagem;
            vthis.problemas = retorno.Problemas;
            vthis.editoras = retorno.Valor;
        });				
    };

    this.ajustar = function(editoraId) {
        this.redirect('cadastro_editora.html',`id=${editoraId}`);
    };

    //eventos do framework
    this.onCreate = function() {
        this.listaEditoras();
    }; 	
});		

js_frame.newContext('cadastrarEditoraContext', function(registrarEditoraService) {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    
    this.editora = new EditoraDTO();
    this.problemas = [];
    this.mensagem = '';

    //funções do contexto    
    this.localizaEditora = function(id) {
        registrarEditoraService.localizaEditora(id, function(retorno) {
            vthis.mensagem = retorno.Mensagem;
            vthis.problemas = retorno.Problemas;
            vthis.editora = retorno.Valor;  
        });				
    };

    this.salvarEditora = function() {
        if (this.editora.EditoraId > 0) {
            registrarEditoraService.ajusteEditora(this.editora, function(retorno) {
                vthis.mensagem = retorno.Mensagem;
                vthis.problemas = retorno.Problemas;
                if (retorno.Valor || retorno.Problemas.length <= 0) {
                    vthis.mensagem = 'Editora ajustada com sucesso!';
                }
            });
        } else {
            registrarEditoraService.novaEditora(this.editora, function(retorno) {
                vthis.mensagem = retorno.Mensagem;
                vthis.problemas = retorno.Problemas;
                if (retorno.Valor > 0) {
                    vthis.mensagem = 'Editora registrada com sucesso!';
                    vthis.editora.EditoraId = retorno.Valor;
                }
            });
        }
    };

    //eventos do framework
    this.onCreate = function() {
        var params = this.getUrlParams();
        if (params && Object.keys(params).length > 0) {
            this.localizaEditora(params['id']);
        }
    }; 	
});		

//--------------------------------------------------------------------

js_frame.newService('registrarAutorService', function() {
    this.listaAutores = function(onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/registrarAutor/listaAutores`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };    
    this.localizaAutor = function(id, onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/registrarAutor/localizaAutor/${id}`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };    
    this.novoAutor = function(autor, onSucess, onError) {
        js_frame.httpPost(
            `${Constantes.URL_API}/registrarAutor/novoAutor`,
            JSON.stringify(autor),
            'application/json; charset=utf-8', 
            'json', 
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };  
    this.ajusteAutor = function(autor, onSucess, onError) {
        js_frame.httpPut(
            `${Constantes.URL_API}/registrarAutor/ajusteAutor/${autor.AutorId}`,
            JSON.stringify(autor),
            'application/json; charset=utf-8', 
            'json', 
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    }; 
});

js_frame.newContext('registrarAutorContext', function(registrarAutorService) {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    
    this.autores = [];
    this.problemas = [];
    this.mensagem = '';

    //funções do contexto    
    this.listaAutores = function() {
        this.autores.length = 0; //mais eficiente
        registrarAutorService.listaAutores(function(retorno) {
            vthis.mensagem = retorno.Mensagem;
            vthis.problemas = retorno.Problemas;
            vthis.autores = retorno.Autores;
        });				
    };

    this.ajustar = function(editoraId) {
        this.redirect('cadastro_autor.html',`id=${editoraId}`);
    };

    //eventos do framework
    this.onCreate = function() {
        this.listaAutores();
    }; 	
});		

js_frame.newContext('cadastrarAutorContext', function(registrarAutorService) {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    
    this.autor = new AutorDTO();
    this.problemas = [];
    this.mensagem = '';

    //funções do contexto    
    this.localizaAutor = function(id) {
        registrarAutorService.localizaAutor(id, function(retorno) {
            vthis.mensagem = retorno.Mensagem;
            vthis.problemas = retorno.Problemas;
            vthis.autor = retorno.Autor;  
        });				
    };

    this.salvarAutor = function() {
        if (this.autor.AutorId > 0) {
            registrarAutorService.ajusteAutor(this.autor, function(retorno) {
                vthis.mensagem = retorno.Mensagem;
                vthis.problemas = retorno.Problemas;
                if (retorno.A || retorno.Problemas.length <= 0) {
                    vthis.mensagem = 'Autor ajustado com sucesso!';
                }
            });
        } else {
            registrarAutorService.novoAutor(this.autor, function(retorno) {
                vthis.mensagem = retorno.Mensagem;
                vthis.problemas = retorno.Problemas;
                if (retorno.AutorId > 0) {
                    vthis.mensagem = 'Autor registrado com sucesso!';
                    vthis.autor.AutorId = retorno.AutorId;
                }
            });
        }
    };

    //eventos do framework
    this.onCreate = function() {
        var params = this.getUrlParams();
        if (params && Object.keys(params).length > 0) {
            this.localizaAutor(params['id']);
        }
    }; 	
});		

//--------------------------------------------------------------------

js_frame.newService('consultarAcervoService', function() {
    this.listaAutores = function(onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/consultarAcervo/listaAutores`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };    
    this.listaEditoras = function(onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/consultarAcervo/listaEditoras`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };  
    this.listaAcervo = function(editoraId, autorId, titulo, onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/consultarAcervo/listaAcervo/${editoraId}/${autorId}/${titulo}`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };        
});

js_frame.newContext('consultarAcervoContext', function(consultarAcervoService) {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    
    this.titulo = '';
    this.editoraId = 0;
    this.autorId = 0;
    this.autores = [];    
    this.editoras = [];
    this.acervo = [];
    this.problemas = [];
    this.mensagem = '';

    //funções do contexto    
    this.listaAutores = function() {
        this.autores.length = 0; //mais eficiente
        consultarAcervoService.listaAutores(function(retorno) {
            var autorTodos = new AutorDTO();
            autorTodos.Nome = 'Todos';
            vthis.autores.push(autorTodos);
            for (var i = 0; i < retorno.Autores.length; i++) {
                vthis.autores.push(retorno.Autores[i]);
            }
        });				
    };

    this.listaEditoras = function() {
        this.editoras.length = 0; //mais eficiente
        consultarAcervoService.listaEditoras(function(retorno) {
            var editoraTodas = new EditoraDTO();
            editoraTodas.Nome = 'Todas';
            vthis.editoras.push(editoraTodas);
            for (var i = 0; i < retorno.Valor.length; i++) {
                vthis.editoras.push(retorno.Valor[i]);
            }
        });				
    };

    this.listaAcervo = function() {
        this.acervo.length = 0; //mais eficiente
        consultarAcervoService.listaAcervo(this.editoraId, this.autorId, this.titulo, function(retorno) {
            vthis.mensagem = retorno.Mensagem;
            vthis.problemas = retorno.Problemas;
            vthis.acervo = retorno.Valor;
        });				
    };

    this.ajustarLivro = function(livroId) {
        this.redirect('registrar_livro.html',`id=${livroId}&origem=consultar_acervo`);
    };

    //eventos do framework
    this.onCreate = function() {
        this.listaAutores();
        this.listaEditoras();
        this.listaAcervo();
    }; 	
});		

//--------------------------------------------------------------------

js_frame.newService('registrarLivroService', function() {
    this.localizaLivro = function(id, onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/registrarLivro/localizaLivro/${id}`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    }; 
    this.listaAutoresDisponiveis = function(id, onSucess, onError) {
        js_frame.httpGet(
            `${Constantes.URL_API}/registrarLivro/listaAutoresDisponiveis/${id}`,
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    }; 
    this.novoLivro = function(livro, onSucess, onError) {
        js_frame.httpPost(
            `${Constantes.URL_API}/registrarLivro/novoLivro`,
            JSON.stringify(livro),
            'application/json; charset=utf-8', 
            'json', 
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    };  
    this.ajusteLivro = function(livro, onSucess, onError) {
        js_frame.httpPut(
            `${Constantes.URL_API}/registrarLivro/ajusteLivro/${livro.LivroId}`,
            JSON.stringify(livro),
            'application/json; charset=utf-8', 
            'json', 
            null,
            function(response){			
                if (onSucess) onSucess(response);
            },
            true,
            function(jqXHR, textStatus, response){
                alert('problema');
                console.log(jqXHR);
                console.log(textStatus);
                console.log(response);
                if (onError) onError();
            },
            false
        );
    }; 
});

js_frame.newContext('registrarLivroContext', function(registrarLivroService, registrarEditoraService) {		
    var vthis = this; /*! importante para usar em callbacks */

    //propriedades do contexto    
    this.livro = new LivroDTO();
    this.problemas = [];
    this.mensagem = '';
    this.editoras = [];
    this.autorDisponivelId = 0;
    this.autoresDisponiveis = [];
    this.href = '../index.html';

    //funções do contexto    
    this.listaEditoras = function() {
        this.editoras.length = 0; //mais eficiente
        registrarEditoraService.listaEditoras(function(retorno) {
            vthis.editoras = retorno.Valor; 
        });				
    };

    this.listaAutoresDisponiveis = function() {
        this.autoresDisponiveis.length = 0; //mais eficiente
        registrarLivroService.listaAutoresDisponiveis(this.livro.LivroId, function(retorno) {
            vthis.autoresDisponiveis = retorno.Valor; 
            vthis.autorDisponivelId = 0;
            if (vthis.autoresDisponiveis.length > 0) {
                vthis.autorDisponivelId = vthis.autoresDisponiveis[0].AutorId;
            }
        });				
    };

    this.localizaLivro = function(id) {
        registrarLivroService.localizaLivro(id, function(retorno) {
            vthis.mensagem = retorno.Mensagem;
            vthis.problemas = retorno.Problemas;
            vthis.livro = retorno.Valor;  
            vthis.listaAutoresDisponiveis();
        });				
    };

    this.incluirAutor = function() {
        if (this.autorDisponivelId > 0) {
            var i = 0;
            var tam = this.autoresDisponiveis.length;
            var achou = false;
            while (!achou && i < tam) {
                if (this.autoresDisponiveis[i].AutorId == this.autorDisponivelId) {
                    achou = true;
                    var novoAutor = JSON.parse(JSON.stringify(this.autoresDisponiveis[i]));
                    this.livro.Autores.push(novoAutor);
                } else {
                i++;
                }
            }
            if (achou) {
                this.autoresDisponiveis = this.autoresDisponiveis.filter(autor => autor.AutorId != this.autorDisponivelId);
                this.autorDisponivelId = 0;
                if (this.autoresDisponiveis.length > 0) {
                    this.autorDisponivelId = this.autoresDisponiveis[0].AutorId;
                }
            }
        }
    };

    this.salvar = function() {
        if (this.livro.EditoraId > 0) {
            var editora = this.editoras.filter(ed => ed.EditoraId == this.livro.EditoraId);
            this.livro.Editora = editora[0];
        }
        if (this.livro.LivroId > 0) {
            registrarLivroService.ajusteLivro(this.livro, function(retorno) {
                vthis.mensagem = retorno.Mensagem;
                vthis.problemas = retorno.Problemas;
                if (retorno.Valor || retorno.Problemas.length <= 0) {
                    vthis.mensagem = 'Livro ajustado com sucesso!';
                }
            });
        } else {
            registrarLivroService.novoLivro(this.livro, function(retorno) {
                vthis.mensagem = retorno.Mensagem;
                vthis.problemas = retorno.Problemas;
                if (retorno.Valor > 0) {
                    vthis.livro.LivroId = retorno.Valor;
                    vthis.mensagem = 'Livro registrado com sucesso!';
                }
            });
        }
    };
    
    //eventos do framework 
    this.onCreate = function() {
        this.listaEditoras();        
        var params = this.getUrlParams();
        if (params && Object.keys(params).length > 0) {
            var origem = '';
            if (params['origem']) {
                origem = params['origem'];
                if (origem == 'consultar_acervo') {
                    this.href = 'consultar_acervo.html';
                }
            }
            var id = 0;
            if (params['id']) id = params['id'];
            if (id > 0) {
                this.localizaLivro(id); 
            }
        } else {
            this.listaAutoresDisponiveis();
        }
    }; 	
});		
