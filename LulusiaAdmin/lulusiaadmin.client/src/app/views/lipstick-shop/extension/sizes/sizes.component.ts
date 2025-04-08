import { NgFor, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonCloseDirective, ButtonDirective, CardBodyComponent, CardComponent, CardHeaderComponent, FormCheckComponent, FormCheckInputDirective, FormControlDirective, FormDirective, FormLabelDirective, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective, ModalToggleDirective, TableDirective } from '@coreui/angular';
import { SizeViewModel } from '@models/lipstick-shop-models/size.model';
import { PageInformation, Pagination } from '@models/pagination.model';
import { ToastService } from '@services/helper-services/toast.service';
import { SizeService } from '@services/lipstick-shop-services/size.service';
import { DataTableComponent } from "@components/data-table/data-table.component";
import { EColors } from '@common/global';

@Component({
  selector: 'app-sizes',
  templateUrl: './sizes.component.html',
  styleUrl: './sizes.component.scss',
  imports: [ ModalBodyComponent, NgFor, NgIf, FormControlDirective, FormLabelDirective,
    ModalComponent, ButtonDirective, FormCheckComponent, FormDirective, ReactiveFormsModule,
    ModalFooterComponent, ButtonCloseDirective, ModalHeaderComponent, DataTableComponent]
})
export class SizesComponent implements OnInit {
  pageInformation: PageInformation = new PageInformation();
  visibleCreateModal: boolean = false;
  visibleUpdateModal: boolean = false;
  visibleDelete: boolean = false;
  deleteById: number = 0;
  data: Pagination<SizeViewModel> = new Pagination<SizeViewModel>();
  createForm: FormGroup = new FormGroup({
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(100)]),
    isActive: new FormControl(true, Validators.required)
  });
  updateForm: FormGroup = new FormGroup({
    id: new FormControl(0, Validators.required),
    nameEN: new FormControl('', Validators.required),
    nameVN: new FormControl('', Validators.required),
    priority: new FormControl(1, [Validators.min(1), Validators.max(100)]),
    isActive: new FormControl(true, Validators.required)
  });

  constructor(private sizeService : SizeService, private toastService: ToastService) {}
  ngOnInit(): void {
    this.getData();
  }

  getData(){
    this.sizeService.getAll(this.pageInformation.pageIndex,this.pageInformation.pageSize).subscribe((res) => {
      this.data = res.data;
      this.pageInformation.currentPage = res.data.currentPage;
      this.pageInformation.totalItems = res.data.totalItems;
      this.pageInformation.totalPages = res.data.totalPages;
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
      this.sizeService.create(this.createForm.value).subscribe((res) => {
        this.toggleLiveCreateModel();
        this.toastService.showToast(EColors.success,res.message);
        this.getData();
        this.createForm.reset();
        this.createForm.patchValue({priority: 1, isActive: true});
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
//#endregion
//#region  Update Form
updateData(id: number) {
  this.sizeService.getById(id).subscribe((res) => {
    this.updateForm.patchValue(res);
    this.toggleLiveUpdateModel();
  });
}
onSubmitUpdateForm() {
  if (this.updateForm.valid) {
    this.sizeService.update(this.updateForm.value).subscribe((res) => {
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

//#endregion
//#region Delete
deleteData(id: number) {
  this.deleteById = id;
  this.toggleLiveDelete();
}
deleteDataConfirm() {
  this.sizeService.softDelete(this.deleteById).subscribe((res) => {
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
