import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WmservicesService {
  private headers = new HttpHeaders({
    'Content-type': 'application/json'
  });

  constructor(
    private http: HttpClient
  ) { }

  getMarcas() {
    return this.http.get<string[]>(`${environment.apiUrl}/services/getmakers`,
    { headers: this.headers});
  }

  getModelos(marca) {
    return this.http.get<string[]>(`${environment.apiUrl}/services/getmodels?make=${marca}`,
    { headers: this.headers});
  }

  getVersoes(marca, modelo) {
    return this.http.get<string[]>(`${environment.apiUrl}/services/getversions?make=${marca}&model=${modelo}`,
    { headers: this.headers});
  }
}
