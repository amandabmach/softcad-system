import { Component, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-alert-modal',
  templateUrl: './alert-modal.component.html',
  styleUrl: './alert-modal.component.scss'
})
export class AlertModalComponent  {

  @Input() type = 'sucess';
  @Input() message!: string;

  constructor(public bsModalRef: BsModalRef) { }

  onClose() {
    this.bsModalRef.hide();
  }
}
