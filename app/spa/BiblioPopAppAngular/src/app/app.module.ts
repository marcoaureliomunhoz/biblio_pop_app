import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';

import { HomeComponent } from './home/home.component';
import { RegistrarEditoraComponent } from './registrar-editora/registrar-editora.component';
import { RegistrarEditoraCadastroComponent } from './registrar-editora-cadastro/registrar-editora-cadastro.component';
import { RegistrarAutorComponent } from './registrar-autor/registrar-autor.component';
import { RegistrarAutorCadastroComponent } from './registrar-autor-cadastro/registrar-autor-cadastro.component';
import { ConsultarAcervoComponent } from './consultar-acervo/consultar-acervo.component';
import { RegistrarLivroComponent } from './registrar-livro/registrar-livro.component';

import { RegistrarEditoraService } from './servicos/registrar-editora.service';
import { RegistrarAutorService } from './servicos/registrar-autor.service';
import { ConsultarAcervoService } from './servicos/consultar-acervo.service';
import { RegistrarLivroService } from './servicos/registrar-livro.service';

const appRoutes: Routes = [
  { path: 'ajustar-editora/:id', component: RegistrarEditoraCadastroComponent },
  { path: 'nova-editora', component: RegistrarEditoraCadastroComponent },
  { path: 'registrar-editora', component: RegistrarEditoraComponent },
  { path: 'ajustar-autor/:id', component: RegistrarAutorCadastroComponent },
  { path: 'novo-autor', component: RegistrarAutorCadastroComponent },
  { path: 'registrar-autor', component: RegistrarAutorComponent },
  { path: 'consultar-acervo', component: ConsultarAcervoComponent },
  { path: 'registrar-livro', component: RegistrarLivroComponent },
  { path: 'home', component: HomeComponent },
  { path: '', component: HomeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegistrarEditoraComponent,
    RegistrarEditoraCadastroComponent,
    RegistrarAutorComponent,
    RegistrarAutorCadastroComponent,
    ConsultarAcervoComponent,
    RegistrarLivroComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    RegistrarEditoraService,
    RegistrarAutorService,
    ConsultarAcervoService,
    RegistrarLivroService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
