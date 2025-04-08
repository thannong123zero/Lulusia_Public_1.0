import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Extend'
    },
    children: [
      {
        path: '',
        redirectTo: 'brands',
        pathMatch: 'full'
      },
      {
        path: 'brands',
        loadComponent: () => import('./brands/brands.component').then(m => m.BrandsComponent),
        data: {
          title: 'Brands'
        }
      },
      {
        path: 'categories',
        loadComponent: () => import('./categories/categories.component').then(m => m.CategoriesComponent),
        data: {
          title: 'Categories'
        }
      },
      {
        path: 'sub-categories',
        loadComponent: () => import('./sub-categories/sub-categories.component').then(m => m.SubCategoriesComponent),
        data: {
          title: 'Sub Categories'
        }
      },
      {
        path: 'colors',
        loadComponent: () => import('./colors/colors.component').then(m => m.ColorsComponent),
        data: {
          title: 'Colors'
        }
      },
      {
        path: 'sizes',
        loadComponent: () => import('./sizes/sizes.component').then(m => m.SizesComponent),
        data: {
          title: 'Sizes'
        }
      },
      {
        path: 'topics',
        loadComponent: () => import('./topics/topics.component').then(m => m.TopicsComponent),
        data: {
          title: 'Topics'
        }
      },
      {
        path: 'page-contents',
        loadComponent: () => import('./page-contents/page-contents.component').then(m => m.PageContentsComponent),
        data: {
          title: 'Page Contents'
        }
      },
      {
        path:"page-types",
        loadComponent: () => import('./page-types/page-types.component').then(m => m.PageTypesComponent),
        data: {
          title: 'Page Types'
        }
      },
      {
        path: 'home-banners',
        loadComponent: () => import('./home-banners/home-banners.component').then(m => m.HomeBannersComponent),
        data: {
          title: 'Home Banners'
        }
      }
     
      
    ]
  }
];


