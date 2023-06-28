import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesFuncionarioComponent } from './detalhes-funcionario.component';

describe('DetalhesFuncionarioComponent', () => {
  let component: DetalhesFuncionarioComponent;
  let fixture: ComponentFixture<DetalhesFuncionarioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalhesFuncionarioComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesFuncionarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
