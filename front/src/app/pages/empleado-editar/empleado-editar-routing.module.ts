import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmpleadoEditarComponent} from "./empleado-editar.component";

const routes: Routes = [{
  path: '',
  component: EmpleadoEditarComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmpleadoEditarRoutingModule { }
