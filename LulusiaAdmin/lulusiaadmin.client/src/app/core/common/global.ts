export const baseUrl = "https://localhost:7025/";


export enum EPageType
{
    AboutUs,
    Contact,
    ReturnPolicy,
    TermsAndConditions,
    PrivacyPolicy,
}

export enum EBannerType
{
    MainBanner,
    SubBanner
}

export enum EColors {
    primary = 'primary',
    secondary = 'secondary',
    success = 'success',
    info = 'info',
    warning = 'warning',
    danger = 'danger',
    dark = 'dark',
    light = 'light'
}

export enum EActions{
    View = 'View',
    Create = 'Create',
    Update = 'Update',
    SoftDelete = 'SoftDelete',
    Restore = 'Restore',
    Delete = 'Delete',
    Approve = 'Approve',
    Reject = 'Reject',
    Activate = 'Activate',
    Deactivate = 'Deactivate',
    Assign = 'Assign',
    Unassign = 'Unassign',
    Import = 'Import',
    export = 'Export',
}
