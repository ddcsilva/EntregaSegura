import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CondominioDetalheComponent } from './condominio-detalhe.component';

describe('CondominioDetalheComponent', () => {
  let component: CondominioDetalheComponent;
  let fixture: ComponentFixture<CondominioDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CondominioDetalheComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CondominioDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
