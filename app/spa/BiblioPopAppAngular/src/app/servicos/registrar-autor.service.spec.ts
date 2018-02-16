import { TestBed, inject } from '@angular/core/testing';

import { RegistrarAutorService } from './registrar-autor.service';

describe('RegistrarAutorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegistrarAutorService]
    });
  });

  it('should be created', inject([RegistrarAutorService], (service: RegistrarAutorService) => {
    expect(service).toBeTruthy();
  }));
});
