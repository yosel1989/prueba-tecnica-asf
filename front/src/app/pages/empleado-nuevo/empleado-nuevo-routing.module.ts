import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmpleadoNuevoComponent} from "./empleado-nuevo.component";

const routes: Routes = [{
  path: '',
  component: EmpleadoNuevoComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmpleadoNuevoRoutingModule { }
