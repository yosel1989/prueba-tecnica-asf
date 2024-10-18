import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppComponent} from "./app.component";

const routes: Routes = [
  {
    path: 'empleado',
    loadChildren: () => import('./pages/empleado-listado/empleado-listado.module').then(m => m.EmpleadoListadoModule)
  },
  {
    path: 'empleado/nuevo',
    loadChildren: () => import('./pages/empleado-nuevo/empleado-nuevo.module').then(m => m.EmpleadoNuevoModule)
  },
  {
    path: 'empleado/editar/:id',
    loadChildren: () => import('./pages/empleado-editar/empleado-editar.module').then(m => m.EmpleadoEditarModule)
  },
  {
    path: '**',
    redirectTo: 'empleado'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
