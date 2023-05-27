import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExclusaoDialogComponent } from './exclusao-dialog.component';

describe('ExclusaoDialogComponent', () => {
  let component: ExclusaoDialogComponent;
  let fixture: ComponentFixture<ExclusaoDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExclusaoDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExclusaoDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
