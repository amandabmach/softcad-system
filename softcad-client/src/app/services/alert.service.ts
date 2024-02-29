import { Injectable } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AlertModalComponent } from '../shared/components/modal-alert/alert-modal.component';
import { ConfirmModalComponent } from '../shared/components/modal-confirm/confirm-modal.component';

export enum AlertTypes {
  ERROR = 'error',
  SUCCESS = 'success',
  INFO = "info"
}

@Injectable({
  providedIn: 'root'
})

export class AlertService {

  constructor(private alert: BsModalService) {}  

  private showAlert(message: string, type: AlertTypes) {
    const bsModalRef: BsModalRef = this.alert.show(AlertModalComponent);
    bsModalRef.content.type = type;
    bsModalRef.content.message = message;
  }
  
  showAlertError(message: string) {
    this.showAlert(message, AlertTypes.ERROR);
  }

  showAlertSuccess(message: string) {
    this.showAlert(message, AlertTypes.SUCCESS);
  }

  showAlertInfo(message: string) {
    this.showAlert(message, AlertTypes.INFO);
  }

  showConfirm(title: string, msg: string, ok?: string, cancel?: string) {
    const bsModalRef: BsModalRef = this.alert.show(ConfirmModalComponent);
    bsModalRef.content.title = title;
    bsModalRef.content.msg = msg;

    if (ok) {
      bsModalRef.content.sucessTxt = ok;
    }

    if (cancel) {
      bsModalRef.content.cancelTxt = cancel;
    }
    return (<ConfirmModalComponent>bsModalRef.content).confirmResult;
  }
}
