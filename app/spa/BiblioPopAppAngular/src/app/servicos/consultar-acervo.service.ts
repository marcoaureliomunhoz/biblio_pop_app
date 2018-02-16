import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/toPromise';

import { Constantes } from './../constantes';

@Injectable()
export class ConsultarAcervoService {

  constructor(private http: Http) { }

  listaAcervo(titulo = '', editoraId = 0, autorId = 0) {
    return this.http
        .get(`${Constantes.API_URL}/api/acervo/${editoraId}/${autorId}/${titulo}`)
        .toPromise()
        .then((resposta) => {
          return resposta.json();
        });
  }

}
