import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrarEditoraComponent } from './registrar-editora.component';

describe('RegistrarEditoraComponent', () => {
  let component: RegistrarEditoraComponent;
  let fixture: ComponentFixture<RegistrarEditoraComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrarEditoraComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrarEditoraComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
