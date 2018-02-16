import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';

import { AutorDTO } from './../dto/AutorDTO';
import { RegistrarAutorService } from './../servicos/registrar-autor.service';

@Component({
  selector: 'app-registrar-autor-cadastro',
  templateUrl: './registrar-autor-cadastro.component.html',
  styleUrls: ['./registrar-autor-cadastro.component.css']
})
export class RegistrarAutorCadastroComponent implements OnInit {

  show = 0;
  mensagem = '';
  problemas: Array<string> = [];
  autor: AutorDTO = new AutorDTO();

  toggleCollapse() {
    if (this.show === 0) {
      this.show = 1;
    } else {
      this.show = 0;
    }
  }

  salvar() {
    if (this.autor.AutorId > 0) {
      this.registrarAutor.salvarAjusteAutor(this.autor)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          if (resposta.AlterouComSucesso || resposta.Problemas.length <= 0) {
            this.mensagem = 'Autor ajustado com sucesso!';
          }
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao ajustar o autor!');
          console.log(resposta);
        });
    } else {
      this.registrarAutor.salvarNovoAutor(this.autor)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          if (resposta.AutorId > 0) {
            this.router.navigate(['registrar-autor']);
          }
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao registrar o autor!');
          console.log(resposta);
        });
    }
  }

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private registrarAutor: RegistrarAutorService
  ) { }

  ngOnInit() {
    const id: number = Number(this.route.snapshot.paramMap.get('id'));
    if (id > 0) {
      this.registrarAutor.localizaAutor(id)
        .then((resposta: any) => {
          this.mensagem = resposta.Mensagem;
          this.problemas = resposta.Problemas;
          this.autor = resposta.Autor;
        })
        .catch((resposta) => {
          alert('Ops, algo deu errado ao localizar o autor!');
          console.log(resposta);
        });
    }
  }

}
