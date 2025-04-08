import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormCheckInputDirective, FormControlDirective, FormDirective, FormLabelDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { TopicViewModel } from '@models/lipstick-shop-models/topic.model';
import { TopicService } from '@services/lipstick-shop-services/topic.service';
import { IconDirective } from '@coreui/icons-angular';
import { cilCloudUpload } from '@coreui/icons';
import { baseUrl, EColors } from '@common/global';
import { DataTableComponent } from "../../../../core/components/data-table/data-table.component";
import { PageInformation, Pagination } from '@models/pagination.model';
import { ToastService } from '@services/helper-services/toast.service';
@Component({
  selector: 'app-topics',
  imports: [ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, IconDirective, DataTableComponent],
  templateUrl: './topics.component.html',
  styleUrl: './topics.component.scss'
})

export class TopicsComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  deleteById: number = 0;
  icons : any = {cilCloudUpload};
  data: Pagination<TopicViewModel> = new Pagination<TopicViewModel>();
  reviewCreateUploadImage: string = '';
  reviewUpdateUploadImage: string = '';
  baseUrl:string = baseUrl;
  createForm: FormGroup = new FormGroup({
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    inNavbar: new FormControl(true, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required),
    inHomePage: new FormControl(true, Validators.required),
    imageFile: new FormControl(null)
    
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    inNavbar: new FormControl(true, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required),
    inHomePage: new FormControl(true, Validators.required),
    imageFile: new FormControl(null)
  });

  constructor(private topicService : TopicService, private toastService : ToastService) {}
  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.topicService.getAll(this.pageInformation.pageIndex,this.pageInformation.pageSize).subscribe((res) => {
      this.data = res.data;
      this.pageInformation.currentPage = this.data.currentPage;
      this.pageInformation.totalPages = this.data.totalPages;
      this.pageInformation.totalItems = this.data.totalItems;
    });
  }
  openFileInput(type: string) {
    if (type === 'create') document.getElementById('createUploadImage')?.click();
    else if(type === 'update') document.getElementById('updateUploadImage')?.click();
  }
  onChangeUploadImage(event: any, type: string) {
    debugger;
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

      formData.append('nameEN', this.createForm.value.nameEN);
      formData.append('nameVN', this.createForm.value.nameVN);
      formData.append('note', this.createForm.value.note);
      formData.append('priority', this.createForm.value.priority);
      formData.append('isActive', this.createForm.value.isActive);
      formData.append('inNavBar', this.createForm.value.inNavBar);
      formData.append('inHomePage', this.createForm.value.inHomePage);
      formData.append('imageFile', this.createForm.value.imageFile);

      this.topicService.create(formData).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.getData();
        this.reviewCreateUploadImage = '';
        this.toastService.showToast(EColors.success,res.message);
        this.createForm.reset();
        this.createForm.patchValue({priority: 1, isActive: true, inNavbar: true});
      });
    }
  }

  toggleLiveCreateModel() {
    this.visibleCreateModal = !this.visibleCreateModal;
  }

  handleLiveCreateModelChange(event: any) {
    this.visibleCreateModal = event;
  }

  get nameENCreateForm() { return this.createForm.get('nameEN'); }
  get nameVNCreateForm() { return this.createForm.get('nameVN'); }
  get priorityCreateForm() { return this.createForm.get('priority'); }
  get noteCreateForm() { return this.createForm.get('note'); }

//#endregion
//#region  Update Form
updateData(id: number) {
  this.topicService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res.data);
    this.toggleLiveUpdateModel();
    if(res.data.avatar){
      this.reviewUpdateUploadImage = `<img src="${baseUrl + res.data.avatar}" alt="Image Preview" class="mw-100 mh-100"/>`;
    }else{
      this.reviewUpdateUploadImage = '';
    }
  });
}
onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    const formData = new FormData();
    formData.append('id', this.updateForm.value.id);
    formData.append('nameEN', this.updateForm.value.nameEN);
    formData.append('nameVN', this.updateForm.value.nameVN);
    formData.append('note', this.updateForm.value.note);
    formData.append('priority', this.updateForm.value.priority);
    formData.append('isActive', this.updateForm.value.isActive);
    formData.append('inNavBar', this.updateForm.value.inNavBar);
    formData.append('inHomePage', this.updateForm.value.inHomePage);
    formData.append('imageFile', this.updateForm.value.imageFile);
    this.topicService.update(formData).subscribe((res) => {
      this.toggleLiveUpdateModel();
      this.reviewUpdateUploadImage = '';
      this.toastService.showToast(EColors.success,res.message);
      this.getData();
    });
  }
}

toggleLiveUpdateModel() {
  this.visibleUpdateModal = !this.visibleUpdateModal;
}

handleLiveUpdateModelChange(event: any) {
  this.visibleUpdateModal = event;
}

get nameENUpdateForm() { return this.updateForm.get('nameEN'); }
get nameVNUpdateForm() { return this.updateForm.get('nameVN'); }
get priorityUpdateForm() { return this.updateForm.get('priority'); }
get noteUpdateForm() { return this.updateForm.get('note'); }

//#endregion
//#region Delete
deleteData(id: number) {
  this.deleteById = id;
  this.toggleLiveDelete();
}
deleteDataConfirm() {
  this.topicService.softDelete(this.deleteById).subscribe((res) => {
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