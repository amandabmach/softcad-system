import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { FormComponent } from './pages/form/form.component';
import { LogControlComponent } from './pages/log-control/log-control.component';
import { ReportComponent } from './pages/reports/reports.component';
import { ErrorMessageComponent } from './shared/components/error-message/error-message.component';
import { FormDebugComponent } from './shared/components/form-debug/form-debug.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { SidebarComponent } from './shared/components/sidebar/sidebar.component';
import { TableComponent } from './shared/components/table/table.component';
import { UserRegistrationComponent } from './pages/registrations/registrations.component';
import { UsersListComponent } from './pages/list-users/list-users.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterLink, provideRouter } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { AuthGuard, authGuard } from './guards/auth-guard';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './services/jwt-interceptor.service';
import { ConfirmModalComponent } from './shared/components/modal-confirm/confirm-modal.component';
import { AlertModalComponent } from './shared/components/modal-alert/alert-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ModalFormComponent } from './shared/components/modal-form/modal-form.component';
import { SidebarProfileComponent } from './shared/components/sidebar-profile/sidebar-profile.component';
import { CreateAccountComponent } from './pages/create-account/create-account.component';
import { UserPipe } from './pipe/user.pipe';
import { OperationPipe } from './pipe/operation.pipe';
import { ModalLogsComponent } from './shared/components/modal-logs/modal-logs.component';
import { ResultPipe } from './pipe/result.pipe';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    FormComponent,
    ErrorMessageComponent,
    FormDebugComponent,
    NavbarComponent,
    SidebarComponent,
    TableComponent,
    UserRegistrationComponent,
    ReportComponent,
    LogControlComponent,
    UsersListComponent,
    LayoutComponent,
    ConfirmModalComponent,
    AlertModalComponent,
    ModalFormComponent,
    SidebarProfileComponent,
    CreateAccountComponent,
    UserPipe,
    OperationPipe,
    ResultPipe,
    ModalLogsComponent,
    PageNotFoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,   
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    RouterLink,
    ModalModule.forRoot()
  ],
  providers: [
    provideRouter([
      {
        path: 'guards',
        component: AuthGuard,
        canActivate: [authGuard],
      },
    ]),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
