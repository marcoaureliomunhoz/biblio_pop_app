import { TestBed, inject } from '@angular/core/testing';

import { ConsultarAcervoService } from './consultar-acervo.service';

describe('ConsultarAcervoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConsultarAcervoService]
    });
  });

  it('should be created', inject([ConsultarAcervoService], (service: ConsultarAcervoService) => {
    expect(service).toBeTruthy();
  }));
});
