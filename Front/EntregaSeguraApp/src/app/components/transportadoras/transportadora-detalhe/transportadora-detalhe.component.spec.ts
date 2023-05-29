import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportadoraDetalheComponent } from './transportadora-detalhe.component';

describe('TransportadoraDetalheComponent', () => {
  let component: TransportadoraDetalheComponent;
  let fixture: ComponentFixture<TransportadoraDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransportadoraDetalheComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransportadoraDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
