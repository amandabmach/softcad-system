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
  form!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private service: UsersRequestService,
    private alert: AlertService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      email: [null, [Validators.required, Validators.email, Validators.minLength(5), Validators.maxLength(255)]],
      age: [null, Validators.required],
      address: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      information: [null, Validators.maxLength(255)],
      interests: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      feelings: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      principles: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      status: [true]
    });
  }

  onSubmit() {
    if(this.form.valid) {
      this.service.createUser(this.form.value).subscribe({
        next: () => {
          this.alert.showAlertSuccess("Usuário cadastrado com sucesso!"),
          this.resetaDadosForm();
        },
        error: () => this.alert.showAlertError("Erro ao cadastrar usuário, tente novamente")
      })
    }
  }

  resetaDadosForm() {
    this.form.reset();
  }
}
