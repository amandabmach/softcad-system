import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-debug',
  templateUrl: './form-debug.component.html',
  styleUrl: './form-debug.component.scss'
})
export class FormDebugComponent {
  @Input() form: any;

}
