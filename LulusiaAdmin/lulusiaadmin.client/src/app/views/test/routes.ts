import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./test.component').then(m => m.TestComponent),
    data: {
      title: $localize`Test Component`
    }
  }
];

