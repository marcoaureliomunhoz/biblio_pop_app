import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RegistrarLivroService } from './../servicos/registrar-livro.service';
import { RegistrarEditoraService } from './../servicos/registrar-editora.service';

import { LivroDTO } from './../dto/LivroDTO';
import { EditoraDTO } from './../dto/EditoraDTO';
import { AutorDTO } from '../dto/AutorDTO';

@Component({
  selector: 'app-registrar-livro',
  templateUrl: './registrar-livro.component.html',
  styleUrls: ['./registrar-livro.component.css']
})
export class RegistrarLivroComponent implements OnInit {

  show = 0;
  mensagem = '';
  problemas: Array<string> = [];
  livro: LivroDTO = new LivroDTO();
  editoras: Array<EditoraDTO> = [];
  autoresDisponiveis: Array<AutorDTO> = [];
  autorDisponivelId = 0;

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  incluirAutor() {
    if (this.autorDisponivelId > 0) {
      let i = 0;
      const tam = this.autoresDisponiveis.length;
      let achou = false;
      while (!achou && i < tam) {
        if (this.autoresDisponiveis[i].AutorId == this.autorDisponivelId) {
          achou = true;
          const novoAutor = JSON.parse(JSON.stringify(this.autoresDisponiveis[i]));
          this.livro.Autores.push(novoAutor);
        } else {
          i++;
        }
      }
      if (achou) {
        this.autoresDisponiveis = this.autoresDisponiveis.filter(autor => autor.AutorId != this.autorDisponivelId);
      }
    }
  }

  salvar() {
    if (this.livro.EditoraId > 0) {
      const editora = this.editoras.filter(ed => ed.EditoraId == this.livro.EditoraId);
      this.livro.Editora = editora[0];
    }
    if (this.livro.LivroId > 0) {
      this.registrarLivro.salvarAjusteLivro(this.livro)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          if (resposta.Valor || resposta.Problemas.length <= 0) {
            this.mensagem = 'Livro ajustado com sucesso!';
          }
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao ajustar o livro!');
          console.log(resposta);
        });
    } else {
      this.registrarLivro.salvarNovoLivro(this.livro)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          if (resposta.Valor > 0) {
            this.livro.LivroId = resposta.Valor;
            this.mensagem = 'Livro registrado com sucesso!';
          }
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao localizar a editora!');
          console.log(resposta);
        });
    }
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private registrarEditora: RegistrarEditoraService,
    private registrarLivro: RegistrarLivroService
  ) { }

  carregarEditoras() {
    this.registrarEditora.listaEditoras()
      .then((resposta: any) => {
        this.editoras = resposta.Valor;
      })
      .catch((resposta) => {
        alert('Ops, algo deu errado ao listar as editoras!');
        console.log(resposta);
      });
  }

  carregarAutoresDisponiveis() {
    this.registrarLivro.listaAutoresDisponiveis(this.livro.LivroId)
        .then((resposta: any) => {
          this.autoresDisponiveis = resposta.Valor;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao listar os autores disponíveis!');
          console.log(resposta);
        });
  }

  ngOnInit() {
    this.carregarEditoras();
    this.carregarAutoresDisponiveis();

    const id: number = Number(this.route.snapshot.paramMap.get('id'));
    if (id > 0) {
      this.registrarLivro.localizaLivro(id)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          this.livro = resposta.Valor;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao localizar o livro!');
          console.log(resposta);
        });
    }
  }

}
