export interface AccountModel {
    id: number;
    maillId: number;
    officeId: number;
    roleId: number;
    //fullName: string;
    userName: string;
    email: string;
    phoneNumber: string;
    isVerifiedEmail: boolean;
    isActive: boolean;
}

export interface AccountErrorModel {
    //fullName: string[];
    userName: string[];
    email: string[];
    phoneNumber: string[];
    password: string[];
    roleId: string[];
}