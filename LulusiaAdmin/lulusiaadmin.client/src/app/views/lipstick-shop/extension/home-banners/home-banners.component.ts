import { animation } from '@angular/animations';
import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { baseUrl, EColors } from '@common/global';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { cilCloudUpload } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { HomeBannerViewModel } from '@models/lipstick-shop-models/home-banner.model';
import { PageInformation, Pagination } from '@models/pagination.model';
import { HomeBannerService } from '@services/lipstick-shop-services/home-banner.service';
import { DataTableComponent } from "@components/data-table/data-table.component";
import { ToastService } from '@services/helper-services/toast.service';
@Component({
  selector: 'app-home-banners',
  imports: [ ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, IconDirective, FormSelectDirective,DataTableComponent],
  templateUrl: './home-banners.component.html',
  styleUrl: './home-banners.component.scss'
})
export class HomeBannersComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  baseUrl:string = baseUrl;
  bannerTypeId: number = -1;
  deleteById: number = 0;
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  icons : any = {cilCloudUpload};
  bannerTypeList: string[] = ['Main Banner', 'Sub Banner'];
  animationList: string[] = ['Snow', 'Fade'];
  data: Pagination<HomeBannerViewModel> = new Pagination<HomeBannerViewModel>();
  reviewCreateUploadImage: string = '';
  reviewUpdateUploadImage: string = '';
  createForm: FormGroup = new FormGroup({
    bannerTypeId: new FormControl(-1, [Validators.min(0)]),
    animation: new FormControl(-1),
    subjectEN: new FormControl(''),
    subjectVN: new FormControl(''),
    descriptionEN: new FormControl(''),
    descriptionVN: new FormControl(''),
    redirectUrl: new FormControl(''),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required),
    imageFile: new FormControl(null,Validators.required),
    tag: new FormControl('')
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    bannerTypeId: new FormControl(-1, [Validators.min(0)]),
    animation: new FormControl(null),
    subjectEN: new FormControl(''),
    subjectVN: new FormControl(''),
    descriptionEN: new FormControl(''),
    descriptionVN: new FormControl(''),
    redirectUrl: new FormControl(''),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required),
    imageFile: new FormControl(null)
  });

  constructor(private homeBannerService : HomeBannerService, private toastService: ToastService) {}
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.homeBannerService.getAll(this.pageInformation.pageIndex,this.pageInformation.pageSize,this.bannerTypeId).subscribe((res) => {
      this.data = res.data;
      this.pageInformation.totalItems = res.data.totalItems;
      this.pageInformation.totalPages = res.data.totalPages;
      this.pageInformation.currentPage = res.data.currentPage;
    });
  }

  openFileInput(type: string) {
    if (type === 'create') document.getElementById('createUploadImage')?.click();
    else if(type === 'update') document.getElementById('updateUploadImage')?.click();
  }
  
  onChangeUploadImage(event: any, type: string) {
    const file:File = event.target.files[0];
    if (file) {
      //show image preview
      const reader = new FileReader();
      reader.onload = (e: any) => {
        if(type === 'create'){
          this.reviewCreateUploadImage = `<img src="${e.target.result}" alt="Image Preview" class="mw-100 mh-100"/>`;
          this.createForm.patchValue({
            imageFile: file
          });
        }else if(type === 'update'){
          this.reviewUpdateUploadImage = `<img src="${e.target.result}" alt="Image Preview" class="mw-100 mh-100"/>`;
          this.updateForm.patchValue({
            imageFile: file
          });
        }
          
      };
      reader.readAsDataURL(file);
    }
  }
  filter(event: any){
    const value = event.target.value;
    this.bannerTypeId = value;
    this.getData();
  }
  onPageIndexChange(index: any) {
    this.pageInformation.pageIndex = index;
    this.getData();
  }
  onPageSizeChange(size: any) {
    this.pageInformation.pageSize = size;
    this.pageInformation.pageIndex = 1;
    this.getData();
  }
//#region  Create Form
  onSubmitCreateForm() {
    if (this.createForm.valid) {
      const formData = new FormData();
      if(this.createForm.value.animation)
      {
        formData.append('animation', this.createForm.value.animation);
      }
      formData.append('bannerTypeId', this.createForm.value.bannerTypeId);
      if(this.createForm.value.subjectEN){
        formData.append('subjectEN', this.createForm.value.subjectEN);
      }
      if(this.createForm.value.subjectVN){
        formData.append('subjectVN', this.createForm.value.subjectVN);
      }
      if(this.createForm.value.descriptionEN){
        formData.append('descriptionEN', this.createForm.value.descriptionEN);
      }
      if(this.createForm.value.descriptionVN){
        formData.append('descriptionVN', this.createForm.value.descriptionVN);
      }
      if(this.createForm.value.redirectUrl){
        formData.append('redirectUrl', this.createForm.value.redirectUrl);
      }
      formData.append('isActive', this.createForm.value.isActive);
      formData.append('priority', this.createForm.value.priority);
      formData.append('imageFile', this.createForm.value.imageFile);
      if(this.createForm.value.tag){
        formData.append('tag', this.createForm.value.tag);
      }
      this.homeBannerService.create(formData).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.toastService.showToast(EColors.success,res.message);
        this.reviewCreateUploadImage = '';
        this.getData();
        this.createForm.reset();
        this.createForm.patchValue({priority: 1,bannerTypeId:-1,animation:-1, isActive: true});
      });
    }
  }

  toggleLiveCreateModel() {
    this.visibleCreateModal = !this.visibleCreateModal;
  }

  handleLiveCreateModelChange(event: any) {
    this.visibleCreateModal = event;
  }

  get priorityCreateForm() { return this.createForm.get('priority'); }
  get bannerTypeIdCreateForm() { return this.createForm.get('bannerTypeId'); }

//#endregion
//#region  Update Form
updateData(id: number) {
  this.homeBannerService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res.data);
    this.toggleLiveUpdateModel();
    if(res.data.imageName){
      this.reviewUpdateUploadImage = `<img src="${baseUrl + res.data.imageName}" alt="Image Preview" class="mw-100 mh-100"/>`;
    }else{
      this.reviewUpdateUploadImage = '';
    }
  });
}
onSubmitUpdateForm() {
  debugger;
  if (this.updateForm.valid) {
    const formData = new FormData();
    formData.append('id', this.updateForm.value.id);
    formData.append('bannerTypeId', this.updateForm.value.bannerTypeId);
    formData.append('subjectEN', this.updateForm.value.subjectEN);
    formData.append('subjectVN', this.updateForm.value.subjectVN);
    formData.append('descriptionEN', this.updateForm.value.descriptionEN);
    formData.append('descriptionVN', this.updateForm.value.descriptionVN);
    formData.append('redirectUrl', this.updateForm.value.redirectUrl);
    formData.append('isActive', this.updateForm.value.isActive);
    formData.append('priority', this.updateForm.value.priority);
    formData.append('imageFile', this.updateForm.value.imageFile);

    this.homeBannerService.update(formData).subscribe((res) => {
      this.toggleLiveUpdateModel();
      this.toastService.showToast(EColors.success,res.message);
      this.getData();
    },
    (error) => {
      console.log(error);
      this.toastService.showToast(EColors.danger,error.error.message);
    });
  }
}

toggleLiveUpdateModel() {
  this.visibleUpdateModal = !this.visibleUpdateModal;
}

handleLiveUpdateModelChange(event: any) {
  this.visibleUpdateModal = event;
}

get priorityUpdateForm() { return this.updateForm.get('priority'); }

//#endregion
//#region Delete
deleteData(id: number) {
  this.deleteById = id;
  this.toggleLiveDelete();
}
deleteDataConfirm() {
  this.homeBannerService.softDelete(this.deleteById).subscribe((res) => {
    this.toggleLiveDelete();
    this.toastService.showToast(EColors.success,res.message);
    this.getData();
  });
}
toggleLiveDelete() {
  this.visibleDelete = !this.visibleDelete;
}

handleLiveDeleteChange(event: any) {
  this.visibleDelete = event;
}
//#endregion
}