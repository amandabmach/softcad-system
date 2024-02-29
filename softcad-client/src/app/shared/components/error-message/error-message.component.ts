import { Component, Input } from '@angular/core';

import { FormValidationsService } from '../../../services/form-validations.service';


@Component({
  selector: 'app-error-message',
  templateUrl: './error-message.component.html',
  styleUrl: './error-message.component.scss'
})
export class ErrorMessageComponent {
  
  @Input() control!: any;
  @Input() label!: string;
  
  constructor() { }

  get errorMessage() {
    for (const propertyName in this.control.errors) {
      if (this.control.errors.hasOwnProperty(propertyName) && this.control.touched) {
          return FormValidationsService.getErrorMessage(this.label, propertyName, this.control.errors[propertyName]);
        }
    }
    return null;
  }

}
