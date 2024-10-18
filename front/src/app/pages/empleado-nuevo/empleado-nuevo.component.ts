import {AfterViewInit, Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {EmpleadoService} from "../../shared/services/empleado.service";
import {Subscription} from "rxjs";
import {HotToastService} from "@ngneat/hot-toast";
import {Router} from "@angular/router";
import {Empleado} from "../../shared/models/Empleado";

@Component({
  selector: 'app-usuario-nuevo',
  templateUrl: './empleado-nuevo.component.html',
  styleUrls: ['./empleado-nuevo.component.scss']
})
export class EmpleadoNuevoComponent implements OnInit, AfterViewInit, OnDestroy {

  formGroup: FormGroup;

  loadingSubmit: boolean;

  subscriptions: Subscription[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private userService: EmpleadoService,
    private toast: HotToastService,
    private router: Router
  ) {

    this.loadingSubmit = true;

    this.formGroup = this.formBuilder.group({
      nombres: new FormControl(null, Validators.required),
      apellidos: new FormControl(null, Validators.required),
      fechaNacimiento: new FormControl(null, Validators.required),
      fechaIngreso: new FormControl(null, Validators.required),
      idAFP: new FormControl('', Validators.required),
      idCargo: new FormControl('', Validators.required),
      sueldo: new FormControl(0, [Validators.required, Validators.min(0)]),
      status: new FormControl(1, Validators.required),
    });

  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
  }

  ngOnDestroy(): void {

    this.subscriptions.forEach(s => s.unsubscribe());

  }

  // Getter
  get f(): any{
    return this.formGroup.controls;
  }

  get model(): any{
    return {
      nombres: this.f.nombres.value,
      apellidos: this.f.apellidos.value,
      fechaNacimiento: this.f.fechaNacimiento.value,
      fechaIngreso: this.f.fechaIngreso.value,
      idAFP: parseInt(this.f.idAFP.value, 10),
      idCargo: parseInt(this.f.idCargo.value, 10),
      sueldo: parseFloat(this.f.sueldo.value),
      status: !!parseInt(this.f.status.value, 10),
    }
  }

  // Events
  evtOnSubmit(): void{
      if(this.formGroup.invalid){
        this.toast.error('Debe llenar todos los campos');
        return;
      }

      this.loadingSubmit = true;
      const sub = this.userService.crearEmpleado(this.model).subscribe((res: Empleado)=>{
        this.toast.success('Se registro el empleado con exito');
        this.loadingSubmit = false;
        this.router.navigate(['/empleado'])
      }, error => {
        this.toast.error('No se pudo registrar el empleado');
        this.loadingSubmit = false;
      });
      this.subscriptions.push(sub);

  }


}
