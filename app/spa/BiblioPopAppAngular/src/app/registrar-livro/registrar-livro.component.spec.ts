import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrarLivroComponent } from './registrar-livro.component';

describe('RegistrarLivroComponent', () => {
  let component: RegistrarLivroComponent;
  let fixture: ComponentFixture<RegistrarLivroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrarLivroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrarLivroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
