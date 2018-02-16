import { Component, OnInit } from '@angular/core';

import { RegistrarEditoraService } from './../servicos/registrar-editora.service';
import { EditoraDTO } from './../dto/EditoraDTO';

@Component({
  selector: 'app-registrar-editora',
  templateUrl: './registrar-editora.component.html',
  styleUrls: ['./registrar-editora.component.css']
})
export class RegistrarEditoraComponent implements OnInit {

  show = 0;
  mensagem = '';
  problemas: Array<string> = [];
  editoras: Array<EditoraDTO> = [];

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  constructor(private registrarEditora: RegistrarEditoraService) { }

  ngOnInit() {
    this.registrarEditora.listaEditoras()
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          this.editoras = resposta.Valor;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao listar as editoras!');
          console.log(resposta);
        });
  }

}
