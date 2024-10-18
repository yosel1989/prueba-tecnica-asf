import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmpleadoEditarRoutingModule } from './empleado-editar-routing.module';
import {EmpleadoEditarComponent} from "./empleado-editar.component";
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [EmpleadoEditarComponent],
  imports: [
    CommonModule,
    EmpleadoEditarRoutingModule,
    ReactiveFormsModule
  ],
  exports:[EmpleadoEditarComponent],
  providers: [
  ],
  bootstrap: [EmpleadoEditarComponent]
})
export class EmpleadoEditarModule { }
