import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Log } from '../../../models/log';

@Component({
  selector: 'app-modal-logs',
  templateUrl: './modal-logs.component.html',
  styleUrl: './modal-logs.component.scss'
})
export class ModalLogsComponent{

  @Input() log!: Log;
  @Input() modal!: boolean;
  @Output() event = new EventEmitter<boolean>();

  onChangeModal() {
    this.modal = !this.modal;
    this.event.emit(this.modal);
  }
}
