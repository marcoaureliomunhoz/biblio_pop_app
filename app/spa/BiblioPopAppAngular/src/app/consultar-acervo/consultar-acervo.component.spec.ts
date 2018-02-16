import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultarAcervoComponent } from './consultar-acervo.component';

describe('ConsultarAcervoComponent', () => {
  let component: ConsultarAcervoComponent;
  let fixture: ComponentFixture<ConsultarAcervoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultarAcervoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultarAcervoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
