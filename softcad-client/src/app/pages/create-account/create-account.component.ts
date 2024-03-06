import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AdminsRequestService } from '../../services/requests/admins-request.service';
import { AlertService} from '../../services/alert.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.scss'
})
export class CreateAccountComponent implements OnInit{
  form!: FormGroup;
  
  constructor(
    private formBuilder: FormBuilder,
    private service: AdminsRequestService,
    private alert: AlertService,
    private router: Router
  ){ } 

  ngOnInit(){
    localStorage.clear();
    this.form = this.formBuilder.group({
      name: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      email: [null, [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(255)]],
      password: [null, [Validators.required, Validators.pattern(/(?=^.{8,20}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/)]]
    });
  }

  onSubmit(){
    if(this.form.valid){
      this.service.createAdministrator(this.form.value).subscribe({
        next: () => {
          this.alert.showAlertSuccess("Usuário criado com sucesso!");
          this.router.navigate(['/home'])
        },
        error: () => this.alert.showAlertError("Erro ao criar conta, tente novamente!")
      })
    }
  }
}
