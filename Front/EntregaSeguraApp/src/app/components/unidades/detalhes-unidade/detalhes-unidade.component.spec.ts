import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesUnidadeComponent } from './detalhes-unidade.component';

describe('DetalhesUnidadeComponent', () => {
  let component: DetalhesUnidadeComponent;
  let fixture: ComponentFixture<DetalhesUnidadeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalhesUnidadeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesUnidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
