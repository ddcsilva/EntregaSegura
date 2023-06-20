import { TestBed } from '@angular/core/testing';

import { SidenavServiceService } from './sidenav-service.service';

describe('SidenavServiceService', () => {
  let service: SidenavServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SidenavServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
