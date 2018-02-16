import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { LivroDTO } from './../dto/LivroDTO';
import { Constantes } from './../constantes';

@Injectable()
export class RegistrarLivroService {

  constructor(private http: Http) { }

  localizaLivro(id: number) {
    return this.http
        .get(`${Constantes.API_URL}/api/livros/${id}`)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  listaAutoresDisponiveis(id: number) {
    return this.http
        .get(`${Constantes.API_URL}/api/autores-disponiveis/${id}`)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  salvarNovoLivro(livro: LivroDTO) {
    return this.http
        .post(`${Constantes.API_URL}/api/livros`, livro)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  salvarAjusteLivro(livro: LivroDTO) {
    return this.http
        .put(`${Constantes.API_URL}/api/livros/${livro.LivroId}`, livro)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

}
