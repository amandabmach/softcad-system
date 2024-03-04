import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { AlertService } from '../../services/alert.service';
import { AdminsRequestService } from '../../services/requests/admins-request.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent implements OnInit{
  formulario!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private service: AdminsRequestService,
    private route: Router,
    private alert : AlertService,
  ){ } 

  ngOnInit(){
    localStorage.clear();
    this.formulario = this.formBuilder.group({
      email:[null, [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(255)]],
      password: [null, [Validators.required, Validators.pattern(/(?=^.{8,20}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/)]]
    });
  }

  onSubmit(){
    if(this.formulario.valid){
      this.service.login(this.formulario.value).subscribe({
        next: () => this.route.navigate(['/home']),
        error: () => this.alert.showAlertError("Erro ao realizar login, tente novamente!"),
      })
    }
  }
}
