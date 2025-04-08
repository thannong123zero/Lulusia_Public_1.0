import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormCheckInputDirective, FormControlDirective, FormDirective, FormLabelDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { CategoryViewModel } from '@models/lipstick-shop-models/category.model';
import { CategoryService } from '@services/lipstick-shop-services/category.service';
import { DataTableComponent } from "@components/data-table/data-table.component";
import { PageInformation, Pagination } from '@models/pagination.model';
import { ToastService } from '@services/helper-services/toast.service';
import { EColors } from '@common/global';

@Component({
  selector: 'app-categories',
  imports: [ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, DataTableComponent],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss'
})
export class CategoriesComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  deleteById: number = 0;
  data: Pagination<CategoryViewModel> = new Pagination<CategoryViewModel>();
  createForm: FormGroup = new FormGroup({
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    inNavbar: new FormControl(true, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required)
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    inNavbar: new FormControl(true, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required)
  });

  constructor(private categoryService : CategoryService, private toastService : ToastService) {}
  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.categoryService.getAll(this.pageInformation.pageIndex, this.pageInformation.pageSize).subscribe((res) => {
      this.data = res.data;
      console.log(this.data);
      console.log(res);
      this.pageInformation.currentPage = this.data.currentPage;
      this.pageInformation.totalItems = this.data.totalItems;
      this.pageInformation.totalPages = this.data.totalPages;
    });
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
      this.categoryService.create(this.createForm.value).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.getData();
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
  this.categoryService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res);
    this.toggleLiveUpdateModel();
  });
}
onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    this.categoryService.update(this.updateForm.value).subscribe((res) => {
      this.toggleLiveUpdateModel();
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
  this.categoryService.softDelete(this.deleteById).subscribe((res) => {
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