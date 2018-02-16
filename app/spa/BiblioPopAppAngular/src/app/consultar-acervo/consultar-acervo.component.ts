import { Component, OnInit } from '@angular/core';

import { RegistrarEditoraService } from './../servicos/registrar-editora.service';
import { RegistrarAutorService } from './../servicos/registrar-autor.service';
import { ConsultarAcervoService } from './../servicos/consultar-acervo.service';

import { LivroAcervoDTO } from './../dto/LivroAcervoDTO';
import { AutorDTO } from './../dto/AutorDTO';
import { EditoraDTO } from './../dto/EditoraDTO';

@Component({
  selector: 'app-consultar-acervo',
  templateUrl: './consultar-acervo.component.html',
  styleUrls: ['./consultar-acervo.component.css']
})
export class ConsultarAcervoComponent implements OnInit {

  show = 0;
  mensagem = '';
  problemas: Array<string> = [];
  livros: Array<LivroAcervoDTO> = [];
  titulo = '';
  editoraId = 0;
  autorId = 0;
  editoras: Array<EditoraDTO> = [];
  autores: Array<AutorDTO> = [];

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  consultar() {
    this.registrarEditora.listaEditoras()
      .then((resposta: any) => {
        this.editoras = resposta.Valor;
      })
      .catch((resposta) => {
        alert('Ops, algo deu errado ao listar as editoras!');
        console.log(resposta);
      });

      this.registrarAutor.listaAutores()
        .then((resposta: any) => {
          this.autores = resposta.Autores;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao listar os autores!');
          console.log(resposta);
        });

    this.consultarAcervo.listaAcervo(this.titulo, this.editoraId, this.autorId)
      .then((resposta: any) => {
        this.mensagem = resposta.Mensagem;
        this.problemas = resposta.Problemas;
        this.livros = resposta.Valor;
      })
      .catch((resposta) => {
        alert('Ops, algo deu errado ao listar o acervo!');
        console.log(resposta);
      });
  }

  constructor(
    private consultarAcervo: ConsultarAcervoService,
    private registrarAutor: RegistrarAutorService,
    private registrarEditora: RegistrarEditoraService
  ) { }

  ngOnInit() {
    this.consultar();
  }

}
