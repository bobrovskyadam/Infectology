import { Routes } from '@angular/router';
import { Articles } from './pages/articles/articles';
import { ArticleDetail } from './pages/article.detail/article.detail';
import { LoginComponent } from './auth/login/login.component/login.component';
import { RegisterComponent } from './auth/register.component/register.component';
import { Home } from './pages/home/home';
import { authGuardGuard, documentRoleGuard } from './guards/auth.guard';
import { Landing } from './pages/landing/landing';
import { Documents } from './pages/documents/documents';

export const routes: Routes = [
    {path: '', pathMatch: 'full', component: Landing},
    { path: 'articles', component: Articles },
    { path: 'articles/:id', component: ArticleDetail},
    { path: 'login', component: LoginComponent},
    { path: 'register', component: RegisterComponent},
    {
        path: 'home', 
        component: Home,
        canActivate: [authGuardGuard]
    },
    {
        path: 'documents',
        component: Documents,
        canActivate: [authGuardGuard, documentRoleGuard]
    }
];
