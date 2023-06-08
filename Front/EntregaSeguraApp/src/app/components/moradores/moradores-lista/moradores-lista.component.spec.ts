import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MoradoresListaComponent } from './moradores-lista.component';

describe('MoradoresListaComponent', () => {
  let component: MoradoresListaComponent;
  let fixture: ComponentFixture<MoradoresListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MoradoresListaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MoradoresListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
