import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../../../services/alert.service';
import { AdminsRequestService } from '../../../services/requests/admins-request.service';
import { Router } from '@angular/router';
import { AdministradorService } from '../../../services/administrador.service';

@Component({
  selector: 'app-sidebar-profile',
  templateUrl: './sidebar-profile.component.html',
  styleUrl: './sidebar-profile.component.scss'
})
export class SidebarProfileComponent implements OnInit{

  formulario!: FormGroup;
  @Input() visible!: boolean;
  @ViewChild('fileInput') fileInput: any;

  photo!: string | null;
  file!: File;

  onCloseSidebar() {
    this.visible = false;
  }

  constructor(
    private formBuilder: FormBuilder,
    private service: AdminsRequestService,
    private alert: AlertService,
    private router: Router,
    private photoService: AdministradorService
  ) { }

  ngOnInit() {
    this.formulario = this.formBuilder.group({
      name: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
      email: [null, [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(200)]],
      password: [null, [Validators.required, Validators.pattern(/(?=^.{8,}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/)]]
    });

    this.service.getAdministrator().subscribe(dados => {
      this.formulario.patchValue({
        name: dados.name,
        email: dados.email
      })
    });

    this.photo = this.photoService.getPhoto();
  }

  addImage() {
    this.fileInput.nativeElement.click();
  }


  fileSelected(event: any) {
    this.file = event.target.files[0];
    this.photo = URL.createObjectURL(this.file);
  }

  onUpdate() {
    if (this.formulario.valid) {
      this.alert.showConfirm("Atualizar dados da conta", "Tem certeza que deseja atualizar seus dados?", "Sim", "Não").subscribe(dados => {
        if (dados) {
          if (this.file != null) {
            const body = { ...this.formulario.value, foto: this.file }
            this.service.updateAdministrator(body).subscribe({
              complete: () => {
                this.alert.showAlertSuccess("Perfil atualizado com sucesso"),
                this.router.navigate(['/login'])
              },
              error: () => this.alert.showAlertError("Erro ao atualizar perfil")
            })
          } else{
            const body = { ...this.formulario.value}
            this.service.updateAdministrator(body).subscribe({
              complete: () => {
                this.alert.showAlertSuccess("Perfil atualizado com sucesso"),
                this.router.navigate(['/login'])
              },
              error: () => this.alert.showAlertError("Erro ao atualizar perfil")
            })
          }
        }
      });
    }
  }

  onDelete() {
    this.alert.showConfirm("Deletar conta", "Tem certeza que deseja deletar seu perfil?", "Sim", "Não").subscribe(dados => {
      if (dados === true) {
        this.service.deleteAdministrator().subscribe({
          next: () => {
            this.alert.showAlertSuccess("Conta deletada com sucesso")
            this.router.navigate(['/login']);
          },
          error: () => this.alert.showAlertError("Erro ao deletar conta, tente novamente!")
        })
      }
    })
  }
}

