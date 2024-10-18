import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmpleadoListadoComponent} from "./empleado-listado.component";

const routes: Routes = [{
  path: '',
  component: EmpleadoListadoComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmpleadoListadoRoutingModule { }
