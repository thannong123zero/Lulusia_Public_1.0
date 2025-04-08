import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EColors } from '@common/global';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormCheckInputDirective, FormControlDirective, FormDirective, FormLabelDirective, FormSelectDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { CategoryViewModel } from '@models/lipstick-shop-models/category.model';
import { SubCategoryViewModel } from '@models/lipstick-shop-models/sub-category.model';
import { PageInformation, Pagination } from '@models/pagination.model';
import { ToastService } from '@services/helper-services/toast.service';
import { CategoryService } from '@services/lipstick-shop-services/category.service';
import { SubCategoryService } from '@services/lipstick-shop-services/sub-category.service';
import { DataTableComponent } from "@components/data-table/data-table.component";

@Component({
  selector: 'app-sub-categories',
  imports: [ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule, FormSelectDirective,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, DataTableComponent],
  templateUrl: './sub-categories.component.html',
  styleUrl: './sub-categories.component.scss'
})
export class SubCategoriesComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  

  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  categoryId: number = -1;
  deleteById: number = 0;
  data: Pagination<SubCategoryViewModel> = new Pagination<SubCategoryViewModel>();
  categoryList: CategoryViewModel[] = [];
  createForm: FormGroup = new FormGroup({
    categoryId: new FormControl(-1, Validators.min(1)),
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    //inNavbar: new FormControl(true, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required)
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    categoryId: new FormControl(-1, Validators.min(1)),
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    note: new FormControl('',Validators.maxLength(500)),
    //inNavbar: new FormControl(true, Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(9999)]),
    isActive: new FormControl(true, Validators.required)
  });

  constructor(private categoryService : CategoryService, private subCategoryService : SubCategoryService, private toastService : ToastService) {}

  ngOnInit(): void {
    this.getData();
    this.categoryService.getAllActive().subscribe((res) => {
      this.categoryList = res.data;
    });
  }
  getData(){
    this.subCategoryService.getAll(this.pageInformation.pageIndex,this.pageInformation.pageSize,this.categoryId).subscribe((res) => {
      this.data = res.data;
      this.pageInformation.currentPage = this.data.currentPage;
      this.pageInformation.totalItems = this.data.totalItems;
      this.pageInformation.totalPages = this.data.totalPages;
    });
  }
  filter(category: any) {
    let categoryId = category.target.value;
    this.categoryId = categoryId;
    this.pageInformation.pageIndex = 1;
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
      this.subCategoryService.create(this.createForm.value).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.toastService.showToast(EColors.success,res.message);
        this.getData();
        this.createForm.reset();
        this.createForm.patchValue({priority: 1,categoryId: -1, isActive: true});
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
  this.subCategoryService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res.data);
    this.toggleLiveUpdateModel();
  });
}
onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    this.subCategoryService.update(this.updateForm.value).subscribe((res) => {
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
  this.subCategoryService.softDelete(this.deleteById).subscribe((res) => {
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
