import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UnidadeListaComponent } from './unidade-lista.component';

describe('UnidadeListaComponent', () => {
  let component: UnidadeListaComponent;
  let fixture: ComponentFixture<UnidadeListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UnidadeListaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UnidadeListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
