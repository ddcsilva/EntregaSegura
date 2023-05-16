import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-condominios',
  templateUrl: './condominios.component.html',
  styleUrls: ['./condominios.component.scss']
})
export class CondominiosComponent implements OnInit {

  public condominios: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getCondominios();
  }

  public getCondominios(): void {
    this.http.get('https://localhost:5001/api/condominios').subscribe({
      next: response => {
        this.condominios = response;
      },
      error: error => {
        console.log(error);
      }
    });
  }  
}
