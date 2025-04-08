import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Test Component',
    url: '/test-component',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
  {
    title: true,
    name: 'Features'
  },
  {
    name: 'Qr Code',
    url: '/features/qr-code',
    iconComponent: { name: 'cil-qr-code' }
  },
  {
    name: 'Email Service',
    url: '/features/email-service',
    iconComponent: { name: 'cil-envelope-closed' }
  },
  {
    title: true,
    name: 'Lipstick Shop'
  },
  {
    name: 'Report',
    url: '/lipstick-shop/report',
    iconComponent: { name: 'cil-chart-line' }
  },
  {
    name: 'Orders',
    url: '/lipstick-shop/orders',
    iconComponent: { name: 'cil-notes' }
  },
  {
    name: 'Products',
    url: '/lipstick-shop/products',
    iconComponent: { name: 'cil3d' }
  },
  {
    name: 'Blogs',
    url: '/lipstick-shop/blogs',
    iconComponent: { name: 'cil-color-border' }
  },
  {
    name: 'Extend',
    url: '/lipstick-shop/extension',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Brands',
        url: '/lipstick-shop/extension/brands',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Categories',
        url: '/lipstick-shop/extension/categories',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'SubCategories',
        url: '/lipstick-shop/extension/sub-categories',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Colors',
        url: '/lipstick-shop/extension/colors',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Sizes',
        url: '/lipstick-shop/extension/sizes',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Topics',
        url: '/lipstick-shop/extension/topics',
        icon: 'nav-icon-bullet'
      },
      {
        name: "Page Types",
        url: '/lipstick-shop/extension/page-types',
        icon: 'nav-icon-bullet'
      },
      {
        name: "Page Contents",
        url: '/lipstick-shop/extension/page-contents',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Home Banners',
        url: '/lipstick-shop/extension/home-banners',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  //#region System Management
  {
    name: 'System Management',
    title: true
  },
  {
    name: 'Accounts',
    url: '/accounts',
    iconComponent: { name: 'cil-people' }
  },
  {
    name: 'Logs',
    url: '/logs',
    iconComponent: { name: 'cil-book' }
  },
  {
    name: 'Extend',
    url: '/system-management/extension',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Roles',
        url: '/system-management/extension/roles',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Modules',
        url: '/system-management/extension/modules',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Actions',
        url: '/system-management/extension/actions',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Settings',
        url: '/system-management/extension/settings',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  //#endregion
  {
    title: true,
    name: 'Theme'
  },
  {
    name: 'Colors',
    url: '/theme/colors',
    iconComponent: { name: 'cil-drop' }
  },
  {
    name: 'Typography',
    url: '/theme/typography',
    linkProps: { fragment: 'headings' },
    iconComponent: { name: 'cil-pencil' }
  },
  {
    name: 'Components',
    title: true
  },
  {
    name: 'Base',
    url: '/base',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Accordion',
        url: '/base/accordion',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Breadcrumbs',
        url: '/base/breadcrumbs',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Cards',
        url: '/base/cards',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Carousel',
        url: '/base/carousel',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Collapse',
        url: '/base/collapse',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'List Group',
        url: '/base/list-group',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Navs & Tabs',
        url: '/base/navs',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Pagination',
        url: '/base/pagination',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Placeholder',
        url: '/base/placeholder',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Popovers',
        url: '/base/popovers',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Progress',
        url: '/base/progress',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Spinners',
        url: '/base/spinners',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Tables',
        url: '/base/tables',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Tabs',
        url: '/base/tabs',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Tooltips',
        url: '/base/tooltips',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Buttons',
    url: '/buttons',
    iconComponent: { name: 'cil-cursor' },
    children: [
      {
        name: 'Buttons',
        url: '/buttons/buttons',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Button groups',
        url: '/buttons/button-groups',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Dropdowns',
        url: '/buttons/dropdowns',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Forms',
    url: '/forms',
    iconComponent: { name: 'cil-notes' },
    children: [
      {
        name: 'Form Control',
        url: '/forms/form-control',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Select',
        url: '/forms/select',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Checks & Radios',
        url: '/forms/checks-radios',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Range',
        url: '/forms/range',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Input Group',
        url: '/forms/input-group',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Floating Labels',
        url: '/forms/floating-labels',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Layout',
        url: '/forms/layout',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Validation',
        url: '/forms/validation',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Charts',
    iconComponent: { name: 'cil-chart-pie' },
    url: '/charts'
  },
  {
    name: 'Icons',
    iconComponent: { name: 'cil-star' },
    url: '/icons',
    children: [
      {
        name: 'CoreUI Free',
        url: '/icons/coreui-icons',
        icon: 'nav-icon-bullet',
        badge: {
          color: 'success',
          text: 'FREE'
        }
      },
      {
        name: 'CoreUI Flags',
        url: '/icons/flags',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'CoreUI Brands',
        url: '/icons/brands',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Notifications',
    url: '/notifications',
    iconComponent: { name: 'cil-bell' },
    children: [
      {
        name: 'Alerts',
        url: '/notifications/alerts',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Badges',
        url: '/notifications/badges',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Modal',
        url: '/notifications/modal',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Toast',
        url: '/notifications/toasts',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    name: 'Widgets',
    url: '/widgets',
    iconComponent: { name: 'cil-calculator' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
  {
    title: true,
    name: 'Extras'
  },
  {
    name: 'Pages',
    url: '/login',
    iconComponent: { name: 'cil-star' },
    children: [
      {
        name: 'Login',
        url: '/login',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Register',
        url: '/register',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Error 404',
        url: '/404',
        icon: 'nav-icon-bullet'
      },
      {
        name: 'Error 500',
        url: '/500',
        icon: 'nav-icon-bullet'
      }
    ]
  },
  {
    title: true,
    name: 'Links',
    class: 'mt-auto'
  },
  {
    name: 'Docs',
    url: 'https://coreui.io/angular/docs/5.x/',
    iconComponent: { name: 'cil-description' },
    attributes: { target: '_blank' }
  }
];
