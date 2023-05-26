import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CondominioListaComponent } from './condominio-lista.component';

describe('CondominioListaComponent', () => {
  let component: CondominioListaComponent;
  let fixture: ComponentFixture<CondominioListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CondominioListaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CondominioListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
