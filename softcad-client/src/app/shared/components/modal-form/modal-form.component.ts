import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UsersRequestService } from '../../../services/requests/users-request.service';
import { AlertService } from '../../../services/alert.service';

@Component({
  selector: 'app-modal-form',
  templateUrl: './modal-form.component.html',
  styleUrl: './modal-form.component.scss'
})
export class ModalFormComponent implements OnInit, OnChanges {

  @Input() user: any | null;
  @Input() visibility!: boolean;
  @Output() event = new EventEmitter<boolean>();

  formulario!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private service: UsersRequestService,
    private alert: AlertService
  ) { }

  ngOnInit() {
    this.formulario = this.formBuilder.group({
      name: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(200)]],
      email: [null, [Validators.required, Validators.email,  Validators.minLength(5), Validators.maxLength(200)]],
      age: [null, Validators.required],
      address: [null],
      information: [null, [Validators.maxLength(255)]],
      interests: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      feelings: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]],
      principles: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(255)]]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['user'] && this.visibility === true) {
      this.setValues(this.user);
    }
  }

  onChangeModal() {
    this.visibility = !this.visibility;
    this.event.emit(this.visibility);
  }

  onDelete() {
    this.alert.showConfirm("Deletar usuário", "Tem certeza que deseja deletar o usuário?", "Sim", "Não").subscribe(dados => {
      if (dados === true) {
        this.service.deleteUser(this.user?.id).subscribe({
          next: () => this.alert.showAlertSuccess("Usuário deletado com sucesso!"),
          error: () => this.alert.showAlertError("Erro ao deletar usuário")
        }).add(this.onChangeModal())
      }
    })
  }

  onUpdate() {
    if (this.formulario.valid) {
      this.alert.showConfirm("Atualizar usuário", "Tem certeza que deseja atualizar os dados do usuário?", "Sim", "Não").subscribe(dados => {
        if (dados) {
          const body = { ...this.formulario.value, id: this.user?.id, dataCadastro: this.user?.dataCadastro};
          this.service.updateUser(body).subscribe({
            next: () => this.alert.showAlertSuccess("Usuário atualizado com sucesso!"),
            error: () => this.alert.showAlertError("Erro ao atualizar usuário"),
          }).add(this.onChangeModal());
        }
      });
    }
  }

  resetaDadosForm() {
    this.formulario.reset();
  }

  setValues(user: any) {
    this.formulario.patchValue({
      name: user.name,
      email: user.email,
      age: user.age,
      address: user.address,
      information: user.information,
      interests: user.interests,
      feelings: user.feelings,
      principles: user.principles
    })
  }
}
