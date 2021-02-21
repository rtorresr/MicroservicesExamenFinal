import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { SideNavComponent } from './side-nav/side-nav.component';
import { ProductsComponent } from './products/products.component';
import { PoliciesComponent } from './policies/policies.component';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { CanActivateGuard } from './guard/can-activate.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';

const routes: Routes = [
  {
    path: '',
    canActivate: [CanActivateGuard],
    children: [
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', loadChildren: () => import('./products/products.module').then(m => m.ProductsModule) },
      { path: 'policies', loadChildren: () => import('./policies/policies.module').then(m => m.PoliciesModule) },
      { path: 'chat', component: ChatComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  declarations: [
    AppComponent,
    SideNavComponent,
    PoliciesComponent,
    ChatComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    SharedModule,
    RouterModule.forRoot(routes, { useHash: true }),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: 'Window', useFactory: () => window },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
