import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransportadoraListaComponent } from './transportadora-lista.component';

describe('TransportadoraListaComponent', () => {
  let component: TransportadoraListaComponent;
  let fixture: ComponentFixture<TransportadoraListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TransportadoraListaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransportadoraListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
