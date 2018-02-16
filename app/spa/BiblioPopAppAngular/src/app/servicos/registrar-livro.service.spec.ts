import { TestBed, inject } from '@angular/core/testing';

import { RegistrarLivroService } from './registrar-livro.service';

describe('RegistrarLivroService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegistrarLivroService]
    });
  });

  it('should be created', inject([RegistrarLivroService], (service: RegistrarLivroService) => {
    expect(service).toBeTruthy();
  }));
});
