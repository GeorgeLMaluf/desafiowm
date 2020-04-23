import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { switchMap, map } from 'rxjs/operators';
import { AnuncioService } from 'src/app/servicos/anuncio.service';
import { WmservicesService } from 'src/app/servicos/wmservices.service';



@Component({
  selector: 'app-anuncios-form',
  templateUrl: './anuncios-form.component.html',
  styleUrls: ['./anuncios-form.component.scss']
})
export class AnunciosFormComponent implements OnInit {
  AnuncioForm: FormGroup;
  titulo: string;
  marcas: string[];
  modelos: string[];
  versoes: string[];

  editMode: boolean;
  insertMode: boolean;
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private anuncioSrv: AnuncioService,
    private wbSrv: WmservicesService
  ) { }

  ngOnInit(): void {
    this.editMode = false;
    this.titulo = "Novo anuncio";
    if (this.route.snapshot.params.id) {
      this.titulo = 'Editar anuncio';
      this.editMode = true;
      this.route.params
        .pipe(
          map((params: any) => params.id),
          switchMap(id => this.anuncioSrv.getAnuncio(id))
        )
        .subscribe(anuncio => this.updateForm(anuncio));
    }
    this.insertMode = !this.editMode;
    this.AnuncioForm = this.fb.group ({
      ID: [null],
      marca: [null, Validators.required],
      modelo: [null, Validators.required],
      versao: [null, Validators.required],
      ano: [null, Validators.required],
      quilometragem: [null, Validators.required],
      observacao: ['', Validators.required]
    });
    this.loadMarcas();
  }

  updateForm(anuncio) {
    this.loadMarcas();
    this.loadModelos(anuncio.marca);
    this.loadVersoes(anuncio.marca, anuncio.modelo);

    this.AnuncioForm.patchValue({
      ID: anuncio.ID,
      marca: anuncio.marca,
      modelo: anuncio.modelo,
      versao: anuncio.versao,
      ano: anuncio.ano,
      quilometragem: anuncio.quilometragem,
      observacao: anuncio.observacao
    });
  }

  loadMarcas() {
    this.wbSrv.getMarcas()
      .subscribe(
        res => {
          this.marcas = res;
        }
      )
  };

  onMarcaChange(marca) {
    this.AnuncioForm.patchValue({
      modelo: [null],
      versao: [null]
    });
    this.loadModelos(marca);
  }

  onModeloChange(modelo) {
    this.AnuncioForm.patchValue({
      versao: [null]
    });
    this.loadVersoes(this.AnuncioForm.value.marca, modelo);
  }

  loadModelos(marca) {
    this.wbSrv.getModelos(marca)
      .subscribe(
        res => {
          this.modelos = res;
        }
      )
  }

  loadVersoes(marca, modelo) {
    this.wbSrv.getVersoes(marca, modelo)
      .subscribe(
        res => {
          this.versoes = res;
        }
      )
  }


  onSubmit() {
    if (this.AnuncioForm.value.ID) {
      this.anuncioSrv.updateAnuncio(this.AnuncioForm.value)
        .subscribe(
          res => {
            this.router.navigate(['/anuncios']);
          }
        );
    } else {
      this.anuncioSrv.registerAnuncio(this.AnuncioForm.value)
        .subscribe(
          res => {
            this.router.navigate(['/anuncios']);
          }
        );
    }
  }
}
