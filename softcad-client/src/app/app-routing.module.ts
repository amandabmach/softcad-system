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
  { path: 'login', component: LoginComponent, title:'SC | Login'},
  { path: 'criar-conta', component: CreateAccountComponent, title:'SC | Criar Conta'},
  { path: '',
    component: LayoutComponent,
    children:[
      { path: '', redirectTo: '/login', pathMatch: 'full'},
      { path: 'home', component: HomeComponent, title:'SC | Página Inicial'},
      { path: 'novo-cadastro', component: FormComponent, title:'SC | Novo Cadastro'},
      { path: 'cadastros', component: UserRegistrationComponent, title:'SC | Cadastros'},
      { path: 'relatorios', component: ReportComponent, title:'SC | Relatórios'},
      { path: 'logs', component: LogControlComponent, title:'SC | Controle de Logs'},
      { path: 'listagem', component: UsersListComponent, title:'SC | Lista de Usuários'},
  ]},
  { path: '**', component: PageNotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
