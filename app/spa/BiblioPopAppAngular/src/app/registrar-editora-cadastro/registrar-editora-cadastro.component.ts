import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RegistrarEditoraService } from '../servicos/registrar-editora.service';

import { EditoraDTO } from './../dto/EditoraDTO';

@Component({
  selector: 'app-registrar-editora-cadastro',
  templateUrl: './registrar-editora-cadastro.component.html',
  styleUrls: ['./registrar-editora-cadastro.component.css']
})
export class RegistrarEditoraCadastroComponent implements OnInit {

  show = 0;
  mensagem = '';
  problemas: Array<string> = [];
  editora: EditoraDTO = new EditoraDTO();

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  salvar() {
    if (this.editora.EditoraId > 0) {
      this.registrarEditora.salvarAjusteEditora(this.editora)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          if (resposta.Valor || resposta.Problemas.length <= 0) {
            this.mensagem = 'Editora ajustada com sucesso!';
          }
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao ajustar a editora!');
          console.log(resposta);
        });
    } else {
      this.registrarEditora.salvarNovaEditora(this.editora)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          if (resposta.Valor > 0) {
            this.router.navigate(['registrar-editora']);
          }
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao registrar a editora!');
          console.log(resposta);
        });
    }
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private registrarEditora: RegistrarEditoraService
  ) { }

  ngOnInit() {
    const id: number = Number(this.route.snapshot.paramMap.get('id'));
    if (id > 0) {
      this.registrarEditora.localizaEditora(id)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          this.editora = resposta.Valor;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao localizar a editora!');
          console.log(resposta);
        });
    }
  }

}
