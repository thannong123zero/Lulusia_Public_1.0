import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormControlDirective, FormDirective, FormLabelDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { BrandViewModel } from '@models/lipstick-shop-models/brand.model';
import { BrandService } from '@services/lipstick-shop-services/brand.service';
import { cilCloudUpload } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { PageInformation, Pagination } from '@models/pagination.model';
import { ToastService } from '@services/helper-services/toast.service';
import { baseUrl, EColors } from '@common/global';
import { DataTableComponent } from "@components/data-table/data-table.component";

@Component({
  selector: 'app-brands',
  imports: [ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, IconDirective, DataTableComponent],
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.scss'
})
export class BrandsComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  baseUrl: string = baseUrl;
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  deleteById: number = 0;
  data: Pagination<BrandViewModel> = new Pagination<BrandViewModel>();
  reviewCreateUploadImage: string = '';
  reviewUpdateUploadImage: string = '';
  icons : any = {cilCloudUpload};
  createForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required),
    imageFile: new FormControl(null)
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    name: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required),
    imageFile: new FormControl(null)

  });

  constructor(private brandService : BrandService,private toastService : ToastService) {}
  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.brandService.getAll(this.pageInformation.pageIndex,this.pageInformation.pageSize).subscribe((res) => {
      this.data = res.data;
      this.pageInformation.currentPage = this.data.currentPage;
      this.pageInformation.totalItems = this.data.totalItems;
      this.pageInformation.totalPages = this.data.totalPages;
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
      formData.append('name', this.createForm.value.name);
      if(this.createForm.value.note) formData.append('note', this.createForm.value.note);
      formData.append('priority', this.createForm.value.priority);
      formData.append('isActive', this.createForm.value.isActive);
      formData.append('imageFile', this.createForm.value.imageFile);

      this.brandService.create(formData).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.getData();
        this.toastService.showToast(EColors.success,res.message);
        this.createForm.reset();
        this.createForm.patchValue({priority: 1, isActive: true});
        this.reviewCreateUploadImage = '';
      });
    }
  }

  toggleLiveCreateModel() {
    this.visibleCreateModal = !this.visibleCreateModal;
  }

  handleLiveCreateModelChange(event: any) {
    this.visibleCreateModal = event;
  }

  get nameCreateForm() { return this.createForm.get('name'); }
  get priorityCreateForm() { return this.createForm.get('priority'); }
  get noteCreateForm() { return this.createForm.get('note'); }

//#endregion
//#region  Update Form
updateData(id: number) {
  this.brandService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res.data);
    this.toggleLiveUpdateModel();
    if(res.data.avatar){
      this.reviewUpdateUploadImage = `<img src="${ this.baseUrl +res.data.avatar}" alt="Image Preview" class="mw-100 mh-100"/>`;
    }else{
      this.reviewUpdateUploadImage = '';
    }
  });
}
onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    const formData = new FormData();
    formData.append('id', this.updateForm.value.id);
    formData.append('name', this.updateForm.value.name);
    formData.append('note', this.updateForm.value.note);
    formData.append('priority', this.updateForm.value.priority);
    formData.append('isActive', this.updateForm.value.isActive);
    formData.append('imageFile', this.updateForm.value.imageFile);

    this.brandService.update(formData).subscribe((res) => {
      this.toggleLiveUpdateModel();
      this.getData();
      this.toastService.showToast(EColors.success,res.message);
    });
  }
}

toggleLiveUpdateModel() {
  this.visibleUpdateModal = !this.visibleUpdateModal;
}

handleLiveUpdateModelChange(event: any) {
  this.visibleUpdateModal = event;
}

get nameUpdateForm() { return this.updateForm.get('name'); }
get priorityUpdateForm() { return this.updateForm.get('priority'); }
get noteUpdateForm() { return this.updateForm.get('note'); }

//#endregion
//#region Delete
deleteData(id: number) {
  this.deleteById = id;
  this.toggleLiveDelete();
}
deleteDataConfirm() {
  this.brandService.softDelete(this.deleteById).subscribe(() => {
    this.toggleLiveDelete();
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
