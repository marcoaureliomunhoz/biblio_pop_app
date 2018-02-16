import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/toPromise';

import { EditoraDTO } from './../dto/EditoraDTO';
import { Constantes } from './../constantes';

@Injectable()
export class RegistrarEditoraService {

  constructor(private http: Http) { }

  listaEditoras() {
    return this.http
        .get(`${Constantes.API_URL}/api/editoras`)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  localizaEditora(id: number) {
    return this.http
        .get(`${Constantes.API_URL}/api/editoras/${id}`)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  salvarNovaEditora(editora: EditoraDTO) {
    return this.http
        .post(`${Constantes.API_URL}/api/editoras`, editora)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

  salvarAjusteEditora(editora: EditoraDTO) {
    return this.http
        .put(`${Constantes.API_URL}/api/editoras/${editora.EditoraId}`, editora)
        .toPromise()
        .then(resposta => {
          return resposta.json();
        });
  }

}
