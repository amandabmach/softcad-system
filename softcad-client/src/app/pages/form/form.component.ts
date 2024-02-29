import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { AlertService } from '../../services/alert.service';
import { UsersRequestService } from '../../services/requests/users-request.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.scss'
})
export class FormComponent implements OnInit {
  formulario!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private service: UsersRequestService,
    private alert: AlertService
  ) { }

  ngOnInit() {
    this.formulario = this.formBuilder.group({
      nome: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      email: [null, [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(255)]],
      idade: [null, Validators.required],
      endereco: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      informacoes: [null, Validators.maxLength(255)],
      interesses: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      sentimentos: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      valores: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      status: [true]
    });
  }

  onSubmit() {
    if(this.formulario.valid) {
      this.service.createUser(this.formulario.value).subscribe({
        next: () => {
          this.alert.showAlertSuccess("Usuário cadastrado com sucesso!"),
          this.resetaDadosForm();
        },
        error: () => this.alert.showAlertError("Erro ao cadastrar usuário, tente novamente")
      })
    }
  }

  resetaDadosForm() {
    this.formulario.reset();
  }
}
