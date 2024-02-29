import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AdminsRequestService } from '../../services/requests/admins-request.service';
import { AlertService} from '../../services/alert.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.scss'
})
export class CreateAccountComponent {
  formulario!: FormGroup;
  
  constructor(
    private formBuilder: FormBuilder,
    private service: AdminsRequestService,
    private alert: AlertService,
    private router: Router
  ){ } 

  ngOnInit(){
    localStorage.clear();
    this.formulario = this.formBuilder.group({
      nome: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      email: [null, [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(255)]],
      senha: [null, [Validators.required, Validators.pattern(/(?=^.{8,20}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/)]]
    });
  }

  onSubmit(){
    if(this.formulario.valid){
      this.service.createAdministrador(this.formulario.value).subscribe({
        next: () => {
          this.alert.showAlertSuccess("Usuário criado com sucesso!");
          this.router.navigate(['/home'])
        },
        error: () => this.alert.showAlertError("Erro ao criar conta, tente novamente!")
      })
    }
  }
}
