import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnidadeDetalheComponent } from './unidade-detalhe.component';

describe('UnidadeDetalheComponent', () => {
  let component: UnidadeDetalheComponent;
  let fixture: ComponentFixture<UnidadeDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnidadeDetalheComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnidadeDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
