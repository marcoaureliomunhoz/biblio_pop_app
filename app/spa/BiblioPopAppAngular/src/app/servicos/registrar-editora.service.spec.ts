import { TestBed, inject } from '@angular/core/testing';

import { RegistrarEditoraService } from './registrar-editora.service';

describe('RegistrarEditoraService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RegistrarEditoraService]
    });
  });

  it('should be created', inject([RegistrarEditoraService], (service: RegistrarEditoraService) => {
    expect(service).toBeTruthy();
  }));
});
