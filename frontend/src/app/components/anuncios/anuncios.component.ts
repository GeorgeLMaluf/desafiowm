import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Anuncio } from 'src/app/modelos/anuncio';
import { AnuncioService } from 'src/app/servicos/anuncio.service';

@Component({
  selector: 'app-anuncios',
  templateUrl: './anuncios.component.html',
  styleUrls: ['./anuncios.component.scss']
})
export class AnunciosComponent implements OnInit {
  anuncios: Anuncio[];
  anuncioId: number;

  constructor(
    private anuncioSrv: AnuncioService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.carregaAnuncios();
  }

  private carregaAnuncios() {
    this.anuncioSrv.getAllAnuncios()
      .subscribe(resp => {
        this.anuncios = resp;
      });
  }

  editar(id) {
    this.router.navigate(['editar', id], { relativeTo: this.route });
  }

  confirmDelete(id) {
    if (confirm("Apagar o anuncio?")) {
      this.anuncioSrv.removeAnuncio(id)
        .subscribe(
          res => {
            this.carregaAnuncios()
          }
        );
    }
  }
}
