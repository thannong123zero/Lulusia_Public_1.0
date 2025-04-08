import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./report.component').then(m => m.ReportComponent),
    data: {
      title: $localize`Report`
    }
  }
];

