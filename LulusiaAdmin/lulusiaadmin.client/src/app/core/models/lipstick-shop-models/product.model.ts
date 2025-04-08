export interface ProductViewModel {
    id: number;
    categoryId: number;
    subCategoryId: number;
    nameEN: string;
    nameVN: string;
    descriptionEN: string;
    descriptionVN: string;
    detailsEN: string;
    detailsVN: string;
    images: string;
    avatar: string;
    backgroundImage: string;
    inHomePage: boolean;
    isRecommended: boolean;
    isActive: boolean;
    price: number;
    quantity: number;
    discountPercent: number;
    startDiscountDate: Date;
    endDiscountDate: Date;
    saleOff: boolean;
}