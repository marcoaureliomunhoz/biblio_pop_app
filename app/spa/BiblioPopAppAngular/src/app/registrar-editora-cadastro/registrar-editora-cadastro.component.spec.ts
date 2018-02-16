import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrarEditoraCadastroComponent } from './registrar-editora-cadastro.component';

describe('RegistrarEditoraCadastroComponent', () => {
  let component: RegistrarEditoraCadastroComponent;
  let fixture: ComponentFixture<RegistrarEditoraCadastroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrarEditoraCadastroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrarEditoraCadastroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
