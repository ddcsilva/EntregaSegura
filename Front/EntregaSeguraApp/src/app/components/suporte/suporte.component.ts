import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-suporte',
  templateUrl: './suporte.component.html',
  styleUrls: ['./suporte.component.scss']
})
export class SuporteComponent implements OnInit {
  public titulo: string = 'Suporte';

  constructor() { }

  ngOnInit(): void {
  }

}
