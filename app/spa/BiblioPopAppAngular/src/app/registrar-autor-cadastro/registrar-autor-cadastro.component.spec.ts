import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrarAutorCadastroComponent } from './registrar-autor-cadastro.component';

describe('RegistrarAutorCadastroComponent', () => {
  let component: RegistrarAutorCadastroComponent;
  let fixture: ComponentFixture<RegistrarAutorCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrarAutorCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrarAutorCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
