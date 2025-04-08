export interface HomeBannerViewModel{
    id: number;
    bannerTypeId: number;
    imageName:string;
    subjectEN: string;
    subjectVN: string;
    descriptionEN: string;
    descriptionVN: string;
    redirectUrl: string;
    priority: number;
    isActive: boolean;
}