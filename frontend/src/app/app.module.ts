import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AnunciosComponent } from './components/anuncios/anuncios.component';
import { AnunciosFormComponent } from './components/anuncios/anuncios-form/anuncios-form.component';

@NgModule({
  declarations: [
    AppComponent,
    AnunciosComponent,
    AnunciosFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
