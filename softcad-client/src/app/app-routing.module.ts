import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FormComponent } from './pages/form/form.component';
import { LoginComponent } from './pages/login/login.component';
import { UserRegistrationComponent } from './pages/registrations/registrations.component';
import { ReportComponent } from './pages/reports/reports.component';
import { UsersListComponent } from './pages/list-users/list-users.component';
import { HomeComponent } from './pages/home/home.component';
import { LogControlComponent } from './pages/log-control/log-control.component';
import { LayoutComponent } from './layout/layout.component';
import { authGuard } from './guards/auth-guard';
import { CreateAccountComponent } from './pages/create-account/create-account.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, title:'OC | Login'},
  { path: 'criar-conta', component: CreateAccountComponent, title:'OC | Criar Conta'},
  { path: '',
    component: LayoutComponent,
    children:[
      { path: '', redirectTo: '/login', pathMatch: 'full'},
      { path: 'home', component: HomeComponent, title:'OC | Página Inicial'},
      { path: 'novo-cadastro', component: FormComponent, title:'OC | Novo Cadastro'},
      { path: 'cadastros', component: UserRegistrationComponent, title:'OC | Cadastros'},
      { path: 'relatorios', component: ReportComponent, title:'OC | Relatórios'},
      { path: 'logs', component: LogControlComponent, title:'OC | Controle de Logs'},
      { path: 'listagem', component: UsersListComponent, title:'OC | Lista de Usuários'},
  ], canActivate: [authGuard]},
  { path: '**', component: PageNotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
