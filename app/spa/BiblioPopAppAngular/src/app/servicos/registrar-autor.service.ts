import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { AutorDTO } from './../dto/AutorDTO';
import { Constantes } from './../constantes';

@Injectable()
export class RegistrarAutorService {

  constructor(private http: Http) { }

  listaAutores() {
    return this.http
        .get(`${Constantes.API_URL}/api/autores`)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  localizaAutor(id: number) {
    return this.http
        .get(`${Constantes.API_URL}/api/autores/${id}`)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  salvarNovoAutor(autor: AutorDTO) {
    return this.http
        .post(`${Constantes.API_URL}/api/autores`, autor)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  salvarAjusteAutor(autor: AutorDTO) {
    return this.http
        .put(`${Constantes.API_URL}/api/autores/${autor.AutorId}`, autor)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

}
