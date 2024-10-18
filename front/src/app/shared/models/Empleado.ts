export class Empleado {
  public id: number;           // Id_Empleado
  public nombres: string;              // Nombres
  public apellidos: string;            // ApellIdos
  public fechaNacimiento: Date;        // Fecha_Nacimiento
  public fechaIngreso: Date;           // Fecha_Ingreso
  public idAFP: number;                // Id_AFP
  public afp: number;                   // AFP
  public idCargo: number;              // Id_Cargo
  public cargo: string;              // Cargo
  public sueldo: number;               // Sueldo
  public status: boolean;              // Status (1 = Activo, 0 = Inactivo)

  constructor(){}
}
