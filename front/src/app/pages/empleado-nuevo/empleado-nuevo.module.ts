import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {EmpleadoNuevoComponent} from "./empleado-nuevo.component";
import {EmpleadoNuevoRoutingModule} from "./empleado-nuevo-routing.module";
import {ReactiveFormsModule} from "@angular/forms";



@NgModule({
  declarations: [EmpleadoNuevoComponent],
  imports: [
    EmpleadoNuevoRoutingModule,
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [EmpleadoNuevoComponent],
  providers: [],
  bootstrap: [EmpleadoNuevoComponent]
})
export class EmpleadoNuevoModule { }
