import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Anuncio } from '../modelos/anuncio';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AnuncioService {
  private headers = new HttpHeaders({
    'Content-type': 'application/json'
  });
  constructor(
    private http: HttpClient
  ) {}

  getAllAnuncios() {
    return this.http.get<Anuncio[]>(`${environment.apiUrl}/anuncios`,
     { headers: this.headers});
  }

  getAnuncio(id) {
    return this.http.get<Anuncio>(`${environment.apiUrl}/anuncios/${id}`,
    { headers: this.headers});
  }

  registerAnuncio(value) {
    return this.http.post(`${environment.apiUrl}/anuncios`, JSON.stringify(value),
    { headers: this.headers});
  }

  updateAnuncio(value) {
    return this.http.put(`${environment.apiUrl}/anuncios/${value.ID}`,JSON.stringify(value),
    { headers: this.headers});
  }

  removeAnuncio(id) {
    return this.http.delete(`${environment.apiUrl}/anuncios/${id}`,
    {headers: this.headers});
  }
}
