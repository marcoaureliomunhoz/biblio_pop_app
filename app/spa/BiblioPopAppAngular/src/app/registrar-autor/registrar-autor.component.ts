import { Component, OnInit } from '@angular/core';

import { RegistrarAutorService } from './../servicos/registrar-autor.service';
import { AutorDTO } from './../dto/AutorDTO';

@Component({
  selector: 'app-registrar-autor',
  templateUrl: './registrar-autor.component.html',
  styleUrls: ['./registrar-autor.component.css']
})
export class RegistrarAutorComponent implements OnInit {

  show = 0;
  mensagem = '';
  problemas: Array<string> = [];
  autores: Array<AutorDTO> = [];

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  constructor(private registrarAutor: RegistrarAutorService) { }

  ngOnInit() {
    this.registrarAutor.listaAutores()
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          this.autores = resposta.Autores;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao listar os autores!');
          console.log(resposta);
        });
  }

}
