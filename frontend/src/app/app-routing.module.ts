import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AnunciosComponent } from './components/anuncios/anuncios.component';
import { AnunciosFormComponent } from './components/anuncios/anuncios-form/anuncios-form.component';


const routes: Routes = [
  { path: '', redirectTo: '/anuncios', pathMatch: 'full'},
  { path: 'anuncios', component: AnunciosComponent},
  { path: 'anuncios/novo', component: AnunciosFormComponent},
  { path: 'anuncios/editar/:id', component: AnunciosFormComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
